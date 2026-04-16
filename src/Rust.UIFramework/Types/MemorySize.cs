using System;
using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Types;

[JsonConverter(typeof(MemorySizeConverter))]
public readonly struct MemorySize(ulong bytes)
{
    public readonly ulong Bytes = bytes;

    private static readonly string[] Sizes = ["B", "KB", "MB", "GB"];

    public static MemorySize Parse(string input) => Parse(input.AsSpan());
    public static MemorySize Parse(ReadOnlySpan<char> input) => TryParse(input, out MemorySize result) ? result : throw FormatException.FailedParse<MemorySize>(input);
    public static bool TryParse(string input, out MemorySize rotation) => TryParse(input.AsSpan(), out rotation);
    
    public static bool TryParse(ReadOnlySpan<char> input, out MemorySize size)
    {
        size = default;
        if (input.IsEmptyOrWhitespace)
        {
            return false;
        }
        
        ReadOnlySpan<char> numbers = default;
        ReadOnlySpan<char> type = default;

        for (int i = 0; i < input.Length; i++)
        {
            if (!char.IsDigit(input[i]))
            {
                numbers = input[..i];
                type = input[i..];
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