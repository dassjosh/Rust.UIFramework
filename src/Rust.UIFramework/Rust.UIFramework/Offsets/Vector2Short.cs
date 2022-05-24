using System;

namespace Oxide.Ext.UiFramework.Offsets
{
    public struct Vector2Short : IEquatable<Vector2Short>
    {
        public readonly short X;
        public readonly short Y;

        public Vector2Short(short x, short y)
        {
            X = x;
            Y = y;
        }

        public Vector2Short(int x, int y) : this((short)x, (short)y) { }

        public bool Equals(Vector2Short other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector2Short && Equals((Vector2Short)obj);
        }

        public override int GetHashCode()
        {
            return (int)X | (Y << 16);
        }

        public static bool operator ==(Vector2Short lhs, Vector2Short rhs) => lhs.X == rhs.X && lhs.Y == rhs.Y;

        public static bool operator !=(Vector2Short lhs, Vector2Short rhs) => !(lhs == rhs);
        
        public static Vector2Short operator +(Vector2Short a, Vector2Short b) => new Vector2Short(a.X + b.X, a.Y + b.Y);

        public static Vector2Short operator -(Vector2Short a, Vector2Short b) => new Vector2Short(a.X - b.X, a.Y - b.Y);

        public static Vector2Short operator *(Vector2Short a, Vector2Short b) => new Vector2Short(a.X * b.X, a.Y * b.Y);

        public static Vector2Short operator /(Vector2Short a, Vector2Short b) => new Vector2Short(a.X / b.X, a.Y / b.Y);

        public static Vector2Short operator -(Vector2Short a) => new Vector2Short(-a.X, -a.Y);

        public static Vector2Short operator *(Vector2Short a, short d) => new Vector2Short(a.X * d, a.Y * d);

        public static Vector2Short operator *(short d, Vector2Short a) => new Vector2Short(a.X * d, a.Y * d);

        public static Vector2Short operator /(Vector2Short a, short d) => new Vector2Short(a.X / d, a.Y / d);
    }
}