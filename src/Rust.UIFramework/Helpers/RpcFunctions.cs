using System;
using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Helpers;

public static class RpcFunctions
{
    private const string AddUiFunc = "AddUI";
    private const string DestroyUiFunc = "DestroyUI";
    private const string ReceiveFilePngFunc = "CL_ReceiveFilePng";
    
    public static readonly uint AddUi = StringPool.Get(AddUiFunc);
    public static readonly uint DestroyUi = StringPool.Get(DestroyUiFunc);
    public static readonly uint ReceiveFilePng = StringPool.Get(ReceiveFilePngFunc);

    public static void SendAddUi(SendInfo send, JsonFrameworkWriter writer)
    {
        NetWrite write = ClientRPCStart(AddUi);
        if (write != null)
        {
            writer.WriteToNetwork(write);
            write.Send(send);
        }
    }

    public static void SendAddUi(SendInfo send, byte[] bytes)
    {
        NetWrite write = ClientRPCStart(AddUi);
        if (write != null)
        {
            write.BytesWithSizeCustom(bytes);
            write.Send(send);
        }
    }

    public static void SendAddUi(SendInfo send, string json)
    {
        NetWrite write = ClientRPCStart(AddUi);
        if (write != null)
        {
            write.String(json);
            write.Send(send);
        }
    }

    public static void SendDestroyUi(SendInfo send, string name)
    {
        NetWrite write = ClientRPCStart(DestroyUi);
        if (write != null)
        {
            write.String(name);
            write.Send(send);
        }
    }

    public static void SendDestroyUis(SendInfo send, List<string> names)
    {
        NetWrite write = ClientRPCStart(DestroyUi);
        if (write != null)
        {
            //write.String(name); //TODO: Implement once available
            write.Send(send);
        }
    }
    
    public static void SendFilePng(SendInfo send, uint textureId, byte[] bytes)
    {
        NetWrite write = ClientRPCStart(ReceiveFilePng);
        if (write != null)
        {
            write.UInt32(textureId);
            write.BytesWithSizeCustom(bytes);
            write.Send(send);
        }
    }

    public static NetWrite ClientRPCStart(uint funcId)
    {
        if (!Net.sv.IsConnected() || CommunityEntity.ServerInstance.net == null)
        {
            return null;
        }

        NetWrite write = Net.sv.StartWrite();
        write.PacketID(Message.Type.RPCMessage);
        write.EntityID(CommunityEntity.ServerInstance.net.ID);
        write.UInt32(funcId);
        return write;
    }
}