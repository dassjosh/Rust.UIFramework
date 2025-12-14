using System.Text;

namespace Rust.UiFramework.SourceGenerators.Builder;

internal static class KeywordExt
{
    extension<T>(T type) where T : IKeywords
    {
        public T Static()
        {
            type.Keywords |= Keywords.Static;
            return type;
        }

        public T Extern()
        {
            type.Keywords |= Keywords.Extern;
            return type;
        }

        public T Unsafe()
        {
            type.Keywords |= Keywords.Unsafe;
            return type;
        }

        public T Abstract()
        {
            type.Keywords |= Keywords.Abstract;
            return type;
        }

        public T Virtual()
        {
            type.Keywords |= Keywords.Virtual;
            return type;
        }

        public T Override()
        {
            type.Keywords |= Keywords.Override;
            return type;
        }

        public T Sealed()
        {
            type.Keywords |= Keywords.Sealed;
            return type;
        }

        public T Async()
        {
            type.Keywords |= Keywords.Async;
            return type;
        }

        public T New()
        {
            type.Keywords |= Keywords.New;
            return type;
        }

        public T Readonly()
        {
            type.Keywords |= Keywords.Readonly;
            return type;
        }

        public T Volatile()
        {
            type.Keywords |= Keywords.Volatile;
            return type;
        }

        public T Const()
        {
            type.Keywords |= Keywords.Const;
            return type;
        }

        public T Event()
        {
            type.Keywords |= Keywords.Event;
            return type;
        }

        public T In()
        {
            type.Keywords |= Keywords.In;
            return type;
        }

        public T Out()
        {
            type.Keywords |= Keywords.Out;
            return type;
        }

        public T Partial()
        {
            type.Keywords |= Keywords.Partial;
            return type;
        }
    }

    public static string GetKeywords(this IKeywords keywords)
    {
        StringBuilder sb = new();
        if (keywords.Keywords.HasFlag(Keywords.Static))
        {
            sb.Append("static ");
        }
        if (keywords.Keywords.HasFlag(Keywords.Extern))
        {
            sb.Append("extern ");
        }
        if (keywords.Keywords.HasFlag(Keywords.Unsafe))
        {
            sb.Append("unsafe ");
        }
        if (keywords.Keywords.HasFlag(Keywords.Abstract))
        {
            sb.Append("abstract ");
        }
        if (keywords.Keywords.HasFlag(Keywords.Virtual))
        {
            sb.Append("virtual ");
        }
        if (keywords.Keywords.HasFlag(Keywords.Override))
        {
            sb.Append("override ");
        }
        if (keywords.Keywords.HasFlag(Keywords.Sealed))
        {
            sb.Append("sealed ");
        }
        if (keywords.Keywords.HasFlag(Keywords.Async))
        {
            sb.Append("async ");
        }
        if (keywords.Keywords.HasFlag(Keywords.New))
        {
            sb.Append("new ");
        }
        if (keywords.Keywords.HasFlag(Keywords.Readonly))
        {
            sb.Append("readonly ");
        }
        if (keywords.Keywords.HasFlag(Keywords.Volatile))
        {
            sb.Append("volatile ");
        }
        if (keywords.Keywords.HasFlag(Keywords.Const))
        {
            sb.Append("const ");
        }
        if (keywords.Keywords.HasFlag(Keywords.Event))
        {
            sb.Append("event ");
        }
        if (keywords.Keywords.HasFlag(Keywords.In))
        {
            sb.Append("in ");
        }
        if (keywords.Keywords.HasFlag(Keywords.Out))
        {
            sb.Append("out ");
        }
        if (keywords.Keywords.HasFlag(Keywords.Partial))
        {
            sb.Append("partial ");
        }
        return sb.ToString();
    }
}