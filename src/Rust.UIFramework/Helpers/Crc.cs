using Ionic.Crc;

namespace Oxide.Ext.UiFramework.Helpers;

internal static class Crc
{
    private static readonly CRC32 CRC = new();
    
    internal static uint GetCRC(byte[] data)
    {
        CRC.Reset();
        CRC.SlurpBlock(data, 0, data.Length);
        CRC.UpdateCRC((byte) FileStorage.Type.png);
        return (uint)CRC.Crc32Result;
    }
}