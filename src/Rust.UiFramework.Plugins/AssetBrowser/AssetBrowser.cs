using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Facepunch.CardGames;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Controls;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Plugins;

[Info("Asset Browser", "MJSU", "1.0.0")]
[Description("Allows browsing Rust assets")]
public class AssetBrowser : RustPlugin
{
    #region Class Fields
    private PluginConfig _pluginConfig;

    private const string UsePermission = "assetbrowser.use";
    private const string AccentColor = "#de8732";

    private static AssetBrowser _ins;

    private readonly Hash<ulong, UiState> _playerStates = new();
    
    private readonly Folder _sprites = Folder.CreateFolders(typeof(UiSprites));
    private readonly Folder _textures = Folder.CreateFolders(typeof(UiTextures));
    private readonly Folder _materials = Folder.CreateFolders(typeof(UiMaterials));
    private readonly Folder _fonts = Folder.CreateFolders(typeof(UiFontCache));
    private readonly Folder _playingCards = new("root", string.Empty, null);
    private readonly Folder _items = new("root", string.Empty, null);
    
    public enum AssetType
    {
        None,
        Sprite,
        Texture,
        Material,
        PlayingCard,
        Item,
        RustIcon,
        Font
    }
    #endregion

    #region Setup & Loading
    private void Init()
    {
        _ins = this;

        permission.RegisterPermission(UsePermission, this);
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
        BasePlayer player = BasePlayer.activePlayerList.FirstOrDefault();
        if (player)
        {
            CreateUi(player);
        }
        
        CreatePlayingCardsFolder(PlayingCardType.Normal);
        CreatePlayingCardsFolder(PlayingCardType.Small);
        CreatePlayingCardsFolder(PlayingCardType.Transparent);
        CreatePlayingCardsFolder(PlayingCardType.Transparent | PlayingCardType.Small);
        CreateItemsFolder();
    }

    private void CreatePlayingCardsFolder(PlayingCardType type)
    {
        string name = type switch
        {
            PlayingCardType.Normal => "Normal",
            PlayingCardType.Small => "Small",
            PlayingCardType.Transparent => "Transparent",
            PlayingCardType.Transparent | PlayingCardType.Small => "Small Transparent",
            _ => null
        };
        
        Folder folder = _playingCards.GetOrCreateFolder(name);
        foreach (Suit suit in Enum.GetValues(typeof(Suit)).Cast<Suit>())
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)).Cast<Rank>())
            {
                folder.AddFile($"{EnumCache<Suit>.ToString(suit)}-{EnumCache<Rank>.ToString(rank)}", UiPlayingCards.GetPlayingCard(suit, rank, type));
            }
        }
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
        UiBuilder.DestroyUi(Uiname);
        _ins = null;
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
        [DefaultValue("DefaultValue")]
        [JsonProperty(PropertyName = "Stub")]
        public string A { get; set; }
            
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

    public Folder(string name, string path, Folder parent)
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
        Folder root = new("", "", null);
        ProcessFolder(type, root);
        return root;
    }

    public Folder GetOrCreateFolder(string name)
    {
        if (!Subfolders.TryGetValue(name, out Folder folder))
        {
            Subfolders[name] = folder = new Folder(name, Path, this);
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
    
    public Folder GetFolder(ReadOnlySpan<char> folder)
    {
        return Subfolders[folder.ToString()];
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
    
    public class UiState
    {
        public AssetType Type { get; private set; }
        public string Path { get; private set; }
        public string SelectedAsset { get; set; }
        public int Page { get; set; }
        public Folder CurrentFolder { get; private set; }
        private readonly List<string> RootFolderPaths = new();
        private int FolderIndex = 0;

        public UiState()
        {
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
            // if (CurrentFolder.Files.Count == 0 && CurrentFolder.Subfolders.Count == 1 && CurrentFolder.Subfolders.ContainsKey("assets"))
            // {
            //     Path = "assets";
            // }
            RootFolderPaths.AddRange(CurrentFolder.EnumerateFolders());
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

            return $"{EnumCache<AssetType>.ToString(Type)}/{Path}";
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
                AssetType.PlayingCard => _ins._playingCards.GetFolderFromPath(Path),
                AssetType.Item => _ins._items.GetFolderFromPath(Path),
                AssetType.Font => _ins._fonts.GetFolderFromPath(Path),
                //AssetType.RustIcon => UiRustIcons.GetFolder(Path),
                _ => CurrentFolder
            };
            
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

    private const string Uiname = nameof(AssetBrowser) + "UI";

    private readonly UiColor _bodyColor = "#888888C8";
    private readonly UiColor _textColor = "#E4DAD1FF";
    private readonly UiColor _buttonColor = "#83838340";
    private readonly UiColor _pathBarColor = "#1D201F96";

    public void CreateUi(BasePlayer player)
    {
        UiState state = new();
        _playerStates[player.userID] = state;
        CreateUi(player, state);
    }

    private void CreateUi(BasePlayer player, UiState state)
    {
        Puts($"{state.Type} {state.Path}");
        UiBuilder builder = UiBuilder.Create(UiPosition.MiddleMiddle, new UiOffset(500, 400), _bodyColor, Uiname);
        builder.NeedsKeyboard();
        builder.NeedsMouse();
        
        UiSection titleBar = builder.Section(builder.Root, new UiPosition(0, 0.95f, 1, 1));
        builder.Label(titleBar, new UiPosition(0.4f, 0, 0.6f, 1), default, "Asset Browser", 14, _textColor);
        UiButton closeButton = builder.CommandButton(titleBar, new UiPosition(0.95f, 0, 1, 1), default, UiColor.Clear, nameof(AssetBrowser_CloseCommand));
        builder.ImageSprite(closeButton, UiPosition.Full, default, UiSprites.Assets.Icons.Close, UiColor.Red);
        
        UiSection pathBar = builder.Section(builder.Root, new UiPosition(0, 0.9f, 1, 0.95f), new UiOffset(1, 1, -1, -1));
        UiPanel pathPanel = builder.Panel(pathBar, new UiPosition(0.1f, 0, .9f, 1), default, _pathBarColor);
        
        string path = state.GetDisplayPath();
        if (!string.IsNullOrWhiteSpace(path))
        {
            builder.Label(pathPanel, UiPosition.Full, new UiOffset(2, 0, 0, 0), path,14, _textColor, TextAnchor.MiddleLeft);
        }

        if (state.Type != AssetType.None)
        {
            builder.ImageSpriteButton(pathBar, new UiPosition(0.00f, 0, 0.05f, 1), default, "#BAB1A8FF", UiSprites.Assets.Icons.DirLeft, nameof(AssetBrowser_PrevFolder));
            builder.ImageSpriteButton(pathBar, new UiPosition(0.05f, 0, 0.1f, 1), default, "#BAB1A8FF", UiSprites.Assets.Icons.ArrowRight, nameof(AssetBrowser_NextFolder));
        }
        
        builder.ImageSpriteButton(pathBar, new UiPosition(0.90f, 0, 0.95f, 1), default, "#BAB1A8FF", UiSprites.Assets.Icons.FolderUp, nameof(AssetBrowser_PathUp));
        
        UiSection body = builder.Section(builder.Root, new UiPosition(0, 0, 1, 0.90f), new UiOffset(2, 2, -2, -2));
        
        switch (state.Type)
        {
            case AssetType.None:
                CreateSelectType(builder, body);
                Puts("A");
                break;
            case AssetType.Sprite:
            case AssetType.Texture:
                CreateImageBrowser(builder, body, state);
                Puts("B");
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
                break;
            case AssetType.Font:
                CreateFonts(builder, body, state);
                break;
        }
        
        string json = builder.GetJsonString();
        
        string dir = Path.Combine(Interface.Oxide.LogDirectory, Name);
        Directory.CreateDirectory(dir);
        
        File.WriteAllText(Path.Combine(dir, "json.json"), json);
        
        Puts("C");
        builder.AddUi(player);
        Puts("D");
    }

    //private readonly GridPosition _selectTypeGrid = new GridPositionBuilder(ImageColumns, ImageRows).SetPadding(0.0025f).Build();
    
    private void CreateSelectType(UiBuilder builder, UiReference root)
    {
        _imageGrid.Reset();
        foreach (AssetType type in Enum.GetValues(typeof(AssetType)).Cast<AssetType>().Where(at => at != AssetType.None))
        {
            UiButton button = builder.CommandButton(root, _imageGrid, default, _buttonColor, $"{nameof(AssetBrowser_SelectAssetType)} {EnumCache<AssetType>.ToString(type)}");
            builder.ImageSprite(button, UiPosition.Full, default, UiSprites.Assets.Icons.Folder, UiColor.White);
            builder.Label(button, UiPosition.Full, default, EnumCache<AssetType>.ToString(type), 14, _textColor).AddElementOutline(UiColor.Black);
            _imageGrid.MoveCols(1);
        }
    }
    
    private readonly GridPosition _imageGrid = new GridPositionBuilder(6, 4).SetPadding(0.01f).Build(); 

    private void CreateImageBrowser(UiBuilder builder, UiReference root, UiState state)
    {
        _imageGrid.Reset();
        Folder folder = state.CurrentFolder;
        Puts($"{nameof(CreateImageBrowser)} Folder Path: {folder.Path} Folder Name: {folder.Name}");
        UiScrollView scroll = CreateScrollView(builder, root, _imageGrid, folder);

        foreach (string name in folder.Subfolders.Keys)
        {
            Puts($"{nameof(CreateImageBrowser)} Subfolder: {name}");
            UiButton button = builder.ImageSpriteButton(scroll, _imageGrid, default, _buttonColor, UiSprites.Assets.Icons.Folder, $"{nameof(AssetBrowser_PathInto)} {name}");
            //Puts($"{name}: {_imageGrid.ToPosition()}");
            builder.Label(button, UiPosition.Full, default, name, 12, _textColor).AddElementOutline(UiColor.Black);
            _imageGrid.MoveCols(1);
        }
        
        foreach (KeyValuePair<string, string> pair in folder.Files)
        {
            //Puts($"{pair.Key}: {_imageGrid.ToPosition()}");
            UiButton button = builder.CommandButton(scroll, _imageGrid, default, _buttonColor, $"{nameof(AssetBrowser_SelectAsset)} \"{pair.Value}\"");
            if (state.Type == AssetType.Sprite)
            {
                UiImage sprite = builder.ImageSprite(button, UiPosition.Full, default, pair.Value, UiColor.White);
                sprite.SetMaterial(UiMaterials.Assets.Content.Ui.Namefontmaterial);
            }
            else
            {
                builder.TextureImage(button, UiPosition.Full, default, pair.Value, UiColor.White);
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
            UiButton button = builder.ImageSpriteButton(scroll, _imageGrid, default, _buttonColor, UiSprites.Assets.Icons.Folder, $"{nameof(AssetBrowser_PathInto)} {name}");
            //Puts($"{name}: {_imageGrid.ToPosition()}");
            builder.Label(button, UiPosition.Full, default, name, 12, _textColor).AddElementOutline(UiColor.Black);
            _imageGrid.MoveCols(1);
        }
        
        foreach (KeyValuePair<string, string> pair in folder.Files)
        {
            //Puts($"{pair.Key}: {_imageGrid.ToPosition()}");
            UiButton button = builder.CommandButton(scroll, _imageGrid, default, _buttonColor, $"{nameof(AssetBrowser_SelectAsset)} \"{pair.Value}\"");
            builder.Panel(button, UiPosition.Full, default, UiColor.White).SetMaterial(pair.Value);
            _imageGrid.MoveCols(1);
        }
    }
    
    private void CreatePlayingCards(UiBuilder builder, UiReference root, UiState state)
    {       
        _imageGrid.Reset();
        Folder folder = state.CurrentFolder;
        UiScrollView scroll = CreateScrollView(builder, root, _imageGrid, folder);
        
        foreach (string name in folder.Subfolders.Keys)
        {
            UiButton button = builder.ImageSpriteButton(scroll, _imageGrid, default, _buttonColor, UiSprites.Assets.Icons.Folder, $"{nameof(AssetBrowser_PathInto)} {name}");
            //Puts($"{name}: {_imageGrid.ToPosition()}");
            builder.Label(button, UiPosition.Full, default, name, 12, _textColor).AddElementOutline(UiColor.Black);
            _imageGrid.MoveCols(1);
        }
        
        foreach (KeyValuePair<string, string> pair in folder.Files)
        {
            //Puts($"{pair.Key}: {_imageGrid.ToPosition()}");
            UiButton button = builder.CommandButton(scroll, _imageGrid, default, _buttonColor, $"{nameof(AssetBrowser_SelectAsset)} \"{pair.Value}\"");
            UiImage sprite = builder.ImageSprite(button, UiPosition.Full, default, pair.Value, UiColor.White);
            sprite.SetMaterial(UiMaterials.Assets.Content.Ui.Namefontmaterial);
            _imageGrid.MoveCols(1);
        }
    }
     
    private void CreateItems(UiBuilder builder, UiReference root, UiState state)
    {
        _imageGrid.Reset();
        Folder folder = state.CurrentFolder;
        UiScrollView scroll = CreateScrollView(builder, root, _imageGrid, folder);
        
        foreach (string name in folder.Subfolders.Keys)
        {
            UiButton button = builder.ImageSpriteButton(scroll, _imageGrid, default, _buttonColor, UiSprites.Assets.Icons.Folder, $"{nameof(AssetBrowser_PathInto)} {name}");
            //Puts($"{name}: {_imageGrid.ToPosition()}");
            builder.Label(button, UiPosition.Full, default, name, 12, _textColor).AddElementOutline(UiColor.Black);
            _imageGrid.MoveCols(1);
        }
        
        foreach (KeyValuePair<string, string> pair in folder.Files)
        {
            //Puts($"{pair.Key}: {_imageGrid.ToPosition()}");
            UiButton button = builder.CommandButton(scroll, _imageGrid, default, _buttonColor, $"{nameof(AssetBrowser_SelectAsset)} \"{pair.Value}\"");
            UiItemIcon icon = builder.ItemIcon(button, UiPosition.Full, default, int.Parse(pair.Value), UiColor.White);
            icon.SetMaterial(UiMaterials.Assets.Content.Ui.Namefontmaterial);
            _imageGrid.MoveCols(1);
        }
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
            
            UiLabelBackground label = builder.LabelBackground(scroll, _fontGrid, new UiOffset(2, 2, -2, -2), text, 14, _textColor, _bodyColor, TextAnchor.MiddleLeft);
            label.Label.Text.Font = pair.Value;
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
        UiScrollView scroll = builder.ScrollView(root, UiPosition.Full, default, false, true, inertia: true, scrollSensitivity: 10f);
        scroll.AddVerticalScrollBar(autoHide: true, handleColor: UiColors.ButtonPrimary, pressedColor: UiColors.ButtonPrimary, highlightColor: UiColors.ButtonPrimary, trackColor: UiColors.PanelSecondary);
        grid.ApplyScrollViewContentVertical(totalItems, scroll);
        return scroll;
    }
    #endregion

    #region Commands

    [ConsoleCommand(nameof(AssetBrowser_CloseCommand))]
    private void AssetBrowser_CloseCommand(ConsoleSystem.Arg arg)
    {
        BasePlayer player = arg.Player();
        if (player)
        {
            UiBuilder.DestroyUi(Uiname);
        }
    }
    
    [ConsoleCommand(nameof(AssetBrowser_SelectAssetType))]
    private void AssetBrowser_SelectAssetType(ConsoleSystem.Arg arg)
    {
        BasePlayer player = arg.Player();
        if (!player)
        {
            return;
        }

        UiState state = _playerStates[player.userID];
        AssetType type = arg.GetEnum<AssetType>(0);
        state.SetType(type);
        
        CreateUi(player, state);
    }
    
    [ConsoleCommand(nameof(AssetBrowser_PathUp))]
    private void AssetBrowser_PathUp(ConsoleSystem.Arg arg)
    {
        BasePlayer player = arg.Player();
        if (!player)
        {
            return;
        }

        UiState state = _playerStates[player.userID];
        state.PathUp();
        CreateUi(player, state);
    }    
    
    [ConsoleCommand(nameof(AssetBrowser_NextFolder))]
    private void AssetBrowser_NextFolder(ConsoleSystem.Arg arg)
    {
        BasePlayer player = arg.Player();
        if (!player)
        {
            return;
        }

        UiState state = _playerStates[player.userID];
        state.NextFolder();
        CreateUi(player, state);
    }
    
    [ConsoleCommand(nameof(AssetBrowser_PrevFolder))]
    private void AssetBrowser_PrevFolder(ConsoleSystem.Arg arg)
    {
        BasePlayer player = arg.Player();
        if (!player)
        {
            return;
        }

        UiState state = _playerStates[player.userID];
        state.PrevFolder();
        CreateUi(player, state);
    }
    
    [ConsoleCommand(nameof(AssetBrowser_PathInto))]
    private void AssetBrowser_PathInto(ConsoleSystem.Arg arg)
    {
        BasePlayer player = arg.Player();
        if (!player)
        {
            return;
        }

        UiState state = _playerStates[player.userID];
        state.PathInto(arg.GetString(0));
        CreateUi(player, state);
    }
    
    [ConsoleCommand(nameof(AssetBrowser_SelectAsset))]
    private void AssetBrowser_SelectAsset(ConsoleSystem.Arg arg)
    {
        BasePlayer player = arg.Player();
        if (!player)
        {
            return;
        }

        string text = string.Join(" ", arg.Args);
        player.SendConsoleCommand($"echo \"{text}\"");
        player.ChatMessage($"\"{text}\"");
        
        // UiState state = _playerStates[player.userID];
        // state.SelectedAsset = arg.GetString(0);
        // CreateUi(player, state);
    }
    #endregion
}