using Cysharp.Threading.Tasks;
using Unity.Jobs;

#if !SERVER
using System.Threading.Tasks;
#endif

namespace Oxide.Ext.UiFramework.Extensions;

public static class JobExt
{
    public static async UniTask RunAsync<T>(
        this T jobData,
        int arrayLength,
        int batchCount,
        JobHandle dependsOn = default(JobHandle))
        where T : struct, IJobParallelFor
    {
#if SERVER
        JobHandle handle = jobData.Schedule(arrayLength, batchCount, dependsOn);
        await handle;
#else
        Parallel.For(0, arrayLength, i => jobData.Execute(i));
#endif
    }
}