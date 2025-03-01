using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

internal static class SingletonBehavior<T> where T : FacepunchBehaviour
{
    public static readonly T Instance;

    static SingletonBehavior()
    {
        GameObject go = new($"{UiFrameworkExtension.Instance.Name} {typeof(T).Name}");
        Instance = go.AddComponent<T>();
    }
}