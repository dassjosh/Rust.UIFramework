using System.Text;

namespace Rust.UiFramework.SourceGenerators.Builder;

internal static class KeywordExt
{
    public static T Static<T>(this T type) where T : IKeywords
    {
        type.Keywords |= Keywords.Static;
        return type;
    }

    public static T Extern<T>(this T type) where T : IKeywords
    {
        type.Keywords |= Keywords.Extern;
        return type;
    }

    public static T Unsafe<T>(this T type) where T : IKeywords
    {
        type.Keywords |= Keywords.Unsafe;
        return type;
    }

    public static T Abstract<T>(this T type) where T : IKeywords
    {
        type.Keywords |= Keywords.Abstract;
        return type;
    }

    public static T Virtual<T>(this T type) where T : IKeywords
    {
        type.Keywords |= Keywords.Virtual;
        return type;
    }

    public static T Override<T>(this T type) where T : IKeywords
    {
        type.Keywords |= Keywords.Override;
        return type;
    }

    public static T Sealed<T>(this T type) where T : IKeywords
    {
        type.Keywords |= Keywords.Sealed;
        return type;
    }

    public static T Async<T>(this T type) where T : IKeywords
    {
        type.Keywords |= Keywords.Async;
        return type;
    }

    public static T New<T>(this T type) where T : IKeywords
    {
        type.Keywords |= Keywords.New;
        return type;
    }

    public static T Readonly<T>(this T type) where T : IKeywords
    {
        type.Keywords |= Keywords.Readonly;
        return type;
    }

    public static T Volatile<T>(this T type) where T : IKeywords
    {
        type.Keywords |= Keywords.Volatile;
        return type;
    }

    public static T Const<T>(this T type) where T : IKeywords
    {
        type.Keywords |= Keywords.Const;
        return type;
    }

    public static T Event<T>(this T type) where T : IKeywords
    {
        type.Keywords |= Keywords.Event;
        return type;
    }

    public static T In<T>(this T type) where T : IKeywords
    {
        type.Keywords |= Keywords.In;
        return type;
    }

    public static T Out<T>(this T type) where T : IKeywords
    {
        type.Keywords |= Keywords.Out;
        return type;
    }
    
    public static T Partial<T>(this T type) where T : IKeywords
    {
        type.Keywords |= Keywords.Partial;
        return type;
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