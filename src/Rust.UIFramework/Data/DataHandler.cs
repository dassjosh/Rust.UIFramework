using System;
using System.IO;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Types;
using ProtoBuf;

namespace Oxide.Ext.UiFramework.Data;

internal sealed class DataHandler : ISingleton
{
    private DataHandler() { }

    internal void LoadAll()
    {
        Load<ImageStorageData>(new DataFileInfo("image.storage", 2, DataFormat.Protobuf));
    }

    public void OnServerSave() => SaveAll(false);

    public void Shutdown() => SaveAll(true);

    private void SaveAll(bool force)
    {
        Save(ImageStorageData.Instance, force);
    }
        
    public static TData Load<TData>(DataFileInfo info) where TData : BaseDataFile<TData>, new()
    {
        int index = 0;
        while (true)
        {
            try
            {
                if (index >= info.NumBackups)
                {
                    break;
                }
                    
                string path = info.GetPathForIndex(index);
                if (!File.Exists(path))
                {
                    continue;
                }
                    
                TData data = Deserialize<TData>(path, info.Format);
                if (data != null)
                {
                    BaseDataFile<TData>.Instance = data;
                    data.OnDataLoaded(info);
                    return data;
                }
            }
            catch (Exception ex)
            {
                //TODO Logging
                //DiscordExtension.GlobalLogger.Exception("An error occured loading the {0} Data File of type {1}", info.FilePath, typeof(TData).FullName, ex);
            }
            finally
            {
                index++;
            }
        }

        TData newData = new();
        newData.OnDataLoaded(info);
        BaseDataFile<TData>.Instance = newData;
        return newData;
    }
        
    public static void Save<TData>(TData data, bool force) where TData : BaseDataFile<TData>, new()
    {
        if (data == null || !data.DataUpdated && !force)
        {
            return;
        }
            
        DataFileInfo info = data.FileInfo;

        try
        {
            int numBackups = info.NumBackups;
            string path = info.GetPathForIndex(numBackups);
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            for (int i = numBackups - 1; i >= 0; i--)
            {
                path = info.GetPathForIndex(i);
                if (File.Exists(path))
                {
                    File.Move(path, info.GetPathForIndex(i + 1));
                }
            }

            path = info.GetPathForIndex(0);
            Serialize(path, info.Format, data);
            data.OnDataSaved();
        }
        catch (Exception ex)
        {
            //TODO Logging
            //DiscordExtension.GlobalLogger.Exception("An error occured saving the data file. {0}", typeof(TData).GetRealTypeName(), ex);
        }
    }

    private static void Serialize<TData>(string fileName, DataFormat format, TData data) where TData : BaseDataFile<TData>, new()
    {
        switch (format)
        {
            case DataFormat.Json:
                File.WriteAllText(fileName, JsonConvert.SerializeObject(data));
                break;
            case DataFormat.Protobuf:
                FileMode saveMode = File.Exists(fileName) ? FileMode.Truncate : FileMode.Create;
                using (FileStream file = File.Open(fileName, saveMode))
                {
                    Serializer.Serialize(file, BaseDataFile<TData>.Instance);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(format), format, null);
        }
    }

    private static TData Deserialize<TData>(string fileName, DataFormat format) where TData : BaseDataFile<TData>, new()
    {
        switch (format)
        {
            case DataFormat.Json:
                return JsonConvert.DeserializeObject<TData>(File.ReadAllText(fileName));

            case DataFormat.Protobuf:
            {
                using FileStream file = File.OpenRead(fileName);
                return Serializer.Deserialize<TData>(file);
            }

            default:
                throw new ArgumentOutOfRangeException(nameof(format), format, null);
        }
    }
}