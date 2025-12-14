using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rust.UiFramework.SourceGenerators.Attributes;
using Rust.UiFramework.SourceGenerators.Builder.Builders;
using Rust.UiFramework.SourceGenerators.Builder.Extensions;
using Rust.UiFramework.SourceGenerators.Constants;
using Rust.UiFramework.SourceGenerators.Enums;
using Rust.UiFramework.SourceGenerators.Extensions;
using Rust.UiFramework.SourceGenerators.Helpers;

namespace Rust.UiFramework.SourceGenerators.Generators.Commands;

[Generator]
public class RegisterCommandsGenerator : BaseGenerator, IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.Register<ClassDeclarationSyntax, GenerateRegisterCommandsAttribute>((spc, compilation, @class, classSymbol, attribute) =>
        {
            SymbolCache.Initialize(compilation);
            spc.AddSource($"{classSymbol.Name}.g.cs", GenerateRegisterCommands(classSymbol));
        });
    }
    
    private string GenerateRegisterCommands(INamedTypeSymbol classSymbol)
    {
        var methods = Enum.GetValues(typeof(ExecutorMode))
            .Cast<ExecutorMode>()
            .Where(mode => mode != ExecutorMode.UniTask)
            .SelectMany(
                _ => Enumerable.Range(0, UiCommands.MaxArgs + 1),
                (mode, arg) => new { mode, arg }
            );
        
        return new CodeBuilder()
            .Usings(["System","System.Threading.Tasks","Oxide.Ext.UiFramework.Extensions", "Oxide.Ext.UiFramework.Types"])
            .Namespace(classSymbol.ContainingNamespace)
            .Add(t => t.Public().Partial().Class().Name("UiCommands")
                .Methods(methods, (d, m) =>
                {
                    m.Public().Name("RegisterCommand").AddGenerics(g => g.Generics(d.arg), out GenericsBuilder generics).Returns(GetReturnType(generics))
                        .AddParameter(p => p.Type(SymbolCache.Instance.Plugins.IUiFrameworkPlugin.Symbol).Name("plugin"))
                        .AddParameter(p => p.Type(GetMethodType(d.mode, generics)).Name("method"))
                        .Body(GenerateBody(generics));
                })
            ).Build();
    }

    private string GetReturnType(GenericsBuilder generics)
    {
        return SymbolCache.Instance.Types.UiTuple.Symbol.AsGeneric([SymbolCache.Instance.Libraries.UiCommands.RegisteredCommand.Symbol.ToString(), SymbolCache.Instance.Libraries.UiCommands.ICommandBuilder.Symbol.AsGeneric(generics)]);
    }
    
    private string GetMethodType(ExecutorMode mode, GenericsBuilder generics)
    {
        switch (mode)
        {
            case ExecutorMode.Void:
                return SymbolCache.Instance.Action.Symbol.AsGeneric([SymbolCache.Instance.Libraries.UiCommands.ExecutionData.Symbol.ToString(), ..generics]);
            case ExecutorMode.Task:
                return SymbolCache.Instance.Func.Symbol.AsGeneric([SymbolCache.Instance.Libraries.UiCommands.ExecutionData.Symbol.ToString(), ..generics, SymbolCache.Instance.Task.Symbol.ToString()]);
            case ExecutorMode.ValueTask:
                return SymbolCache.Instance.Func.Symbol.AsGeneric([SymbolCache.Instance.Libraries.UiCommands.ExecutionData.Symbol.ToString(), ..generics, SymbolCache.Instance.ValueTask.Symbol.ToString()]);
            //case ExecutorMode.UniTask:
               // break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }

    private string GenerateBody(GenericsBuilder generics)
    {
        string argHandlerGenerics = string.Empty;
        if(generics.Count != 0)
        {
            argHandlerGenerics = $"<{generics.Build(0)}>";
        }
        
        StringBuilder sb = new();
        sb.AppendLine($"{SymbolCache.Instance.Libraries.UiCommands.RegisteredCommand.Symbol} command = ParseCommand(plugin, method, ArgCreator.CreateArgHandler{argHandlerGenerics}(plugin.Id()));");
        sb.AppendLine($"_commands[command.Id] = new {SymbolCache.Instance.Libraries.UiCommands.CommandParser.Symbol.AsGeneric(generics)}(command);");
        sb.AppendLine($"{SymbolCache.Instance.Libraries.UiCommands.ICommandBuilder.Symbol.AsGeneric(generics)} builder = new {SymbolCache.Instance.Libraries.UiCommands.CommandBuilder.Symbol.AsGeneric(generics)}(command);");
        sb.AppendLine($"return {SymbolCache.Instance.Types.StaticUiTuple.Symbol}.Create(command, builder);");
        return sb.ToString();
    }
}
