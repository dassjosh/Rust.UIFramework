using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Builder.Builders;
using Rust.UiFramework.SourceGenerators.Builder.Extensions;
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
            SymbolCache.Initialize(compilation);
            spc.AddSource($"{classSymbol.Name}.g.cs", GenerateParser(classSymbol));
        });
    }
    
    private string GenerateParser(INamedTypeSymbol classSymbol)
    {
        return new CodeBuilder()
            .Usings(["Oxide.Ext.UiFramework.Extensions", "Cysharp.Threading.Tasks"])
            .Namespace(classSymbol.ContainingNamespace)
            .Add(Enumerable.Range(1, UiCommands.MaxArgs), (args, t) =>
            {
                t.Internal().Class().Name(classSymbol.Name)
                    .AddGenerics(g => g.Generics(Enumerable.Range(0, args)), out GenericsBuilder generics)
                    .AddParameter(p => p.Type(SymbolCache.Instance.Libraries.UiCommands.ICommandParserData.Symbol).Name("data"))
                    .Extends(SymbolCache.Instance.Libraries.UiCommands.BaseCommandParser.Symbol).AddExtendParameter("data")
                    
                    //Run Command Method
                    .Method(m => m.Protected().Override().Void().Name("RunCommandInternal")
                        .AddParameter(p => p.Type(SymbolCache.Instance.Libraries.UiCommands.ExecutionData.Symbol).Name("data"))
                        .AddParameter(p => p.Type(SymbolCache.Instance.Libraries.UiCommands.UiCommandTokenizer.Symbol).Name("args"))
                        .Body(GenerateRunCommandInternalBody(args)))
                
                    .Method(m => m.Private().Void().Name("RunCommandSync")
                        .AddParameter(p => p.Type(SymbolCache.Instance.Libraries.UiCommands.ExecutionData.Symbol).Name("data"))
                        .AddParameters(generics, (generic, p, index) => p.Type(generic).Name($"arg{index}"))
                        .Body(GenerateRunCommandBody(generics, args, SymbolCache.Instance.Action.Symbol, null)))
                    
                    .Method(m => m.Private().Async().Returns(SymbolCache.Instance.UniTask.UniTaskVoid.Symbol).Name("RunCommandAsync")
                    .AddParameter(p => p.Type(SymbolCache.Instance.Libraries.UiCommands.ExecutionData.Symbol).Name("data"))
                    .AddParameters(generics, (generic, p, index) => p.Type(generic).Name($"arg{index}"))
                    .Body(GenerateRunCommandBody(generics, args, SymbolCache.Instance.Func.Symbol, SymbolCache.Instance.UniTask.UniTask.Symbol)));

            }).Build();
    }

    private string GenerateRunCommandInternalBody(int args)
    {
        string parameterString = GetArgString(args);
        
        StringBuilder sb = new();
        sb.AppendLine($"{SymbolCache.Instance.Libraries.UiCommands.ArgReaderIterator.Symbol} iterator = GetReader();");
        for (int i = 0; i < args; i++)
        {
            sb.AppendLine($"T{i} arg{i} = iterator.ParseNext<T{i}>(ref args);");
        }

        sb.AppendLine("switch (Command.Mode)");
        sb.AppendLine("{");
        sb.AppendLine("\tcase ExecutorMode.Void:");
        sb.AppendLine($"\t\tRunCommandSync(data, {parameterString});");
        sb.AppendLine("\t\tbreak;");
        sb.AppendLine("\tcase ExecutorMode.UniTask:");
        sb.AppendLine($"\t\tRunCommandAsync(data, {parameterString}).Forget();");
        sb.AppendLine("\t\tbreak;");
        sb.AppendLine("}");
        
        return sb.ToString();
    }
    
    private string GenerateRunCommandBody(GenericsBuilder generics, int args, INamedTypeSymbol delegateType, INamedTypeSymbol returnType)
    {
        string parameterString = GetArgString(args);
        
        StringBuilder sb = new();

        sb.AppendLine("try");
        sb.AppendLine("{");
        sb.AppendLine($"\t{(returnType is not null ? "await " : null)}(({delegateType.AsGeneric([SymbolCache.Instance.Libraries.UiCommands.ExecutionData.Symbol.ToString(), ..generics, returnType?.ToString()])})Command.Delegate)(data, {parameterString});");
        sb.AppendLine("}");
        sb.AppendLine($"catch ({SymbolCache.Instance.Exception.Symbol} ex)");
        sb.AppendLine("{");
        sb.AppendLine("\tLogException(ex);");
        sb.AppendLine("}");
        sb.AppendLine("finally");
        sb.AppendLine("{");
        sb.AppendLine("\tdata.TryDispose();");
        sb.AppendLine("}");
        
        return sb.ToString();
    }
    
    private static string GetArgString(int args)
    {
        return string.Join(", ", Enumerable.Range(0, args).Select(i => $"arg{i}"));
    }
}
