using System;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public readonly struct StepTiming
{
    private readonly int _steps;
    private readonly StepPosition _position;

    public StepTiming(int steps, StepPosition position = StepPosition.JumpEnd)
    {
        if (steps <= 0) throw new ArgumentException("Steps must be greater than zero.", nameof(steps));
        _steps = steps;
        _position = position;
    }

    public float Evaluate(float t)
    {
        t = Mathf.Clamp01(t);

        return _position switch
        {
            StepPosition.JumpStart => Mathf.Floor(t * _steps) / _steps,
            StepPosition.JumpEnd => Mathf.Ceil(t * _steps) / _steps,
            StepPosition.JumpNone => Mathf.Floor(t * _steps + 0.5f) / _steps,
            StepPosition.JumpBoth => Mathf.Round(t * _steps) / _steps,
            _ => t
        };
    }

    public static implicit operator TimingFunction(StepTiming linear) => linear.Evaluate;
    public TimingFunction ToTiming() => Evaluate;
}
