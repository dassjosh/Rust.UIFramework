namespace UI.Framework.Rust.Components
{
    public class InputComponent : BaseTextComponent
    {
        public static string Type = "UnityEngine.UI.InputField";

        public int CharsLimit;
        public string Command;
        public bool IsPassword;

        public override void EnterPool()
        {
            base.EnterPool();
            CharsLimit = 0;
            Command = null;
            IsPassword = false;
        }
    }
}