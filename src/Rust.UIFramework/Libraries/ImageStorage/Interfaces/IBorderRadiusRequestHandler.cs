namespace Oxide.Ext.UiFramework.Libraries;

internal interface IBorderRadiusRequestHandler : IRegisterImageRequestHandler
{
    string Name { get; }
    BorderRadiusData Data { get; }
}