using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

internal interface IBehaviorSingleton;

internal static class SingletonBehavior<T> where T : FacepunchBehaviour, IBehaviorSingleton
{
    public static readonly T Instance;

    static SingletonBehavior()
    {
        GameObject go = new($"{UiFrameworkExtension.Instance.Name} {typeof(T).Name}");
        Instance = go.AddComponent<T>();
        Object.DontDestroyOnLoad(go);
    }
}