namespace Oxide.Ext.UiFramework.Colors
{
    public static class UiColors
    {
        public static class BootStrap
        {
            public static readonly UiColor Blue = "#007bff";
            public static readonly UiColor Indigo = "#6610f2";
            public static readonly UiColor Purple = "#6f42c1";
            public static readonly UiColor Pink = "#e83e8c";
            public static readonly UiColor Red = "#dc3545";
            public static readonly UiColor Orange = "#fd7e14";
            public static readonly UiColor Yellow = "#ffc107";
            public static readonly UiColor Green = "#28a745";
            public static readonly UiColor Teal = "#20c997";
            public static readonly UiColor Cyan = "#17a2b8";
            public static readonly UiColor White = "#fff";
            public static readonly UiColor Gray = "#6c757d";
            public static readonly UiColor DarkGray = "#343a40";
            public static readonly UiColor Primary = "#007bff";
            public static readonly UiColor Secondary = "#6c757d";
            public static readonly UiColor Success = "#28a745";
            public static readonly UiColor Info = "#17a2b8";
            public static readonly UiColor Warning = "#ffc107";
            public static readonly UiColor Danger = "#dc3545";
            public static readonly UiColor Light = "#f8f9fa";
            public static readonly UiColor Dark = "#343a40";
        }

        public static class Material
        {
            public static readonly UiColor MatPrimary = "#dfe6e9";
            public static readonly UiColor MatPrimaryLight = "#b2bec3";
            public static readonly UiColor MatPrimaryDark = "#636e72";
            public static readonly UiColor MatSecondary = "#2d3436";
            public static readonly UiColor MatSecondaryLight = "#74b9ff";
            public static readonly UiColor MatSecondaryDark = "#0984e3";
            public static readonly UiColor MatTextOnPrimary = "#0984e3";
            public static readonly UiColor MatTextOnSecondary = "#0984e3";
        }

        public static class Supreme
        {
            public static readonly UiColor Lime = "#acfa58";
            public static readonly UiColor SilverText = "#e3e3e3";
            public static readonly UiColor K1lly0usRed = "#ce422b";
        }

        public static class MJSU
        {
            public static readonly UiColor Orange = "#de8732";
        }

        public static class Rust
        {
            public static readonly UiColor Red = "#cd4632";
            public static readonly UiColor Green = "#8cc83c";
            public static readonly UiColor Panel = "#CCCCCC";

            public static class Ui
            {
                public static readonly UiColor Panel = "#A6A6A60F";
                public static readonly UiColor Header = "#DBDBDB33";
                public static readonly UiColor PanelButton = "#A6A6A60F";
                public static readonly UiColor OkButton = "#6A804266";
                public static readonly UiColor Button = "#BFBFBF1A";
                public static readonly UiColor PanelText = "#E8dED4";
                public static readonly UiColor PanelButtonText = "#C4C4C4";
                public static readonly UiColor OkButtonText = "#9BB46E";
                public static readonly UiColor ButtonText = "#E8DED4CC";
            }

            public static class Chat
            {
                public static readonly UiColor Player = "#55AAFF";
                public static readonly UiColor Developer = "#FFAA55";
                public static readonly UiColor Admin = "#AAFF55";
            }

            public static class Steam
            {
                public static readonly UiColor InGame = "#A2DB40";
                public static readonly UiColor Online = "#60CBF2";
                public static readonly UiColor Normal = "#F7EBE1";
            }
        }

        public static class Form
        {
            public static readonly UiColor Body = "#00001F";
            public static readonly UiColor Header = "#00001F";
            public static readonly UiColor Text = UiColor.White;
            public static readonly UiColor Panel = "#2B2B2B";
            public static readonly UiColor PanelSecondary = "#3f3f3f";
            public static readonly UiColor PanelTertiary = "#525252";
            public static readonly UiColor ButtonPrimary = Rust.Red;
            public static readonly UiColor ButtonSecondary = "#666666";
        }

        #region UI Colors
        public static readonly UiColor Body = UiColor.WithAlpha(Form.Body, "B2");
        public static readonly UiColor BodyHeader = Form.Header;
        public static readonly UiColor Text = UiColor.WithAlpha(Form.Text, "80");
        public static readonly UiColor Panel = Form.Panel;
        public static readonly UiColor PanelSecondary = Form.PanelSecondary;
        public static readonly UiColor PanelTertiary = Form.PanelTertiary;
        public static readonly UiColor CloseButton = Form.ButtonPrimary;
        public static readonly UiColor ButtonPrimary = Form.ButtonPrimary;
        public static readonly UiColor ButtonSecondary = Form.ButtonSecondary;
        #endregion
    }
}