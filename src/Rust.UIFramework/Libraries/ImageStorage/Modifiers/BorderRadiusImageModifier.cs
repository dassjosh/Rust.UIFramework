using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal record BorderRadiusImageModifier(RegisterImageRequestHandler Handler, BorderRadiusData Data) : IImageModifier
{
    public bool Redirect(ProcessStep step)
    {
        switch (step)
        {
            case ProcessStep.Process:
                Singleton<BorderRadiusImageHandler>.Instance.Enqueue(Handler);
                return true;
            default:
                return false;
        }
    }
}