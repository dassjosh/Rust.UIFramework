using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Components
{
    public class CountdownComponent : BasePoolable, IComponent
    {
        private const string Type = "Countdown";
        
        public int StartTime;
        public int EndTime;
        public int Step;
        public string Command;

        public virtual void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            writer.AddField(JsonDefaults.Countdown.StartTimeName, StartTime, JsonDefaults.Countdown.StartTimeValue);
            writer.AddField(JsonDefaults.Countdown.EndTimeName, EndTime, JsonDefaults.Countdown.EndTimeValue);
            writer.AddField(JsonDefaults.Countdown.StepName, Step, JsonDefaults.Countdown.StepValue);
            writer.AddField(JsonDefaults.Countdown.CountdownCommandName, Command, JsonDefaults.Common.NullValue);
            writer.WriteEndObject();
        }

        public virtual void Reset()
        {
            StartTime = JsonDefaults.Countdown.StartTimeValue;
            EndTime = JsonDefaults.Countdown.EndTimeValue;
            Step = JsonDefaults.Countdown.StepValue;
            Command = null;
        }

        protected override void EnterPool()
        {
            Reset();
        }
    }
}