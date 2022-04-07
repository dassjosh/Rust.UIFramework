using System.Collections.Generic;
using System.Text;
using Pool = Facepunch.Pool;

namespace UI.Framework.Rust.Pooling
{
    public static class UiFrameworkPool
    {
        public static List<T> GetList<T>()
        {
            return Pool.GetList<T>() ?? new List<T>();
        }
        
        public static void FreeList<T>(ref List<T> list)
        {
            Pool.FreeList(ref list);
        }

        public static StringBuilder GetStringBuilder()
        {
            return Pool.Get<StringBuilder>() ?? new StringBuilder();
        }
        
        public static void FreeStringBuilder(ref StringBuilder sb)
        {
            sb.Clear();
            Pool.Free(ref sb);
        }
    }
}