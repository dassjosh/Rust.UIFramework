using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

internal static class SingletonBehavior<T> where T : FacepunchBehaviour
{
    public static readonly T Instance = new GameObject($"{UiFrameworkExtension.Instance.Name} {typeof(T).Name}").AddComponent<T>();
}