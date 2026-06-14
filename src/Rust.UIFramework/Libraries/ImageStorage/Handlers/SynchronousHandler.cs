using System;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class SynchronousHandler : ISingleton
{
    private SynchronousHandler() { }

    public ProcessResult RunSynchronously(RegisterImageRequestHandler request)
    {
        try
        {
            if (Singleton<DefaultImageProcessor>.Instance.Process(request) != ProcessResult.Success)
            {
                return ProcessResult.Failed;
            }

            if (Singleton<StoreHandler>.Instance.Process(request) != ProcessResult.Success)
            {
                return ProcessResult.Failed;
            }

            Singleton<SaveHandler>.Instance.Process(request);
            return ProcessResult.Success;
        }
        catch (Exception ex)
        {
            request.Failed(new ExceptionEventArgs(ex));
            return ProcessResult.Failed;
        }
    }

    public ProcessResult RunSynchronously(BorderRadiusRequestHandler request)
    {
        try
        {
            if (Singleton<BorderRadiusHandler>.Instance.Process(request) != ProcessResult.Success)
            {
                return ProcessResult.Failed;
            }
        }
        catch (Exception ex)
        {
            request.Failed(new ExceptionEventArgs(ex));
            return ProcessResult.Failed;
        }

        return RunSynchronously((RegisterImageRequestHandler)request);
    }

    public ProcessResult RunBorderRadiusImageSynchronously(RegisterImageRequestHandler request)
    {
        try
        {
            if (Singleton<BorderRadiusImageHandler>.Instance.Process(request) != ProcessResult.Success)
            {
                return ProcessResult.Failed;
            }
        }
        catch (Exception ex)
        {
            request.Failed(new ExceptionEventArgs(ex));
            return ProcessResult.Failed;
        }

        return RunSynchronously(request);
    }
}