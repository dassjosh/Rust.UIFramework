using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public class FrozenLinearTiming
{
    private readonly LinearStop[] _stops;
    
    public FrozenLinearTiming(IEnumerable<KeyValuePair<float, float>> stops)
    {
        _stops = stops.Select(s => new LinearStop(s.Key, s.Value)).ToArray();
    }

    public float Evaluate(float t)
    {
        if (_stops.Length == 0) return t;
        if (t <= _stops[0].Position) return _stops[0].Value;
        if (t >= _stops[^1].Position) return _stops[^1].Value;

        float progress = Mathf.Clamp(t * 100, 0, 100);
        for (int i = 0; i < _stops.Length - 1; i++)
        {
            LinearStop current = _stops[i];
            LinearStop next = _stops[i + 1];
            if (progress >= current.Position && progress < next.Position)
            {
                float elapsed = (progress - current.Position) / (next.Position - current.Position);
                return Mathf.Lerp(current.Value, next.Value, elapsed);
            }
        }
        
        return _stops[^1].Value;
    }

    public LinearTiming Copy() => new(_stops.Select(k => new KeyValuePair<float, float>(k.Position, k.Value)));
    
    public static implicit operator TimingFunction(FrozenLinearTiming linear) => linear.Evaluate;
    public TimingFunction ToTiming() => Evaluate;
    
    private readonly struct LinearStop(float position, float value)
    {
        public readonly float Position = position;
        public readonly float Value = value;
    }
}


