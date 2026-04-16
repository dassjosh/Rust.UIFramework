using System.Collections.Generic;
using System.Text;
using Oxide.Ext.UiFramework.Interfaces.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public static class KeyFrameExt
{
    extension<T>(IKeyFrame<T> keyframes)
    {
        public string BuildCssKeyFrames()
        {
            StringBuilder sb = new();
            sb.AppendLine("@keyframes ");
            sb.AppendLine("{");
            foreach (KeyValuePair<float, T> frame in keyframes)
            {
                sb.Append('\t');
                if (frame.Key == 0)
                {
                    sb.Append("from");
                } 
                else if(Mathf.Approximately(frame.Key, 100))
                {
                    sb.Append("to");
                }
                else
                {
                    sb.Append(frame.Key);
                    sb.Append('%');
                }
            
                sb.Append(" { ");
                if (frame.Value is ICssString css)
                {
                    sb.Append(css.ToCssString());
                }
                else
                {
                    sb.Append(frame.Value.ToString());
                }
                sb.AppendLine(" }");
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
    
    
}