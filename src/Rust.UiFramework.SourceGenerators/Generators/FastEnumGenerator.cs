using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Attributes;
using Rust.UiFramework.SourceGenerators.Builder;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Generators;

// [Generator]
// public class FastEnumGenerator : IIncrementalGenerator
// {
//     public void Initialize(IncrementalGeneratorInitializationContext context)
//     {
//         IncrementalValuesProvider<EnumDeclarationSyntax> classDeclarations = context.SyntaxProvider
//             .CreateSyntaxProvider(
//                 predicate: static (s, _) => s is EnumDeclarationSyntax,
//                 transform: static (ctx, _) => (EnumDeclarationSyntax)ctx.Node)
//             .Where(static m => m is not null);
//
//         IncrementalValueProvider<(Compilation Left, ImmutableArray<EnumDeclarationSyntax> Right)> compilationAndClasses = context.CompilationProvider.Combine(classDeclarations.Collect());
//
//         context.RegisterSourceOutput(compilationAndClasses, (spc, source) =>
//         {
//             (Compilation compilation, ImmutableArray<EnumDeclarationSyntax> enums) = source;
//             
//             foreach (EnumDeclarationSyntax @class in enums)
//             {
//                 SemanticModel model = compilation.GetSemanticModel(@class.SyntaxTree);
//                 INamedTypeSymbol enumSymbol = model.GetDeclaredSymbol(@class) as INamedTypeSymbol;
//
//                 AttributeData attribute = enumSymbol?.GetAttribute<FastEnumAttribute>();
//                 if (attribute == null)
//                 {
//                     continue;
//                 }
//                 
//                 GeneratorData data = new(enumSymbol);
//                 spc.AddSource($"{enumSymbol.Name}Ext.g.cs", GenerateEnumExtension(compilation, data));
//             }
//         });
//     }
//
//     private string GenerateEnumExtension(Compilation compilation, GeneratorData genData)
//     {
//         INamedTypeSymbol stringType = compilation.GetSpecialType(SpecialType.System_String);
//         
//         return new CodeBuilder()
//             .Namespace(genData.Enum.ContainingNamespace)
//             .Add(t => t.Public().Static().Class().Name($"{genData.Enum.Name}Ext")
//                 .Type(t => t.Private().Static().Class().Name($"{genData.Enum.Name}Name")
//                     .Fields(genData.Members, (member, field) => field.Public().Static().Readonly().Type(stringType).Name(member.Name).Equals($"nameof({genData.Enum}.{member.Name})")))
//                 
//                 .Type(t => t.Private().Static().Class().Name($"{genData.Enum.Name}Lower")
//                     .Fields(genData.Members, (member, field) => field.Public().Static().Readonly().Type(stringType).Name(member.Name).Equals($"nameof({genData.Enum}.{member.Name}).ToLower()")))
//                 
//                 .Type(t => t.Private().Static().Class().Name($"{genData.Enum.Name}Number")
//                     .Fields(genData.Members, (member, field) => field.Public().Static().Readonly().Type(stringType).Name(member.Name).Equals($"{genData.Enum}.{member.Name}.ToString(\"D\")")))
//                 
//                 //.Fields(genData.Members, (member, field) => field.Private().Static().Type(genData.Utf8String).Name(member.Name).New(field.)))
//                 )
//             .Build();
//     }
//
//     private sealed class GeneratorData
//     {
//         public readonly INamedTypeSymbol Enum;
//         public readonly List<IFieldSymbol> Members;
//         
//         public GeneratorData(INamedTypeSymbol enumSymbol)
//         {
//             Enum = enumSymbol;
//             Members = enumSymbol.GetEnumValues().ToList();
//         }
//     }
// }
