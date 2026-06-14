using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;

namespace Rust.UiFramework.UnitTests.Libraries.ImageStorage;

public class UiImageStorageTests
{
    [Fact]
    public async Task UiImageStorage_RegisterUrlImage_Success()
    {
        // Arrange
        UiImageStorage storage = Singleton<UiImageStorage>.Instance;

        //Act
        IDownloadImageRequest request = storage.RegisterImage(UnitTestHelpers.Plugin, UiImageDefaults.NotFound);
        CancellationTokenSource cts = new(TimeSpan.FromSeconds(5));
        await WaitForCompletion(request, cts.Token);

        // Assert
        Assert.Equal(ProcessStep.Completed, request.Step);
        Assert.NotEqual(request.ImageId.Id, (uint)0);
        Assert.NotNull(request.Image);
        Assert.Equal(request.Type, UiImageType.Png);
    }

    [Fact]
    public async Task UiImageStorage_RegisterUrlImage_404Url_Failed()
    {
        // Arrange
        UiImageStorage storage = Singleton<UiImageStorage>.Instance;

        //Act
        IDownloadImageRequest request = storage.RegisterImage(UnitTestHelpers.Plugin, $"{UiImageDefaults.NotFound}123");
        CancellationTokenSource cts = new(TimeSpan.FromSeconds(5));
        await WaitForCompletion(request, cts.Token);

        // Assert
        Assert.Equal(ProcessStep.Failed, request.Step);
        Assert.Equal(request.ImageId.Id, (uint)0);
        Assert.Null(request.Image);
        Assert.Equal(request.Type, UiImageType.Unknown);
    }

    [Fact]
    public async Task UiImageStorage_RegisterUrlImage_Success_Callbacks()
    {
        // Arrange
        UiImageStorage storage = Singleton<UiImageStorage>.Instance;

        //Act
        IDownloadImageRequest request = storage.RegisterImage(UnitTestHelpers.Plugin, UiImageDefaults.NotFound);
        bool success = false;
        bool failed = false;
        request.OnSuccess(_ =>
        {
            success = true;
        });
        request.OnFailed(_ =>
        {
            failed = true;
        });
        CancellationTokenSource cts = new(TimeSpan.FromSeconds(5));
        await WaitForCompletion(request, cts.Token);

        // Assert
        Assert.True(success);
        Assert.False(failed);
    }

    [Fact]
    public async Task UiImageStorage_RegisterUrlImage_Failed_Callbacks()
    {
        // Arrange
        UiImageStorage storage = Singleton<UiImageStorage>.Instance;

        //Act
        IDownloadImageRequest request = storage.RegisterImage(UnitTestHelpers.Plugin, $"{UiImageDefaults.NotFound}123");
        bool success = false;
        bool failed = false;
        request.OnSuccess(_ =>
        {
            success = true;
        });
        request.OnFailed(_ =>
        {
            failed = true;
        });
        CancellationTokenSource cts = new(TimeSpan.FromSeconds(5));
        await WaitForCompletion(request, cts.Token);

        // Assert
        Assert.False(success);
        Assert.True(failed);
    }

    [Fact]
    public async Task UiImageStorage_RegisterUrlImage_MultipleRegisters_SameHandler_DifferentRequest()
    {
        // Arrange
        UiImageStorage storage = Singleton<UiImageStorage>.Instance;

        //Act
        IDownloadImageRequest request1 = storage.RegisterImage(UnitTestHelpers.Plugin, UiImageDefaults.NotFound);
        IDownloadImageRequest request2 = storage.RegisterImage(UnitTestHelpers.Plugin, UiImageDefaults.NotFound);

        DownloadImageRequest internalRequest1 = request1 as DownloadImageRequest;
        DownloadImageRequest internalRequest2 = request2 as DownloadImageRequest;

        CancellationTokenSource cts = new(TimeSpan.FromSeconds(5));
        await WaitForCompletion(request2, cts.Token);

        // Assert
        Assert.NotEqual(request1, request2);
        Assert.Equal(internalRequest1.Handler, internalRequest2.Handler);
        Assert.Equal(ProcessStep.Completed, request1.Step);
        Assert.Equal(ProcessStep.Completed, request2.Step);
    }

    private static async Task WaitForCompletion(IDownloadImageRequest register, CancellationToken cancellationToken)
    {
        await Task.Run(() =>
        {
            while (!cancellationToken.IsCancellationRequested && register.Step != ProcessStep.Completed && register.Step != ProcessStep.Failed) { Thread.Sleep(25); }
        }, cancellationToken);
    }
}