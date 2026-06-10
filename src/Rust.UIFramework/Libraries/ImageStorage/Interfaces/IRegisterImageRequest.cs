namespace Oxide.Ext.UiFramework.Libraries;

public interface IRegisterImageRequest : IBaseRegisterImageRequest
{
    byte[] Image { get; }
    UiImageType Type { get; }
    ImageId ImageId { get; }
}