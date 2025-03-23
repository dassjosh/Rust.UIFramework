using System;
using System.Reflection;
using Oxide.Core;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;

namespace Oxide.Ext.UiFramework.Types;

public interface ISingleton;

public static class Singleton<T> where T : ISingleton
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
        catch(Exception ex)
        {
            Interface.Oxide.LogException("", ex);
            UiFrameworkExtension.GlobalLogger.Exception("An error occured in Singleton<{0}>", typeof(T).GetRealTypeName(), ex);
            throw;
        }
    }
}