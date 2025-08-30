using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

internal static class SingletonBehavior<T> where T : FacepunchBehaviour
{
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private static readonly GameObject _go;
    public static readonly T Instance;

    static SingletonBehavior()
    {
        _go = new GameObject($"{UiFrameworkExtension.Instance.Name} {typeof(T).Name}");
        Instance = _go.AddComponent<T>();
        Object.DontDestroyOnLoad(_go);
    }
}