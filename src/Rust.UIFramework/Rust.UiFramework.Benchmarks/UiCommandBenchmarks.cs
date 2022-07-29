using BenchmarkDotNet.Attributes;
using Oxide.Ext.UiFramework.Commands;

namespace Rust.UiFramework.Benchmarks
{
    [MemoryDiagnoser]
    public class UiCommandBenchmarks
    {
        private const string CommandName = "UiCommandBenchmarksCommand";
        private const string CommandName2 = "UiCommandBenchmarksCommand";
        private const string CommandName3 = "UiCommandBenchmarksCommand";
        
        [GlobalSetup]
        public void Setup()
        {
            
        }
        
        [Benchmark]
        public string CreateSingleStringCommand()
        {
            return CommandName;
        }
        
        [Benchmark]
        public UiCommand CreateSingleUiCommand()
        {
            UiCommand command = UiCommand.Create(CommandName);
            command.Dispose();
            return command;
        }
        
        [Benchmark]
        public string CreateMultiStringCommand()
        {
            return $"{CommandName} {24} {56f} {92.5} {nameof(CommandName)}";
        }
        
        [Benchmark]
        public UiCommand CreateMultiUiCommand()
        {
            UiCommand command = UiCommand.Create(CommandName, 24, 56f, 92.5, nameof(CommandName));
            command.Dispose();
            return command;
        }
        
        [Benchmark]
        public string CreateLargeMultiStringCommand()
        {
            return $"{CommandName} {24} {56f} {92.5} {nameof(CommandName)} {25} {57f} {93.5} {nameof(CommandName2)} {26} {57} {93.5} {nameof(CommandName3)} {26} {57f}";
        }
        
        [Benchmark]
        public UiCommand CreateLargeMultiUiCommand()
        {
            UiCommand command = UiCommand.Create(CommandName, 24, 56f, 92.5, nameof(CommandName2), 25, 57f, 93.5, nameof(CommandName3), 26, 57f);
            command.Dispose();
            return command;
        }
    }
}