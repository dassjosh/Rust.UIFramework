using System.Runtime.CompilerServices;
using Network;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Extensions;

internal static class NetWriteExt
{
    extension(NetWrite write)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void BytesWithSizeCustom(byte[] bytes)
        {
            write.UInt32((uint) bytes.Length);
            write.BytesCustom(bytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void BytesCustom(byte[] bytes)
        {
            write.Write(bytes, 0, bytes.Length);
        }
    }
}