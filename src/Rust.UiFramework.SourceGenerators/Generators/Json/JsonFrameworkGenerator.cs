using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Attributes;
using Rust.UiFramework.SourceGenerators.Builder.Builders;
using Rust.UiFramework.SourceGenerators.Builder.Extensions;
using Rust.UiFramework.SourceGenerators.Extensions;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Generators.Json;

[Generator]
public class JsonFrameworkGenerator : BaseGenerator, IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.Register<ClassDeclarationSyntax, GenerateJsonWriterAttribute>((spc, compilation, @class, classSymbol, attribute) =>
        {
            SymbolCache.Initialize(compilation);
            GeneratorData data = new();
            spc.AddSource($"{classSymbol.Name}.g.cs", GenerateJsonWriter(classSymbol, data));
        });
    }
    
    private string GenerateJsonWriter(INamedTypeSymbol classSymbol, GeneratorData genData)
    {
        return new CodeBuilder()
            .Namespace(classSymbol.ContainingNamespace)
            .Add(t =>
            {
                t.Public().Partial().Class().Name(classSymbol.Name)

                    .Methods(genData.AddFieldTypes, (type, method) =>
                        method.Public().Void().Name("AddField")
                            .AddParameter(parameter => parameter.Type(SymbolCache.Instance.Types.Utf8String.Symbol).Name("name"))
                            .AddParameter(parameter => parameter.Type(type).Name("value")
                                .If(type.ShouldBePassedByIn(), p => p.In())
                                .EndIf())
                            .Body("""
                                  WritePropertyName(name);
                                  WriteValue(value);
                                  """))
                    
                    .Methods(genData.AddDefaultValueTypes, (type, method) =>
                        method.Public().Void().Name("AddField")
                            .AddParameter(parameter => parameter.Type(SymbolCache.Instance.Types.Utf8String.Symbol).Name("name"))
                            .AddParameter(parameter => parameter.Type(type).Name("value")
                                .If(type.ShouldBePassedByIn(), p => p.In())
                                .EndIf())
                            .AddParameter(parameter => parameter.Type(type).Name("defaultValue")
                                .If(type.ShouldBePassedByIn(), p => p.In())
                                .EndIf())
                            .Body("""
                                  if (value != defaultValue)
                                  {
                                      WritePropertyName(name);
                                      WriteValue(value);
                                  }
                                  """))
                            
                    .Methods(genData.AddTrackedTypes, (type, method) =>
                        method.Public().Void().Name("AddField")
                            .AddParameter(parameter => parameter.Type(SymbolCache.Instance.Types.Utf8String.Symbol).Name("name"))
                            .AddParameter(parameter => parameter.Type(SymbolCache.Instance.Types.Tracked.Symbol.Construct(type)).Name("value"))
                            .AddParameter(parameter => parameter.Type(SymbolCache.Instance.Enums.SerializeMode.Symbol).Name("mode"))
                            .Body("""
                                  if (value.ShouldSerialize(mode))
                                  {
                                      WritePropertyName(name);
                                      WriteValue(value.Value);
                                  }
                                  """));
            }).Build();
    }

    private sealed class GeneratorData
    {
        public readonly INamedTypeSymbol[] CommonFieldTypes;
        public readonly INamedTypeSymbol[] AddFieldTypes;
        public readonly INamedTypeSymbol[] AddDefaultValueTypes;
        public readonly INamedTypeSymbol[] AddTrackedTypes;

        public GeneratorData()
        {
            SymbolCache cache = SymbolCache.Instance;
            CommonFieldTypes = [cache.Byte.Symbol, cache.SByte.Symbol, cache.Char.Symbol, cache.Bool.Symbol, cache.Int16.Symbol, cache.UInt16.Symbol, cache.Int32.Symbol, cache.UInt32.Symbol, cache.Int64.Symbol, 
                cache.UInt64.Symbol, cache.Float.Symbol, cache.String.Symbol, cache.Types.UiColor.Symbol, cache.Types.UiRotation.Symbol, cache.Types.UiBorderWidth.Symbol, cache.Types.UiPadding.Symbol,
                cache.Vector2.Symbol, cache.Vector3.Symbol, cache.Vector4.Symbol, cache.Types.Utf8String.Symbol];
            AddFieldTypes = CommonFieldTypes;
            AddDefaultValueTypes = CommonFieldTypes;
            AddTrackedTypes = CommonFieldTypes.Where(t => !t.Equals(cache.String.Symbol, SymbolEqualityComparer.Default)).ToArray(); // String has special tracked handling
        }
    }
    
}
