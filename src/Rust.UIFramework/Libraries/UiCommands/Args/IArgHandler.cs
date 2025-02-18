namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

public interface IArgHandler : IArgReader, IArgWriter;
public interface IArgHandler<T> : IArgReader<T>, IArgWriter<T>, IArgHandler;