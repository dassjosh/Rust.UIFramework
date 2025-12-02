using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Network;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Plugins;

[Info("Asset Browser", "MJSU", "1.0.0")]
[Description("Allows browsing Rust assets")]
public class AssetBrowser : RustPlugin, IUiFrameworkPlugin
{
    #region Class Fields
    private PluginConfig _pluginConfig;

    private const string UsePermission = "assetbrowser.use";
    private const string AccentColor = "#de8732";

    private static AssetBrowser _ins;
    
    private readonly Folder _sprites = Folder.CreateFolders(typeof(UiSprites));
    private readonly Folder _textures = Folder.CreateFolders(typeof(UiTextures));
    private readonly Folder _materials = Folder.CreateFolders(typeof(UiMaterials));
    private readonly Folder _fonts = Folder.CreateFolders(typeof(UiFontCache));
    private readonly Folder _items = new("root", string.Empty);

    private readonly UiCommands _commands = GetLibrary<UiCommands>();
    private readonly UiPlayerStore _store = GetLibrary<UiPlayerStore>();
    private readonly UiImageStorage _storage = GetLibrary<UiImageStorage>();
    private UiCommandHandler _uiCommands;
    
    public UiPluginPool PluginPool { get; set; }
    
    public enum AssetType : byte
    {
        None,
        Sprite,
        Texture,
        Material,
        PlayingCard,
        Item,
        RustIcon,
        FontAwesomeRegular,
        FontAwesomeSolid,
        Font
    }
    #endregion

    #region Setup & Loading
    private void Init()
    {
        _ins = this;
        permission.RegisterPermission(UsePermission, this);
        _uiCommands = new UiCommandHandler(this);
    }

    protected override void LoadDefaultMessages()
    {
        lang.RegisterMessages(new Dictionary<string, string>
        {
            [LangKeys.Chat] = $"<color=#bebebe>[<color={AccentColor}>{Title}</color>] {{0}}</color>",
            [LangKeys.NoPermission] = "You do not have permission to use this command"
        }, this);
    }

    protected override void LoadDefaultConfig()
    {
        PrintWarning("Loading Default Config");
    }

    protected override void LoadConfig()
    {
        base.LoadConfig();
        Config.Settings.DefaultValueHandling = DefaultValueHandling.Populate;
        _pluginConfig = AdditionalConfig(Config.ReadObject<PluginConfig>());
        Config.WriteObject(_pluginConfig);
    }

    public PluginConfig AdditionalConfig(PluginConfig config)
    {
        return config;
    }
        
    private void OnServerInitialized()
    {
        // BasePlayer player = BasePlayer.activePlayerList.FirstOrDefault();
        // if (player)
        // {
        //     CreateUi(player);
        // }
        
        CreateItemsFolder();
        UiInit();
    }

    private void CreateItemsFolder()
    {
        HashSet<string> itemsNames = new();
        foreach (ItemDefinition def in ItemManager.itemList)
        {
            Folder folder = _items.GetOrCreateFolder(EnumCache<ItemCategory>.ToString(def.category));
            string name = def.displayName.english;
            if (!itemsNames.Add(name))
            {
                for (int i = 0; i < 100; i++)
                {
                    name = $"{def.displayName.english}{i}";
                    if (itemsNames.Add(name))
                    {
                        break;
                    }
                }
            }
            
            folder.AddFile(name, StringCache<int>.ToString(def.itemid));
        }
    }
    
    private void Unload()
    {
        BaseBuilder.DestroyUi(UiName);
        _ins = null;
    }
    #endregion

    #region UiFramework Hooks

    private const string LoadingImage = "https://rust-images.joshdass.dev/load.png";
    private const string ErrorImage = "https://rust-images.joshdass.dev/error.png";
    private const string WarningImage = "https://rust-images.joshdass.dev/warning.png";
    private void OnUiImageStorageReady()
    {
        _storage.RegisterImage(this, LoadingImage);
        _storage.RegisterImage(this, ErrorImage);
        _storage.RegisterImage(this, WarningImage);
    }
    #endregion
    
    #region Chat Command
    [ChatCommand("ab")]
    private void AssetBrowserChatCommand(BasePlayer player)
    {
        if (!HasPermission(player, UsePermission) && !player.IsAdmin)
        {
            Chat(player, LangKeys.NoPermission);
            return;
        }   
        
        CreateUi(player);
    }

    private void OnClientCommand(Connection connection, string command)
    {
        Puts($"Command: {command}");
    }
    #endregion

    #region Helper Methods
    public void Chat(BasePlayer player, string key) => PrintToChat(player, Lang(LangKeys.Chat, player, Lang(key, player)));
    public void Chat(BasePlayer player, string key, params object[] args) => PrintToChat(player, Lang(LangKeys.Chat, player, Lang(key, player, args)));

    public string Lang(string key, BasePlayer player = null)
    {
        try
        {
            return lang.GetMessage(key, this, player?.UserIDString);
        }
        catch (Exception ex)
        {
            PrintError($"Lang Key '{key}' threw exception:\n{ex}");
            throw;
        }
    }
        
    public string Lang(string key, BasePlayer player = null, params object[] args)
    {
        try
        {
            return string.Format(lang.GetMessage(key, this, player?.UserIDString), args);
        }
        catch (Exception ex)
        {
            PrintError($"Lang Key '{key}' threw exception:\n{ex}");
            throw;
        }
    }

    public bool HasPermission(BasePlayer player, string perm) => permission.UserHasPermission(player.UserIDString, perm);
    #endregion

    #region Classes
    public class PluginConfig
    {
        
    }

    public class PluginData
    {

    }

    public class LangKeys
    {
        public const string Chat = nameof(Chat);
        public const string NoPermission = nameof(NoPermission);
    }

    public class Folder
    {
        public readonly string Name;
        public readonly string Path;
        public readonly Dictionary<string, Folder> Subfolders = new ();
        public readonly Dictionary<string, string> Files = new ();

        public Folder(string name, string path)
        {
            Name = name;
            if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(name))
            {
                Path = $"{path}/{name}";
            } 
            else if (string.IsNullOrEmpty(path))
            {
                Path = name;
            }
            else
            {
                Path = string.Empty;
            }
        }

        public static Folder CreateFolders(Type type)
        {
            Folder root = new("", "");
            ProcessFolder(type, root);
            return root;
        }

        public Folder GetOrCreateFolder(string name)
        {
            if (!Subfolders.TryGetValue(name, out Folder folder))
            {
                Subfolders[name] = folder = new Folder(name, Path);
            }

            return folder;
        }
    
        public void AddFolder(string name, Folder folder)
        {
            if (!Subfolders.TryAdd(name, folder))
            {
                throw new Exception($"{name} folder cannot be duplicated");
            }
        }
		
        public void AddFile(string name, string assetPath)
        {
            if (!Files.TryAdd(name, assetPath))
            {
                throw new Exception($"{name} file cannot be duplicated");
            }
        }

        public Folder GetFolderFromPath(string path)
        {
            StringTokenizer tokenizer = new(path, "/");
            Folder folder = this;
            while (tokenizer.MoveNext())
            {
                folder = folder.GetFolder(tokenizer.Current);
            }

            return folder;
        }
    
        public Folder GetFolder(ReadOnlySpan<char> folderSpan)
        {
            string folderName = folderSpan.ToString();
            if (!Subfolders.TryGetValue(folderName, out Folder folder))
            {
                throw new KeyNotFoundException($"Folder '{folderName}' not found. Available Folders: {string.Join(", ", Subfolders.Keys)}");
            }
            
            return folder;
        }

        public IEnumerable<string> EnumerateFolders()
        {
            if (Files.Count != 0)
            {
                yield return Path;
            }

            foreach (string paths in Subfolders.Values.SelectMany(folder => folder.EnumerateFolders()))
            {
                yield return paths;
            }
        }
    
        private static void ProcessFolder(Type type, Folder folder)
        {
            foreach (Type subType in type.GetNestedTypes())
            {
                Folder subFolder = folder.GetOrCreateFolder(subType.Name);
                ProcessFolder(subType, subFolder);
            }

            Type stringType = typeof(string);
            foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            {
                if (field.IsLiteral && !field.IsInitOnly && field.FieldType == stringType)
                {
                    string value = (string)field.GetRawConstantValue();
                    folder.AddFile(field.Name, value);
                }
            }
        }
    }
    
    public class UiState : IPlayerStore
    {
        public ulong PlayerId { get; set; }
        public AssetType Type { get; private set; }
        public string Path { get; private set; }
        public string SelectedAsset { get; set; }
        public int Page { get; set; }
        public Folder CurrentFolder { get; private set; }
        private readonly List<string> RootFolderPaths = new();
        private int FolderIndex = 0;
        public UiCardType? CardType { get; set; }
        
        public UiState(ulong playerId)
        {
            PlayerId = playerId;
            //SetType(AssetType.Texture);
        }
        
        public void SetType(AssetType type)
        {
            _ins.Puts($"{type}");
            Type = type;
            Path = string.Empty;
            RootFolderPaths.Clear();
            FolderIndex = 0;
            OnPathChanged();
            CardType = null;
            if (Type is not AssetType.None and not AssetType.RustIcon and not AssetType.FontAwesomeRegular and not AssetType.FontAwesomeSolid and not AssetType.PlayingCard)
            {
                RootFolderPaths.AddRange(CurrentFolder.EnumerateFolders());
            }
        }

        public void PathUp()
        {
            if (Path == string.Empty)
            {
                SetType(AssetType.None);
                return;
            }
            
            int lastIndex = Math.Max(Path.LastIndexOf('/'), 0);
            Path = Path[..lastIndex];
            OnPathChanged();
            // if (CurrentFolder.Files.Count == 0 && CurrentFolder.Subfolders.Count == 1)
            // {
            //     PathUp();
            // }
        }

        public void PathInto(string path)
        {
            if (Type == AssetType.PlayingCard)
            {
                CardType = Enum.Parse<UiCardType>(path);
            }
            
            Path += $"/{path}";
            OnPathChanged();
            // if (CurrentFolder.Files.Count == 0 && CurrentFolder.Subfolders.Count == 1)
            // {
            //     PathInto(CurrentFolder.Subfolders.Keys.FirstOrDefault());
            // }
        }

        public string GetDisplayPath()
        {
            if (Type == AssetType.None)
            {
                return string.Empty;
            }

            return $"{FastEnumCache<AssetType>.ToString(Type)}/{Path}";
        }

        private void OnPathChanged()
        {
            SelectedAsset = string.Empty;
            Page = 0;
            CurrentFolder = Type switch
            {
                AssetType.Sprite => _ins._sprites.GetFolderFromPath(Path),
                AssetType.Texture => _ins._textures.GetFolderFromPath(Path),
                AssetType.Material => _ins._materials.GetFolderFromPath(Path),
                AssetType.PlayingCard => null,
                AssetType.Item => _ins._items.GetFolderFromPath(Path),
                AssetType.Font => _ins._fonts.GetFolderFromPath(Path),
                //AssetType.RustIcon => UiRustIcons.GetFolder(Path),
                _ => CurrentFolder
            };

            if (CurrentFolder == null || Type is AssetType.RustIcon or AssetType.FontAwesomeRegular or AssetType.FontAwesomeSolid or AssetType.PlayingCard)
            {
                return;
            }
            
            string keys = string.Join(", ", CurrentFolder.Subfolders.Keys.Select(k => $"\"{k}\"-{k.Length}"));
            
            _ins.Puts($"{nameof(OnPathChanged)}: {Path} - {keys}");
            FolderIndex = RootFolderPaths.IndexOf(Path);
        }

        public void NextFolder()
        {
            if (FolderIndex < RootFolderPaths.Count)
            {
                Path = RootFolderPaths[FolderIndex + 1];
                OnPathChanged();
            }
        }
        
        public void PrevFolder()
        {
            if (FolderIndex > 0)
            {
                Path = RootFolderPaths[FolderIndex - 1];
                OnPathChanged();
            }
        }
    }
    #endregion

    #region UI

    private const string UiName = nameof(AssetBrowser) + "UI";

    private readonly UiColor _bodyColor = "#888888C8";
    private readonly UiColor _textColor = "#E4DAD1FF";
    private readonly UiColor _buttonColor = "#83838340";
    private readonly UiColor _pathBarColor = "#1D201F96";
    private readonly UiColor _spriteColor = "#BAB1A8FF";
    private KeyFrameAnimator<UiPosition> _animator;

    private readonly ImageDownloadOptions _downloadOptions = new()
    {
        FallbackImageNameOrUrl = ErrorImage,
    };

    private readonly ImageAnimationOptions _downloadAnimation = new()
    {
        DownloadingImageNameOrUrl = LoadingImage,
        TimeoutImageNameOrUrl = WarningImage,
        FailedImageNameOrUrl = ErrorImage,
        Timeout = TimeSpan.FromSeconds(5)
    };

    public void UiInit()
    {
        _animator = new KeyFrameAnimator<UiPosition>()
            .From(UiPosition.MiddleLeft)
            .AddFrame(10f, new UiPosition(0.25f, 0.75f, 0.25f, 0.75f))
            .AddFrame(20f, UiPosition.TopMiddle)
            .AddFrame(30f, new UiPosition(0.75f, 0.75f, 0.75f, 0.75f))
            .AddFrame(40f, UiPosition.MiddleRight)
            .AddFrame(50f, new UiPosition(0.75f, 0.25f, 0.75f, 0.25f))
            .AddFrame(60f, UiPosition.BottomMiddle)
            .AddFrame(70f, new UiPosition(0.25f, 0.25f, 0.25f, 0.25f))
            .AddFrame(80f, UiPosition.MiddleLeft)
            .AddFrame(90f, UiPosition.MiddleRight)
            .To(UiPosition.MiddleMiddle);
        _store.RegisterStore(this, playerId => new UiState(playerId));
    }

    private void CreateUiTest(BasePlayer player)
    {
        // UiBuilder builder = UiBuilder.Create(this, new UiReference(UiLayer.Overlay, UiName), UiPosition.MiddleMiddle, new UiOffset(-121.31f, 69.21f, 121.31f, 110.79f), _bodyColor);
        //
        // builder.DirectionalLayout(builder.Root, LayoutDirection.Horizontal, 0, TextAnchor.UpperLeft, false, false, false, true, true, true);
        //
        // //var panel = builder.Panel(builder.Root, new UiPosition(0f, 0, 0.1f, 1), default, UiColors.Green);
        // //builder.LayoutElement(panel, 50, 50, 50, 50, 50, 50);
        //
        // var label = builder.Label(builder.Root, new UiPosition(0.1f, 0, 1, 1), default, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris suscipit leo in enim egestas rhoncus. Ut aliquam ante vitae sagittis semper. Cras id vestibulum risus, vel malesuada velit. Cras auctor facilisis purus eget tempor. Suspendisse convallis vehicula tempus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus efficitur risus sed metus pulvinar fringilla. Ut nec placerat ligula. Curabitur faucibus velit eget felis placerat pulvinar. Sed semper pulvinar mi. Maecenas blandit mi mauris, ut ornare tellus finibus sit amet. Vestibulum sed mi sem. Vestibulum ac purus pellentesque, vestibulum tellus id, scelerisque ligula. Sed consequat cursus nisi id fermentum. Suspendisse potenti. Vivamus. ", 14, UiColors.White);
        // label.VerticalOverflow = VerticalWrapMode.Overflow;
        //
        // //builder.DirectionalLayout(label.Item1, LayoutDirection.Vertical, 0, TextAnchor.MiddleCenter, false, true);
        // builder.LayoutElement(label, 250, 100, 250, 100, 250, 1000f);
        //
        // builder.ContentSizeFitter(label, ContentSizeFitter.FitMode.MinSize, ContentSizeFitter.FitMode.PreferredSize);
        // builder.ContentSizeFitter(builder.Root, ContentSizeFitter.FitMode.MinSize, ContentSizeFitter.FitMode.PreferredSize);
        // //builder.ContentSizeFitter(label, ContentSizeFitter.FitMode.PreferredSize, ContentSizeFitter.FitMode.PreferredSize);

        UiBuilder builder = UiBuilder.Create(this, new UiReference(UiLayer.Overlay, UiName), UiPosition.MiddleMiddle, new UiOffset(400, 100), new UiColor(0.1f, 0.1f, 0.1f, 0.8f));
        
        UiPanel rootPanel = builder.Root as UiPanel;
        rootPanel.ImageType = Image.Type.Sliced;
        builder.DirectionalLayout(rootPanel, LayoutDirection.Horizontal, 50f, TextAnchor.MiddleLeft, true, true, true, true, true, true).SetPadding(new UiPadding(10)).SetSpacing(10f);
       // builder.LayoutElement(rootPanel).SetFlexibleHeight(1);
        
        UiImage icon = builder.ImageSprite(rootPanel).SetSprite(UiSprites.Icons.Info).SetColor(UiColors.Blue);
        builder.LayoutElement(icon).SetPreferredWidth(32).SetPreferredHeight(32);
        builder.ContentSizeFitter(icon, ContentSizeFitter.FitMode.PreferredSize, ContentSizeFitter.FitMode.PreferredSize);

        UiLabel label = builder.Label(rootPanel).SetFontSize(18).SetColor(UiColors.White)
            .SetTextValue("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris suscipit leo in enim egestas rhoncus. Ut aliquam ante vitae sagittis semper. Cras id vestibulum risus, vel malesuada velit. Cras auctor facilisis purus eget tempor. Suspendisse convallis vehicula tempus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus efficitur risus sed metus pulvinar fringilla. Ut nec placerat ligula. Curabitur faucibus velit eget felis placerat pulvinar. Sed semper pulvinar mi. Maecenas blandit mi mauris, ut ornare tellus finibus sit amet. Vestibulum sed mi sem. Vestibulum ac purus pellentesque, vestibulum tellus id, scelerisque ligula. Sed consequat cursus nisi id fermentum. Suspendisse potenti. Vivamus. ");
        label.VerticalOverflow = VerticalWrapMode.Overflow;

        builder.LayoutElement(label);
        builder.ContentSizeFitter(label, ContentSizeFitter.FitMode.Unconstrained, ContentSizeFitter.FitMode.PreferredSize);
        
        builder.AddUi(player, new UiDebugOptions("test"));
    }
    
    public void CreateUi(BasePlayer player)
    {
        CreateUi(player, _store.GetOrCreateStore<UiState>(this, player), true);
    }

    private void CreateUi(BasePlayer player, UiState state, bool isInitial = false)
    {
        Puts("A");
        UiBuilder builder;
        if (isInitial)
        {
            //builder = UiBuilder.Create(this, new UiReference(UiLayer.Overlay, UiName), new UiPosition(0.5f, -0.5f, 0.5f, -0.5f), new UiOffset(600, 500), _bodyColor);
            builder = UiBuilder.Create(this, new UiReference(UiLayer.Overlay, UiName), UiPosition.MiddleMiddle, new UiOffset(600, 500), _bodyColor);

            UiPanel panel = builder.Root as UiPanel;

            builder.Animate(panel)
                .AnimateField(static p => p.RectTransform.AsTrackable().Rotation)
                .Lerp(UiRotation.Zero, UiRotation.Full)
                .Duration(5f)
                .Linear()
                .RepeatTiming(5);

            //
            // builder.Animate(panel)
            //     .Duration(5f)
            //     .AnimateField(p => p.RectTransform.AsTrackable().Position)
            //         .Lerp(new UiPosition(0.5f, -0.5f, 0.5f, -0.5f), UiPosition.MiddleMiddle)
            //         .Duration(6f)
            //         .Ease()
            //   ;

            //builder = UiBuilder.Create(this, new UiReference(UiLayer.Overlay, UiName), UiPosition.MiddleMiddle, default, _bodyColor);

            // var pos1= builder.AnimatePosition(builder.Root, UiPosition.MiddleMiddle, 2f, delay: 0f).WithAnimator(_animator);
            //  builder.AnimateOffset(builder.Root, new UiOffset(500, 400), .35f);
            // builder.AnimateColor(builder.Root, _bodyColor.WithAlpha(0f), _bodyColor, .5f);
        }
        else
        {
            //builder = UiBuilder.Create(UiPosition.MiddleMiddle, new UiOffset(500, 400), _bodyColor, UiName);
            builder = UiBuilder.Create(this, new UiReference(UiLayer.Overlay, UiName), UiPosition.MiddleMiddle, new UiOffset(600, 500), _bodyColor);
        }
        Puts("B");
        //builder.SetCurrentFont(UiFontCache.RobotomonoRegular);
        builder.NeedsKeyboard();
        builder.NeedsMouse();

        builder.Border(builder.Root, new UiBorderWidth(-2), UiColors.Green);
        
        UiSection titleBar = builder.Section(builder.Root, new UiPosition(0, 0.95f, 1, 1));
        builder.Label(titleBar, new UiPosition(0.4f, 0, 0.6f, 1), default, "Asset Browser", 14, _textColor);

        UiButton closeButton = builder.Button(titleBar, new UiPosition(0.95f, 0, 1, 1), default, UiColors.Clear, _uiCommands.CloseUi.Build());
        builder.ImageSprite(closeButton, UiPosition.Full, default, UiSprites.Icons.Close, UiColors.Rust.Red);
        
        UiSection pathBar = builder.Section(builder.Root, new UiPosition(0, 0.9f, 1, 0.95f), new UiOffset(1, 1, -1, -1));
        UiPanel pathPanel = builder.Panel(pathBar, new UiPosition(0.1f, 0, .9f, 1), default, _pathBarColor);
        
        Puts("C");
        string path = state.GetDisplayPath();
        Puts("C1");
        if (!string.IsNullOrWhiteSpace(path))
        {
            Puts("C2");
            builder.Label(pathPanel, UiPosition.Full, new UiOffset(2, 0, 0, 0), path,14, _textColor, TextAnchor.MiddleLeft);
        }

        Puts("C3");
        if (state.Type != AssetType.None)
        {
            Puts("C$");
            builder.IconButton(pathBar, new UiPosition(0.00f, 0, 0.05f, 1), default, _spriteColor, Icons.ChevronLeft, _uiCommands.PrevFolder.Build());
            builder.IconButton(pathBar, new UiPosition(0.05f, 0, 0.1f, 1), default, _spriteColor, Icons.ChevronRight, _uiCommands.NextFolder.Build());
        }
        
        Puts("C5");
        builder.SpriteButton(pathBar, new UiPosition(0.90f, 0, 0.95f, 1), default, _spriteColor, UiSprites.Icons.FolderUp, _uiCommands.PathUp.Build());
        
        Puts("C6");
        UiSection body = builder.Section(builder.Root, new UiPosition(0, 0, 1, 0.90f), new UiPadding(2).ToOffset());

        UiRawImage image = builder.ImageStorage(body, UiPosition.Full, default, "http://www.i4ani.com/#/home?1", _downloadOptions);
        builder.AnimateDownload(image, _downloadAnimation);
        
        Puts("D");
        switch (state.Type)
        {
            case AssetType.None:
                CreateSelectType(builder, body, state);
                break;
            case AssetType.Sprite:
            case AssetType.Texture:
                CreateImageBrowser(builder, body, state);
                break;
            case AssetType.Material:
                CreateMaterialBrowser(builder, body, state);
                break;
            case AssetType.PlayingCard:
                CreatePlayingCards(builder, body, state);
                break;
            case AssetType.Item:
                CreateItems(builder, body, state);
                break;
            case AssetType.RustIcon:
                CreateIcons<Icons>(builder, body, state, icons => icons != Icons.None && icons != Icons.FontAwesomeLogoFull);
                break;
            case AssetType.FontAwesomeRegular:
                CreateIcons<FontAwesomeRegularIcons>(builder, body, state, _ => true);
                break;
            case AssetType.FontAwesomeSolid:
                CreateIcons<FontAwesomeSolidIcons>(builder, body, state, _ => true);
                break;
            case AssetType.Font:
                CreateFonts(builder, body, state);
                break;
        }

        Puts("E");
        //builder.ImageStorage(body, UiPosition.Full, default, "https://rust-images.joshdass.dev/rust-icons/61504.png", _downloadOptions);
        
        builder.AddUi(player, new UiDebugOptions("main"));

        if (isInitial)
        {
            timer.In(0.0001f, () => CreateUi(player, state, false));
        }
    }

    //private readonly GridPosition _selectTypeGrid = new GridPositionBuilder(ImageColumns, ImageRows).SetPadding(0.0025f).Build();
    
    private void CreateSelectType(UiBuilder builder, UiReference root, UiState state)
    {
        _imageGrid.Reset();
        foreach (AssetType type in Enum.GetValues(typeof(AssetType)).Cast<AssetType>().Where(at => at != AssetType.None))
        {
            Puts(_uiCommands.SelectAssetType.Build(type));
            UiButton button = builder.Button(root, _imageGrid, default, _buttonColor, _uiCommands.SelectAssetType.Build(type));
            builder.ImageSprite(button, UiPosition.Full, default, UiSprites.Icons.Folder, UiColors.White);
            builder.Label(button, UiPosition.Full, default, FastEnumCache<AssetType>.ToString(type), 14, _textColor).AddOutline(UiColors.Black);
            _imageGrid.MoveCols(1);
        }
    }
    
    private const int ImageColumns = 6;
    private const int ImageRows = 4;
    private const float ImagePadding = 0.01f;
    private const int TotalImages = ImageColumns * ImageRows;
    private readonly UiPadding LayoutPadding = new(2);
    
    private readonly GridPosition _imageGrid = new GridPositionBuilder(ImageColumns, ImageRows).SetPadding(ImagePadding).Build(); 

    private void CreateImageBrowser(UiBuilder builder, UiReference root, UiState state)
    {
        _imageGrid.Reset();
        Folder folder = state.CurrentFolder;
        Puts($"{nameof(CreateImageBrowser)} Folder Path: {folder.Path} Folder Name: {folder.Name}");
        UiScrollView scroll = CreateScrollView(builder, root, _imageGrid, folder);
        
        foreach (string name in folder.Subfolders.Keys)
        {
            UiButton button = builder.SpriteButton(scroll, _imageGrid, default, _buttonColor, UiSprites.Icons.Folder, _uiCommands.PathInto.Build(name));
            builder.Label(button, UiPosition.Full, default, name, 12, _textColor).AddOutline(UiColors.Black);
            _imageGrid.MoveCols(1);
        }
        
        foreach (KeyValuePair<string, string> pair in folder.Files)
        {
            //Puts($"{pair.Key}: {_imageGrid.ToPosition()}");
            UiButton button = builder.Button(scroll, _imageGrid, default, _buttonColor, null);
            var anchor = builder.Anchor(scroll, _imageGrid, default);
            button.SetCommand(_uiCommands.PopoverTest.Build(anchor));
            if (state.Type == AssetType.Sprite)
            {
                UiImage sprite = builder.ImageSprite(button, UiPosition.Full, default, pair.Value, UiColors.White);
                sprite.SetMaterial(UiMaterials.Content.Ui.NameFontMaterial);
            }
            else
            {
                builder.TextureImage(button, UiPosition.Full, default, pair.Value, UiColors.White);
            }
            
            _imageGrid.MoveCols(1);
        }
    }

    private void CreateMaterialBrowser(UiBuilder builder, UiReference root, UiState state)
    {
        _imageGrid.Reset();
        Folder folder = state.CurrentFolder;
        UiScrollView scroll = CreateScrollView(builder, root, _imageGrid, folder);
        
        foreach (string name in folder.Subfolders.Keys)
        {
            UiButton button = builder.SpriteButton(scroll, _imageGrid, default, _buttonColor, UiSprites.Icons.Folder, _uiCommands.PathInto.Build(name));
            //Puts($"{name}: {_imageGrid.ToPosition()}");
            builder.Label(button, UiPosition.Full, default, name, 12, _textColor).AddOutline(UiColors.Black);
            _imageGrid.MoveCols(1);
        }
        
        foreach (KeyValuePair<string, string> pair in folder.Files)
        {
            //Puts($"{pair.Key}: {_imageGrid.ToPosition()}");
            UiButton button = builder.Button(scroll, _imageGrid, default, _buttonColor, _uiCommands.SelectAsset.Build(pair.Value));
            builder.Panel(button, UiPosition.Full, default, UiColors.White).SetMaterial(pair.Value);
            _imageGrid.MoveCols(1);
        }
    }
    
    private void CreatePlayingCards(UiBuilder builder, UiReference root, UiState state)
    {       
        UiScrollView scroll = CreateScrollView(builder, root);
        UiGridLayout grid = builder.GridLayout(scroll, UiPosition.Full, new UiOffset(0, 0, -20, 0), ImageColumns, ImageRows, padding: LayoutPadding);
        grid.ForScrollView(scroll);
        
        if (!state.CardType.HasValue)
        {
            foreach (UiCardType type in FastEnumCache<UiCardType>.GetValues())
            {
                string name = FastEnumCache<UiCardType>.ToString(type);
                UiButton button = builder.SpriteButton(grid, _buttonColor, UiSprites.Icons.Folder, _uiCommands.PathInto.Build(name));
                builder.Label(button, UiPosition.Full, default, name, 12, _textColor).AddOutline(UiColors.Black);
            }
            return;
        }

        foreach (UiSuit suit in FastEnumCache<UiSuit>.GetValues())
        {
            foreach (UiRank rank in FastEnumCache<UiRank>.GetValues())
            {
                PlayingCardData card = new(suit, rank);
                string spritePath = UiPlayingCards.GetPlayingCard(suit, rank, state.CardType.Value);
                UiButton button = builder.Button(grid, _buttonColor,  _uiCommands.SelectAsset.Build(spritePath));
                //var sprite = builder.ImageSprite(button, UiPosition.Full, default, spritePath, UiColors.White);
                UiPlayingCard sprite = builder.PlayingCard(button, UiPosition.Full, default, card, state.CardType.Value);
                //sprite.SetMaterial(UiMaterials.Content.Ui.NameFontMaterial);
            }
        }
    }
     
    private void CreateItems(UiBuilder builder, UiReference root, UiState state)
    {
        _imageGrid.Reset();
        Folder folder = state.CurrentFolder;
        UiScrollView scroll = CreateScrollView(builder, root, _imageGrid, folder);
        
        foreach (string name in folder.Subfolders.Keys)
        {
            UiButton button = builder.SpriteButton(scroll, _imageGrid, default, _buttonColor, UiSprites.Icons.Folder, _uiCommands.PathInto.Build(name));
            //Puts($"{name}: {_imageGrid.ToPosition()}");
            builder.Label(button, UiPosition.Full, default, name, 12, _textColor).AddOutline(UiColors.Black);
            _imageGrid.MoveCols(1);
        }
        
        foreach (KeyValuePair<string, string> pair in folder.Files)
        {
            //Puts($"{pair.Key}: {_imageGrid.ToPosition()}");
            UiButton button = builder.Button(scroll, _imageGrid, default, UiColors.Black, _uiCommands.SelectAsset.Build(pair.Value));
            UiItemIcon icon = builder.ItemIcon(button, UiPosition.Full, default, int.Parse(pair.Value), 0, color: UiColors.White).SetOffsetPadding(new UiPadding(20));
            //icon.SetMaterial(UiMaterials.Content.Ui.NameFontMaterial);
            _imageGrid.MoveCols(1);
        }
    }
    
    private void CreateIcons<T>(UiBuilder builder, UiReference root, UiState state, Func<T, bool> filter) where T : unmanaged, Enum
    {
        UiGridLayout layout =  builder.GridLayout(root, new UiPosition(0, 0.075f, 1, 1), default, ImageColumns, ImageRows, default, LayoutPadding);
        
        IReadOnlyCollection<T> values = EnumCache<T>.GetValues();
        int maxPage = UiHelpers.CalculateMaxPage(values.Count, TotalImages);
        
        Puts($"{values.Count} {TotalImages} {maxPage}");
        
        foreach (T icon in values
                     .Where(filter)
                     .Skip(state.Page * TotalImages)
                     .Take(TotalImages))
        {
            Puts($"{icon} | {icon.GetType().FullName}");
            
            UiButton button = builder.Button(layout, _buttonColor, _uiCommands.SelectAsset.Build($"Rust.UI.Icons.{icon} | {Convert.ToUInt32(icon)}"));
            builder.Icon(button, UiPosition.Full, default, icon);
        }
        
        UiDirectionalLayout paginationLayout = builder.DirectionalLayout(root, new UiPosition(0, 0, 1, 0.075f), default, 15, padding: LayoutPadding, direction: LayoutDirection.Horizontal);
        builder.Paginator(paginationLayout, state.Page, maxPage, 14, _textColor, UiColors.ButtonSecondary, UiColors.ButtonPrimary, _uiCommands.ChangePage);
    }
    
    private readonly GridPosition _fontGrid = new GridPositionBuilder(1, 8).SetPadding(0.01f).Build();
    private readonly GridPosition _characterGrid = new GridPositionBuilder(10, 10).SetPadding(0.001f).Build();
    
    private void CreateFonts(UiBuilder builder, UiReference root, UiState state)
    {
        _fontGrid.Reset();
        Folder folder = state.CurrentFolder;
        UiScrollView scroll = CreateScrollView(builder, root, _characterGrid, folder);
        
        foreach (KeyValuePair<string, string> pair in folder.Files)
        {
            string text = pair.Key;
        
            // if (text == "NotoemojiRegular")
            // {
            //     text += " -  ";
            // }
            
            UiLabel label = builder.Label(scroll, _fontGrid, new UiOffset(2, 2, -2, -2), text, 14, _textColor, _bodyColor, TextAnchor.MiddleLeft);
            label.Text.Font = pair.Value;
            _fontGrid.MoveCols(1);
        }
        
        // foreach (string character in charactersSplit)
        // {
        //     UiLabelBackground label = builder.LabelBackground(scroll, _characterGrid, new UiOffset(2, 2, -2, -2), character, 20, _textColor, _bodyColor, TextAnchor.MiddleCenter);
        //     label.Label.Text.Font = UiFontCache.NotoemojiRegular;
        //     _characterGrid.MoveCols(1);
        // }
    }
    
    private UiScrollView CreateScrollView(UiBuilder builder, UiReference root, GridPosition grid, Folder folder)
    {
        int totalItems = folder.Subfolders.Count + folder.Files.Count;
        return CreateScrollView(builder, root, grid, totalItems);
    }
    
    private UiScrollView CreateScrollView(UiBuilder builder, UiReference root, GridPosition grid, int totalItems)
    {
        UiScrollView scroll = CreateScrollView(builder, root);
        grid.ApplyScrollViewContentVertical(totalItems, scroll);
        return scroll;
    }

    private UiScrollView CreateScrollView(UiBuilder builder, UiReference root)
    {
        UiScrollView scroll = builder.ScrollView(root, UiPosition.Full, default, ScrollRect.MovementType.Clamped, inertia: true, scrollSensitivity: 10f);
        scroll.AddVerticalScrollBar(autoHide: true, handleColor: UiColors.ButtonPrimary, pressedColor: UiColors.ButtonPrimary, highlightColor: UiColors.ButtonPrimary, trackColor: UiColors.PanelSecondary);
        return scroll;
    }
    #endregion

    #region Commands

    [UiCommand]
    [UiProtection(ProtectionType.None)]
    private void CloseCommand(ExecutionData data)
    {
        //AnimationBuilder builder = AnimationBuilder.Create(this);
        
        // builder.AnimatePosition(_animationReference, UiPosition.MiddleMiddle, new UiPosition(0.5f, -0.5f, 0.5f, -0.5f), 5f)
        //     .WithBezierProgressor(new BezierProgressor(.18f,-0.95f,.82f,1f))
        //     .DestroyAfter();
        //
        // builder.AddUi(player);
        
        UiBuilder.DestroyUi(UiName);
    }
    
    [UiCommand]
    [UiProtection(ProtectionType.Advanced)]
    private void SelectAssetType(ExecutionData data, AssetType type)
    {
        UiState state = data.GetStore<UiState>();
        state.SetType(type);
        CreateUi(data.Player, state);
    }
    
    [UiCommand]
    [UiProtection(ProtectionType.Extreme)]
    private void PathUp(ExecutionData data)
    {
        UiState state = data.GetStore<UiState>();
        state.PathUp();
        CreateUi(data.Player, state);
    }
    
    [UiCommand]
    private void NextFolder(ExecutionData data)
    {
        UiState state = data.GetStore<UiState>();
        state.NextFolder();
        CreateUi(data.Player, state);
    }
    
    [UiCommand]
    private void PrevFolder(ExecutionData data)
    {
        UiState state = data.GetStore<UiState>();
        state.PrevFolder();
        CreateUi(data.Player, state);
    }
    
    [UiCommand]
    private void PathInto(ExecutionData data, string path)
    {
        UiState state = data.GetStore<UiState>();
        state.PathInto(path);
        CreateUi(data.Player, state);
    }
    
    [UiCommand]
    private void SelectAsset(ExecutionData data, string text)
    {
        BasePlayer player = data.Player;
        player.SendConsoleCommand($"echo \"{text}\"");
        player.ChatMessage($"\"{text}\"");
        Puts(text);
        
        // UiState state = _playerStates[player.userID];
        // state.SelectedAsset = arg.GetString(0);
        // CreateUi(player, state);
    }
    
    [UiCommand]
    private void PopoverTest(ExecutionData data, UiReference parent)
    {
        BasePlayer player = data.Player;

        string name = $"{parent.Name}_Popover";
        UiBuilder builder = UiBuilder.Create(this, parent.WithChild(name), UiPosition.Full, default);

       //BaseUiComponent anchorElement = builder.Root;

        PopoverPosition position = PopoverPosition.Bottom;
        Vector2 size = new Vector2(100, 200);
        string menuSprite = UiSprites.Content.Ui.UiBackgroundRounded;
        UiColor? outlineColor = null;
        UiColor backgroundColor = UiColors.PanelTertiary;
        
        UiPosition anchor = position switch
        {
            PopoverPosition.Top or PopoverPosition.Left => UiPosition.TopLeft,
            PopoverPosition.Right => UiPosition.TopRight,
            PopoverPosition.Bottom => UiPosition.BottomLeft,
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
        };
        
        UiOffset offset = position switch
        {
            PopoverPosition.Top => new UiOffset(0, 1, 1 + size.x, size.y),
            PopoverPosition.Left => new UiOffset(-size.x, -size.y - 1, 0, -1),
            PopoverPosition.Right => new UiOffset(0, -size.y - 1, size.x, -1),
            PopoverPosition.Bottom => new UiOffset(1, -size.y, 1 + size.x, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
        };
        
        builder.Button(builder.Root, new UiPosition(-10000, -10000, 10000, 10000), default, UiColors.Red.WithAlpha(0.5f), name, ButtonType.Close);
        UiPanel background = builder.Panel(builder.Root, anchor, offset, backgroundColor).SetSprite(menuSprite).SetImageType(Image.Type.Sliced);
        background.AddOutline(outlineColor ?? UiColors.Black.WithAlpha(0.75f));
        builder.OverrideRoot(background);
        
        //UiBuilder builder = UiBuilder.Popover(this, reference,), UiColors.PanelTertiary);
        builder.AddUi(player, new UiDebugOptions("popover"));
    }
    
    [UiCommand]
    private void ChangePage(ExecutionData data, int page)
    {
        UiState state = data.GetStore<UiState>();
        state.Page = page;
        CreateUi(data.Player, state);
    }
    
    [UiCommand]
    private void InputTest(ExecutionData data, InputArg input)
    {
        UiState state = data.GetStore<UiState>();
        CreateUi(data.Player, state);
    }

    private sealed class UiCommandHandler
    {
        public readonly ICommandBuilder CloseUi;
        public readonly ICommandBuilder<AssetType> SelectAssetType;
        public readonly ICommandBuilder PathUp;
        public readonly ICommandBuilder NextFolder;
        public readonly ICommandBuilder PrevFolder;
        public readonly ICommandBuilder<string> PathInto;
        public readonly ICommandBuilder<string> SelectAsset;
        public readonly ICommandBuilder<UiReference> PopoverTest;
        public readonly ICommandBuilder<int> ChangePage;
        public readonly ICommandBuilder<InputArg> InputTest;

        public UiCommandHandler(AssetBrowser plugin)
        {
            (_, CloseUi) = plugin._commands.RegisterCommand(plugin, plugin.CloseCommand);
            (_, SelectAssetType) = plugin._commands.RegisterCommand<AssetType>(plugin, plugin.SelectAssetType);
            (_, PathUp) = plugin._commands.RegisterCommand(plugin, plugin.PathUp);
            (_, NextFolder) = plugin._commands.RegisterCommand(plugin, plugin.NextFolder);
            (_, PrevFolder) = plugin._commands.RegisterCommand(plugin, plugin.PrevFolder);
            (_, PathInto) = plugin._commands.RegisterCommand<string>(plugin, plugin.PathInto);
            (_, SelectAsset) = plugin._commands.RegisterCommand<string>(plugin, plugin.SelectAsset);
            (_, PopoverTest) = plugin._commands.RegisterCommand<UiReference>(plugin, plugin.PopoverTest);
            (_, ChangePage) = plugin._commands.RegisterCommand<int>(plugin, plugin.ChangePage);
            (_, InputTest) = plugin._commands.RegisterCommand<InputArg>(plugin, plugin.InputTest);
        }
    }
    #endregion
}