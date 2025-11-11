using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Builder;
using Rust.UiFramework.SourceGenerators.Constants;
using Rust.UiFramework.SourceGenerators.Extensions;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Generators.Commands;

[Generator]
public class CommandParserGenerator : BaseGenerator, IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.Register<ClassDeclarationSyntax>(c => c.Name.Equals("CommandParser") && c.ContainingNamespace.ToString() == "Oxide.Ext.UiFramework.Libraries", (spc, compilation, @class, classSymbol) =>
        {
            InitializeCache(compilation);
            spc.AddSource($"{classSymbol.Name}.g.cs", GenerateParser(classSymbol));
        });
    }
    
    private string GenerateParser(INamedTypeSymbol classSymbol)
    {
        return new CodeBuilder()
            .Usings(["System","System.Threading.Tasks","Oxide.Ext.UiFramework.Extensions"])
            .Namespace(classSymbol.ContainingNamespace)
            .Add(Enumerable.Range(1, UiCommands.MaxArgs), (args, t) =>
            {
                t.Internal().Class().Name(classSymbol.Name)
                    .AddGenerics(g => g.Generics(Enumerable.Range(0, args)), out GenericsBuilder generics)
                    .AddParameter(p => p.Type(SymbolCache.Libraries.UiCommands.ICommandParserData.Symbol).Name("data"))
                    .Extends(SymbolCache.Libraries.UiCommands.BaseCommandParser.Symbol).AddExtendParameter("data")
                    
                    //Run Command Method
                    .Method(m => m.Protected().Override().Void().Name("RunCommandInternal")
                        .AddParameter(p => p.Type(SymbolCache.Libraries.UiCommands.ExecutionData.Symbol).Name("data"))
                        .AddParameter(p => p.Type(SymbolCache.Libraries.UiCommands.UiCommandTokenizer.Symbol).Name("args"))
                        .Body(GenerateBody(generics, args)));

            }).Build();
    }

    private string GenerateBody(GenericsBuilder generics, int args)
    {
        string parameterString = string.Join(", ", Enumerable.Range(0, args).Select(i => $"arg{i}"));
        
        StringBuilder sb = new();
        sb.AppendLine($"{SymbolCache.Libraries.UiCommands.ArgReaderIterator.Symbol} iterator = GetReader();");
        for (int i = 0; i < args; i++)
        {
            sb.AppendLine($"T{i} arg{i} = iterator.ParseNext<T{i}>(ref args);");
        }
        
        sb.AppendLine("switch (Command.Mode)");
        sb.AppendLine("{");
        sb.AppendLine("\tcase ExecutorMode.Void:");
        sb.AppendLine($"\t\ttry");
        sb.AppendLine($"\t\t{{");
        sb.AppendLine($"\t\t\t(({SymbolCache.Action.Symbol.AsGeneric([SymbolCache.Libraries.UiCommands.ExecutionData.Symbol.ToString(), ..generics])})Command.Delegate)(data, {parameterString});");
        sb.AppendLine($"\t\t}}");
        sb.AppendLine($"\t\tcatch (Exception ex)");
        sb.AppendLine($"\t\t{{");
        sb.AppendLine($"\t\t\tLogException(ex);");
        sb.AppendLine($"\t\t}}");
        sb.AppendLine($"\t\tfinally");
        sb.AppendLine($"\t\t{{");
        sb.AppendLine($"\t\t\tdata.TryDispose();");
        sb.AppendLine($"\t\t}}");
        sb.AppendLine("\t\tbreak;");
        sb.AppendLine("\tcase ExecutorMode.Task:");
        sb.AppendLine($"\t\tTaskExt.RunSafely((({SymbolCache.Func.Symbol.AsGeneric([SymbolCache.Libraries.UiCommands.ExecutionData.Symbol.ToString(), ..generics, SymbolCache.Task.Symbol.ToString()])})Command.Delegate)(data, {parameterString}), OnException, data);");
        sb.AppendLine("\t\tbreak;");
        sb.AppendLine("\tcase ExecutorMode.ValueTask:");
        sb.AppendLine($"\t\tTaskExt.RunSafely((({SymbolCache.Func.Symbol.AsGeneric([SymbolCache.Libraries.UiCommands.ExecutionData.Symbol.ToString(), ..generics, SymbolCache.ValueTask.Symbol.ToString()])})Command.Delegate)(data, {parameterString}), OnException, data);");
        sb.AppendLine("\t\tbreak;");
        sb.AppendLine("\tcase ExecutorMode.UniTask:");
        sb.AppendLine("\t\tbreak;");
        sb.AppendLine("}");
        
        return sb.ToString();
    }
}
