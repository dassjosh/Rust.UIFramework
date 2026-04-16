namespace Rust.UiFramework.SourceGenerators.Enums;

public enum ExecutorMode : byte
{
    Void,
    Task,
    ValueTask,
    UniTask //TODO: Add once UniTask is added
}