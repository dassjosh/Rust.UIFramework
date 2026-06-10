using Oxide.Ext.UiFramework.Threading;

namespace Oxide.Ext.UiFramework.Libraries;

internal interface IUiImageProcessor
{
    ProcessResult Process(RegisterImageRequestHandler request);
}