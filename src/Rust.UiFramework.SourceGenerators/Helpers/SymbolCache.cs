using System;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Rust.UiFramework.SourceGenerators.Helpers
{
    public class SymbolCache
    {
        public readonly SpecialSymbolCache Int32 = new(SpecialType.System_Int32);
        public readonly SpecialSymbolCache String = new(SpecialType.System_String);
        public readonly DotNetSymbolCache Action = new("System.Action");
        public readonly DotNetSymbolCache Func = new("System.Func`1");
        public readonly DotNetSymbolCache Task = new("System.Threading.Tasks.Task");
        public readonly DotNetSymbolCache ValueTask = new("System.Threading.Tasks.ValueTask");

        public readonly LibrariesCache Libraries;
        public readonly PluginsCache Plugins;
        public readonly TypesCache Types;

        public SymbolCache(Compilation compilation)
        {
            Libraries = new LibrariesCache(compilation);
            Plugins = new PluginsCache(compilation);
            Types = new TypesCache(compilation);

            InitializeSymbols(this, compilation);
        }

        private static void InitializeSymbols(object instance, Compilation compilation)
        {
            FieldInfo[] fields = instance.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(instance);
                switch (value)
                {
                    case null:
                        continue;
                    case BaseSymbolCacheData symbolCache:
                        symbolCache.Register(compilation);
                        break;
                    default:
                        // Recursively initialize nested classes
                        InitializeSymbols(value, compilation);
                        break;
                }
            }
        }

        public class LibrariesCache
        {
            public readonly UiCommandsCache UiCommands;

            public LibrariesCache(Compilation compilation)
            {
                UiCommands = new UiCommandsCache(compilation);
                InitializeSymbols(this, compilation);
            }

            public class UiCommandsCache
            {
                public readonly FrameworkSymbolCache ICommandBuilderData = new("Libraries.ICommandBuilderData");
                public readonly FrameworkSymbolCache BaseCommandBuilder = new("Libraries.BaseCommandBuilder");
                public readonly FrameworkSymbolCache ICommandBuilder = new("Libraries.ICommandBuilder");
                public readonly FrameworkSymbolCache CommandBuilder = new("Libraries.CommandBuilder");
                public readonly FrameworkSymbolCache ExecutionData = new("Libraries.ExecutionData");
                public readonly FrameworkSymbolCache UiCommandTokenizer = new("Libraries.UiCommandTokenizer");
                public readonly FrameworkSymbolCache ICommandParserData = new("Libraries.ICommandParserData");
                public readonly FrameworkSymbolCache BaseCommandParser = new("Libraries.BaseCommandParser");
                public readonly FrameworkSymbolCache CommandParser = new("Libraries.CommandParser");
                public readonly FrameworkSymbolCache RegisteredCommand = new("Libraries.RegisteredCommand");
                public readonly FrameworkSymbolCache ArgWriterIterator = new("Libraries.ArgWriterIterator");
                public readonly FrameworkSymbolCache ArgReaderIterator = new("Libraries.ArgReaderIterator");

                public UiCommandsCache(Compilation compilation)
                {
                    InitializeSymbols(this, compilation);
                }
            }
        }

        public class PluginsCache
        {
            public readonly FrameworkSymbolCache IUiFrameworkPlugin = new("Plugins.IUiFrameworkPlugin");

            public PluginsCache(Compilation compilation)
            {
                InitializeSymbols(this, compilation);
            }
        }

        public class TypesCache
        {
            public readonly FrameworkSymbolCache Tracked = new("Types.Tracked`1");
            public readonly FrameworkSymbolCache UiTuple = new("Types.UiTuple`2");
            public readonly FrameworkSymbolCache StaticUiTuple = new("Types.UiTuple");
            public readonly FrameworkSymbolCache Utf8String = new("Types.Utf8String");

            public TypesCache(Compilation compilation)
            {
                InitializeSymbols(this, compilation);
            }
        }

        public abstract class BaseSymbolCacheData
        {
            public INamedTypeSymbol Symbol { get; private set; }

            public void Register(Compilation compilation) => Symbol ??= RegisterInternal(compilation);

            protected abstract INamedTypeSymbol RegisterInternal(Compilation compilation);
        }

        public sealed class FrameworkSymbolCache(string type) : BaseSymbolCacheData
        {
            protected override INamedTypeSymbol RegisterInternal(Compilation compilation) =>
                compilation.GetTypeByMetadataName($"Oxide.Ext.UiFramework.{type}")
                ?? throw new InvalidOperationException($"Failed to find symbol 'Oxide.Ext.UiFramework.{type}'");
        }

        public sealed class SpecialSymbolCache(SpecialType type) : BaseSymbolCacheData
        {
            protected override INamedTypeSymbol RegisterInternal(Compilation compilation) =>
                compilation.GetSpecialType(type);
        }

        public sealed class DotNetSymbolCache : BaseSymbolCacheData
        {
            private readonly string _type;
            public DotNetSymbolCache(string type) => _type = type;

            protected override INamedTypeSymbol RegisterInternal(Compilation compilation) =>
                compilation.GetTypeByMetadataName(_type)
                ?? throw new InvalidOperationException($"Failed to find symbol '{_type}'");
        }
    }
}
