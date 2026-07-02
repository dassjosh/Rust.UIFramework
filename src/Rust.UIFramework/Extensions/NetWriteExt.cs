using System.Runtime.CompilerServices;
using Network;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Extensions;

internal static class NetWriteExt
{
    extension(NetWrite write)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void BytesWithSizeCustom(byte[] bytes, bool variableLength = false)
        {
            if (bytes.Length > 10485760 /*0xA00000*/)
            {
                write.WriteUInt32(0U, variableLength);
                Debug.LogError($"BytesWithSize: Too big {bytes.Length}");
            }
            else
            {
                write.WriteUInt32((uint) bytes.Length, variableLength);
                write.Write(bytes, 0, bytes.Length);
            }
        }
    }
}