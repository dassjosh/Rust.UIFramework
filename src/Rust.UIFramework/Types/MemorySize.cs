using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Types;

[JsonConverter(typeof(MemorySizeConverter))]
public readonly struct MemorySize(ulong bytes)
{
    public readonly ulong Bytes = bytes;

    private static readonly string[] Sizes = ["B", "KB", "MB", "GB"];

    public static bool TryParse(string str, out MemorySize size)
    {
        size = default;
        if (string.IsNullOrEmpty(str))
        {
            return false;
        }
        
        ReadOnlySpan<char> numbers = default;
        ReadOnlySpan<char> type = default;

        for (int i = 0; i < str.Length; i++)
        {
            if (!char.IsDigit(str[i]))
            {
                numbers = str[..i];
                type = str[i..];
                break;
            }
        }

        if (numbers.Length == 0)
        {
            return false;
        }

        if (!ulong.TryParse(numbers, out ulong bytes))
        {
            return false;
        }

        ulong multiplier = type switch
        {
            "kb" or "KB" => 1024,
            "mb" or "MB" => 1024 * 1024,
            "gb" or "GB" => 1024 * 1024 * 1024,
            _ => 1
        };

        size = new MemorySize(bytes * multiplier);
        return true;
    }

    public override string ToString()
    {
        ulong size = Bytes;
        int index = 0;
        while (size >= 1024)
        {
            size /= 1024;
            index++;
        }

        return $"{size}{Sizes[index]}";
    }
}