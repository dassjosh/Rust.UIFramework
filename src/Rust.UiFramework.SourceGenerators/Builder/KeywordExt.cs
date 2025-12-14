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
        
        public void BuildKeywords(StringBuilder sb)
        {
            if (type.Keywords.HasFlag(Keywords.Static))
            {
                sb.Append("static ");
            }
            if (type.Keywords.HasFlag(Keywords.Extern))
            {
                sb.Append("extern ");
            }
            if (type.Keywords.HasFlag(Keywords.Unsafe))
            {
                sb.Append("unsafe ");
            }
            if (type.Keywords.HasFlag(Keywords.Abstract))
            {
                sb.Append("abstract ");
            }
            if (type.Keywords.HasFlag(Keywords.Virtual))
            {
                sb.Append("virtual ");
            }
            if (type.Keywords.HasFlag(Keywords.Override))
            {
                sb.Append("override ");
            }
            if (type.Keywords.HasFlag(Keywords.Sealed))
            {
                sb.Append("sealed ");
            }
            if (type.Keywords.HasFlag(Keywords.Async))
            {
                sb.Append("async ");
            }
            if (type.Keywords.HasFlag(Keywords.New))
            {
                sb.Append("new ");
            }
            if (type.Keywords.HasFlag(Keywords.Readonly))
            {
                sb.Append("readonly ");
            }
            if (type.Keywords.HasFlag(Keywords.Volatile))
            {
                sb.Append("volatile ");
            }
            if (type.Keywords.HasFlag(Keywords.Const))
            {
                sb.Append("const ");
            }
            if (type.Keywords.HasFlag(Keywords.Event))
            {
                sb.Append("event ");
            }
            if (type.Keywords.HasFlag(Keywords.In))
            {
                sb.Append("in ");
            }
            if (type.Keywords.HasFlag(Keywords.Out))
            {
                sb.Append("out ");
            }
            if (type.Keywords.HasFlag(Keywords.Partial))
            {
                sb.Append("partial ");
            }
        }
    }
}