using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public class CountdownComponent : BaseComponent
    {
        private const string Type = "Countdown";
        
        public int StartTime;
        public int EndTime;
        public int Step;
        public string Command;

        public override void WriteComponent(JsonTextWriter writer)
        {
            writer.WriteStartObject();
            JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
            JsonCreator.AddField(writer, JsonDefaults.Countdown.StartTimeName, StartTime, JsonDefaults.Countdown.StartTimeValue);
            JsonCreator.AddField(writer, JsonDefaults.Countdown.EndTimeName, EndTime, JsonDefaults.Countdown.EndTimeValue);
            JsonCreator.AddField(writer, JsonDefaults.Countdown.StepName, Step, JsonDefaults.Countdown.StepValue);
            JsonCreator.AddField(writer, JsonDefaults.Countdown.CountdownCommandName, Command, JsonDefaults.Common.NullValue);
            writer.WriteEndObject();
        }

        protected override void EnterPool()
        {
            StartTime = 0;
            EndTime = 0;
            Step = 0;
            Command = null;
        }
    }
}