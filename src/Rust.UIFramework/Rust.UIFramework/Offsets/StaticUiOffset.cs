using System;

namespace Oxide.Ext.UiFramework.Offsets
{
    public class StaticUiOffset : UiOffset
    {
        private readonly Offset _offset;

        public StaticUiOffset(Vector2Short min, Vector2Short max)
        {
            _offset = new Offset(min, max);
        }
        
        public StaticUiOffset(int width, int height) : this(-width / 2, -height / 2, width, height)
        {
            
        }

        public StaticUiOffset(int x, int y, int width, int height)
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "width cannot be less than 0");
            }
            
            if (height < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "height cannot be less than 0");
            }
            
            _offset = new Offset(new Vector2Short(x, y), new Vector2Short( x + width, y + height));
        }

        public override Offset ToOffset()
        {
            return _offset;
        }
    }
}