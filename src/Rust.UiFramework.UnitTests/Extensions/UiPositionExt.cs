using Oxide.Ext.UiFramework.Positions;
using UnityEngine;

namespace Rust.UiFramework.UnitTests.Extensions;

public static class UiPositionExt
{
    public static void ShouldBeInRange(this UiPosition position, Vector2 min, Vector2 max)
    {
        position.Min.ShouldBeInRange(min, max); 
        position.Max.ShouldBeInRange(min, max);
    }
    
    private static void ShouldBeInRange(this Vector2 position, Vector2 min, Vector2 max)
    {
        Math.Round(position.x, 5).Should().BeGreaterThanOrEqualTo(min.x).And.BeLessThanOrEqualTo(max.x);
        Math.Round(position.y, 5).Should().BeGreaterThanOrEqualTo(min.y).And.BeLessThanOrEqualTo(max.y);
    }
}