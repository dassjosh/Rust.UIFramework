
using UnityEngine;

namespace Oxide.Ext.UiFramework.Positions
{
    public class StaticUiPosition : UiPosition
    {
        private readonly Position _pos;

        public StaticUiPosition(Vector2 min, Vector2 max)
        {
            _pos = new Position(min, max);
        }
        
        public StaticUiPosition(float xMin, float yMin, float xMax, float yMax)
        {
            _pos = new Position(new Vector2(xMin, yMin), new Vector2(xMax, yMax));
        }

        public override Position ToPosition()
        {
            return _pos;
        }
    }
}