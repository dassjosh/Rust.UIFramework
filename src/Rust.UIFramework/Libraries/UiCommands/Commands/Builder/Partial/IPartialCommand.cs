namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

public interface IPartialCommand<in T0> : IBaseCommandBuilder<T0>;

public interface IPartialCommand<in T0, in T1> : IBaseCommandBuilder<T0, T1>;