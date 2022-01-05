namespace UI.Framework.Rust.Components
{
    public class OutlineComponent : BaseComponent
    {
        public static string Type = "UnityEngine.UI.Outline";

        public string Distance;
        public bool UseGraphicAlpha;

        public override void EnterPool()
        {
            base.EnterPool();
            Distance = null;
            UseGraphicAlpha = false;
        }
    }
}