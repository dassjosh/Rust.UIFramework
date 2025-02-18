using System;
using System.Reflection;

namespace Oxide.Ext.UiFramework.Types;

internal static class Singleton<T> where T : ISingleton
{
    public static readonly T Instance;
    private const string ErrorMessage = "must have only one constructor that is parameterless and private.";
        
    static Singleton()
    {
        ConstructorInfo[] constructors = typeof(T).GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (constructors.Length != 1)
        {
            throw new InvalidOperationException($"{typeof(T)} {ErrorMessage}");
        }

        ConstructorInfo constructor = constructors[0];
        if (constructor.IsPublic)
        {
            throw new InvalidOperationException($"{typeof(T)} {ErrorMessage}");
        }
            
        try 
        {
            Instance = (T)constructor.Invoke(null);
        }
        catch 
        {
            throw new InvalidOperationException($"{typeof(T)} {ErrorMessage}");
        }
    }
}