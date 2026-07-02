using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Types.Results;

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
        Assert.NotEqual((uint)0, request.ImageId.Id);
        Assert.NotNull(request.Image);
        Assert.Equal(UiImageType.Png, request.Type);
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
        Assert.Equal((uint)0, request.ImageId.Id);
        Assert.Null(request.Image);
        Assert.Equal(UiImageType.Unknown, request.Type);
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
        Assert.Equal(internalRequest1!.Handler, internalRequest2!.Handler);
        Assert.Equal(ProcessStep.Completed, request1.Step);
        Assert.Equal(ProcessStep.Completed, request2.Step);
    }

    [Fact]
    public async Task UiImageStorage_RegisterUrlImage_Success_Async()
    {
        // Arrange
        UiImageStorage storage = Singleton<UiImageStorage>.Instance;

        //Act
        IDownloadImageRequest request = storage.RegisterImage(UnitTestHelpers.Plugin, UiImageDefaults.NotFound);
        ImageId imageId = await request.AsUniTask();

        // Assert
        Assert.Equal(ProcessStep.Completed, request.Step);
        Assert.NotEqual((uint)0, request.ImageId.Id);
        Assert.Equal(imageId.Id, request.ImageId.Id);
        Assert.NotNull(request.Image);
        Assert.Equal(UiImageType.Png, request.Type);
    }

    [Fact]
    public async Task UiImageStorage_RegisterUrlImage_Completed_Success_Async()
    {
        // Arrange
        UiImageStorage storage = Singleton<UiImageStorage>.Instance;

        //Act
        IDownloadImageRequest request = storage.RegisterImage(UnitTestHelpers.Plugin, UiImageDefaults.NotFound);
        Result<ImageId> result = await request.AsUniTask();

        // Assert
        Assert.Equal(ProcessStep.Completed, request.Step);
        Assert.NotEqual((uint)0, request.ImageId.Id);
        Assert.True(result.IsSuccess);
        Assert.Equal(result.Value.Id, request.ImageId.Id);
        Assert.NotNull(request.Image);
        Assert.Equal(UiImageType.Png, request.Type);
    }

    [Fact]
    public async Task UiImageStorage_RegisterUrlImage_404Url_Failed_Async()
    {
        // Arrange
        UiImageStorage storage = Singleton<UiImageStorage>.Instance;

        //Act
        IDownloadImageRequest request = storage.RegisterImage(UnitTestHelpers.Plugin, $"{UiImageDefaults.NotFound}123");
        Result<ImageId> result = await request.AsUniTask();

        // Assert
        Assert.Equal(ProcessStep.Failed, request.Step);
        Assert.Equal((uint)0, request.ImageId.Id);
        Assert.True(result.IsFailure);
        Assert.Equal(result.Value.Id, request.ImageId.Id);
        Assert.Null(request.Image);
        Assert.Equal(UiImageType.Unknown, request.Type);
    }

    [Fact]
    public async Task UiImageStorage_RegisterBorderRadius_Success()
    {
        // Arrange
        UiImageStorage storage = Singleton<UiImageStorage>.Instance;

        //Act
        IRegisterImageRequest request = storage.RegisterBorderRadius(UnitTestHelpers.Plugin, UiSize2D.Size256, new UiBorderRadius(10.Percent()), UiColors.Red, UiColors.Transparent);
        Result<ImageId> result = await request.AsUniTask();

        // Assert
        Assert.Equal(ProcessStep.Completed, request.Step);
        Assert.NotEqual((uint)0, request.ImageId.Id);
        Assert.True(result.IsSuccess);
        Assert.Equal(result.Value.Id, request.ImageId.Id);
        Assert.NotNull(request.Image);
        Assert.Equal(UiImageType.Png, request.Type);
        await Verify(request.Image, "png");
    }

    [Fact]
    public async Task UiImageStorage_RegisterBorderRadius_Border_Success()
    {
        // Arrange
        UiImageStorage storage = Singleton<UiImageStorage>.Instance;

        //Act
        IRegisterImageRequest request = storage.RegisterBorderRadius(UnitTestHelpers.Plugin, UiSize2D.Size512, new UiBorderRadius(10.Percent()), UiColors.Red, UiColors.Transparent,
            enableBorder: true, borderWidth: 1f, borderColor: UiColors.Green);
        Result<ImageId> result = await request.AsUniTask();

        // Assert
        Assert.Equal(ProcessStep.Completed, request.Step);
        Assert.NotEqual((uint)0, request.ImageId.Id);
        Assert.True(result.IsSuccess);
        Assert.Equal(result.Value.Id, request.ImageId.Id);
        Assert.NotNull(request.Image);
        Assert.Equal(UiImageType.Png, request.Type);
        await Verify(request.Image, "png");
    }

    [Fact]
    public async Task UiImageStorage_RegisterBorderRadius_BorderDashed_Success()
    {
        // Arrange
        UiImageStorage storage = Singleton<UiImageStorage>.Instance;

        //Act
        IRegisterImageRequest request = storage.RegisterBorderRadius(UnitTestHelpers.Plugin, UiSize2D.Size512, new UiBorderRadius(10.Percent()), UiColors.Red, UiColors.Transparent,
            enableBorder: true, borderWidth: 1f, borderColor: UiColors.Green,
            enableDashedBorder: true, dashLength: 5f, gapLength: 5f);
        Result<ImageId> result = await request.AsUniTask();

        // Assert
        Assert.Equal(ProcessStep.Completed, request.Step);
        Assert.NotEqual((uint)0, request.ImageId.Id);
        Assert.True(result.IsSuccess);
        Assert.Equal(result.Value.Id, request.ImageId.Id);
        Assert.NotNull(request.Image);
        Assert.Equal(UiImageType.Png, request.Type);
        await Verify(request.Image, "png");
    }

    [Fact]
    public async Task UiImageStorage_RegisterBorderRadius_Url_Success()
    {
        // Arrange
        UiImageStorage storage = Singleton<UiImageStorage>.Instance;

        //Act
        IRegisterImageRequest request = storage.RegisterBorderRadius(UnitTestHelpers.Plugin, UiImageDefaults.Logo, new UiBorderRadius(50.Percent()), null, true, 1f, true, 5f);
        Result<ImageId> result = await request.AsUniTask();

        // Assert
        Assert.Equal(ProcessStep.Completed, request.Step);
        Assert.NotEqual((uint)0, request.ImageId.Id);
        Assert.True(result.IsSuccess);
        Assert.Equal(result.Value.Id, request.ImageId.Id);
        Assert.NotNull(request.Image);
        Assert.Equal(UiImageType.Png, request.Type);
        await Verify(request.Image, "png");
    }

    [Fact]
    public async Task UiImageStorage_GetInvalidImage_ReturnsNotFound()
    {
        // Arrange
        UiImageStorage storage = Singleton<UiImageStorage>.Instance;

        //Act
        IDownloadImageRequest request = storage.RegisterImage(UnitTestHelpers.Plugin, $"{UiImageDefaults.NotFound}123");
        Result<ImageId> result = await request.AsUniTask();

        var image = storage.Get(UnitTestHelpers.Plugin, $"{UiImageDefaults.NotFound}123");

        // Assert
        if (uint.TryParse(image, out uint id))
        {
            Assert.Equal(1503184602u, id);
        }
        else
        {
            Assert.Equal(UiImageDefaults.NotFound, image);
        }
    }

    private static async Task WaitForCompletion(IDownloadImageRequest register, CancellationToken cancellationToken)
    {
        await Task.Run(() =>
        {
            while (!cancellationToken.IsCancellationRequested && register.Step != ProcessStep.Completed && register.Step != ProcessStep.Failed) { Thread.Sleep(25); }
        }, cancellationToken);
    }
}