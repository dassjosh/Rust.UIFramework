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
   HorizontalLayout = 107,
   VerticalLayout = 108,
   GridLayout = 109,
   ContentSizeFitter = 110,
   LayoutElement = 111,
   
   ColorBlock = 1000,
   ScrollBar = 1001,
   ScrollViewContent = 1002,
   
   CoreStart = Empty,
   CoreEnd = PlayingCard,
   SubStart = RectTransform,
   SubEnd = LayoutElement,
   ChildStart = ColorBlock,
   ChildEnd = ScrollViewContent,
}