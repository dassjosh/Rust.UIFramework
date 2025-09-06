namespace Oxide.Ext.UiFramework.Constants;

public static class RpcFunctions
{
    private const string AddUiFunc = "AddUI";
    private const string DestroyUiFunc = "DestroyUI";
    
    
    public static readonly uint AddUi = StringPool.Get(AddUiFunc);
    public static readonly uint DestroyUi = StringPool.Get(DestroyUiFunc);
}