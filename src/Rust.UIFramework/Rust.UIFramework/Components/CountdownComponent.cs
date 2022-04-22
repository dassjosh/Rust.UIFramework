namespace Oxide.Ext.UiFramework.Components
{
    public class CountdownComponent : BaseComponent
    {
        public const string Type = "Countdown";
        
        public int StartTime;
        public int EndTime;
        public int Step;
        public string Command;

        public override void EnterPool()
        {
            StartTime = 0;
            EndTime = 0;
            Step = 0;
            Command = null;
        }
    }
}