using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal record BorderRadiusImageModifier(RegisterImageRequestHandler Handler, BorderRadiusImageData Data) : IImageModifier
{
    public bool Redirect(ProcessStep step)
    {
        switch (step)
        {
            case ProcessStep.Download:
                Singleton<BorderRadiusImageHandler>.Instance.Enqueue(Handler);
                return true;
            default:
                return false;
        }
    }
}