namespace Oxide.Ext.UiFramework.Components;

public enum ComponentType : ushort
{
   Empty,
   Text,
   Image,
   RawImage,
   Button,
   Input,
   ScrollView,
   ItemIcon,
   PlayerAvatar,
   NineSlice,
   PlayingCard,
   
   RectTransform = 100,
   NeedsKeyboard = 101,
   NeedsMouse = 102,
   Outline = 103,
   Countdown = 104,
   Draggable = 105,
   Slot = 106,
   DirectionalLayout = 107,
   GridLayout = 108,
   ContentSizeFitter = 109,
   LayoutElement = 110,
   
   ColorBlock = 1000,
   ScrollBar = 1001,
   ScrollViewContent = 1002,
}