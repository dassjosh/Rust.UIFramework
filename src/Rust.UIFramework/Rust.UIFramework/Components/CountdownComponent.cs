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
            JsonCreator.AddFieldRaw(writer, JsonDefaults.ComponentTypeName, Type);
            JsonCreator.AddField(writer, JsonDefaults.StartTimeName, StartTime, JsonDefaults.StartTimeValue);
            JsonCreator.AddField(writer, JsonDefaults.EndTimeName, EndTime, JsonDefaults.EndTimeValue);
            JsonCreator.AddField(writer, JsonDefaults.StepName, Step, JsonDefaults.StepValue);
            JsonCreator.AddField(writer, JsonDefaults.CountdownCommandName, Command, JsonDefaults.NullValue);
            writer.WriteEndObject();
        }

        public override void EnterPool()
        {
            StartTime = 0;
            EndTime = 0;
            Step = 0;
            Command = null;
        }
    }
}