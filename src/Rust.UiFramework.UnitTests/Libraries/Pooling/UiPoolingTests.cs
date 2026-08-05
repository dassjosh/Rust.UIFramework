using System.Collections.Concurrent;
using System.Reflection;
using System.Text;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Builder.Cached;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using Oxide.Plugins;

namespace Rust.UiFramework.UnitTests.Libraries.Pooling;

public class UiPoolingTests
{
    [Fact]
    private void SamePlugin_Returns_SamePool()
    {
        //Arrange
        PluginId id = PluginId.CreateInternal(nameof(SamePlugin_Returns_SamePool));

        //Act
        UiPluginPool pool1 = Singleton<UiPool>.Instance.GetOrCreate(id);
        UiPluginPool pool2 = Singleton<UiPool>.Instance.GetOrCreate(id);

        //Assert
        Assert.Same(pool1, pool2);

        //Cleanup
        Singleton<UiPool>.Instance.RemovePool(id);
    }

    [Fact]
    private void DifferentPlugin_Returns_DifferentPool()
    {
        //Arrange
        PluginId id1 = PluginId.CreateInternal($"{nameof(DifferentPlugin_Returns_DifferentPool)}_1");
        PluginId id2 = PluginId.CreateInternal($"{nameof(DifferentPlugin_Returns_DifferentPool)}_2");

        //Act
        UiPluginPool pool1 = Singleton<UiPool>.Instance.GetOrCreate(id1);
        UiPluginPool pool2 = Singleton<UiPool>.Instance.GetOrCreate(id2);

        //Assert
        Assert.NotSame(pool1, pool2);

        //Cleanup
        Singleton<UiPool>.Instance.RemovePool(id1);
        Singleton<UiPool>.Instance.RemovePool(id2);
    }

    [Theory]
    [MemberData(nameof(PooledItem_IsReturned_FromPool_TheoryData))]
    private void PooledItem_IsReturned_FromPool(Func<UiPluginPool, object> get, Action<UiPluginPool, object> free)
    {
        //Arrange
        PluginId id = PluginId.CreateInternal(nameof(PooledItem_IsReturned_FromPool));
        UiPluginPool pool = Singleton<UiPool>.Instance.GetOrCreate(id);

        //Act
        object item1 = get(pool);
        free(pool, item1);
        object item2 = get(pool);
        free(pool, item2);

        //Assert
        Assert.Same(item1, item2);
        Assert.False(pool.HasLeaks());

        //Cleanup
        Singleton<UiPool>.Instance.RemovePool(id);
    }

    public static TheoryData<Func<UiPluginPool, object>, Action<UiPluginPool, object>> PooledItem_IsReturned_FromPool_TheoryData() =>
    [
        (pool => pool.GetList<int>(), (pool, item) => pool.FreeList((List<int>)item)),
        (pool => pool.GetConcurrentList<int>(), (pool, item) => pool.FreeConcurrentList((ConcurrentList<int>)item)),
        (pool => pool.GetHashSet<int>(), (pool, item) => pool.FreeHashSet((HashSet<int>)item)),
        (pool => pool.GetConcurrentHashSet<int>(), (pool, item) => pool.FreeConcurrentHashSet((ConcurrentHashSet<int>)item)),
        (pool => pool.GetDictionary<int, string>(), (pool, item) => pool.FreeDictionary((Dictionary<int, string>)item)),
        (pool => pool.GetConcurrentDictionary<int, string>(), (pool, item) => pool.FreeConcurrentDictionary((ConcurrentDictionary<int, string>)item)),
        (pool => pool.GetHash<int, string>(), (pool, item) => pool.FreeHash((Hash<int, string>)item)),
        (pool => pool.GetStringBuilder(), (pool, item) => pool.FreeStringBuilder((StringBuilder)item)),
        (pool => pool.GetMemoryStream(), (pool, item) => pool.FreeMemoryStream((MemoryStream)item)),
    ];

    [Theory]
    [MemberData(nameof(PooledItem_IsReset_ToDefaultState_TheoryData))]
    private void PooledItem_IsReset_ToDefaultState(Func<UiPluginPool, object> get, Action<UiPluginPool, object> free, Action<object> modify, Func<object, bool> validate)
    {
        //Arrange
        PluginId id = PluginId.CreateInternal(nameof(PooledItem_IsReturned_FromPool));
        UiPluginPool pool = Singleton<UiPool>.Instance.GetOrCreate(id);

        //Act
        object item = get(pool);
        modify(item);
        free(pool, item);

        //Assert
        Assert.True(validate(item));
        Assert.False(pool.HasLeaks());

        //Cleanup
        Singleton<UiPool>.Instance.RemovePool(id);
    }

    public static TheoryData<Func<UiPluginPool, object>, Action<UiPluginPool, object>, Action<object>, Func<object, bool>> PooledItem_IsReset_ToDefaultState_TheoryData() =>
    [
        (pool => pool.GetList<int>(), (pool, item) => pool.FreeList((List<int>)item), item => ((List<int>)item).Add(1), item => ((List<int>)item).Count == 0),
        (pool => pool.GetConcurrentList<int>(), (pool, item) => pool.FreeConcurrentList((ConcurrentList<int>)item), item => ((ConcurrentList<int>)item).Add(1), item => ((ConcurrentList<int>)item).Count == 0),
        (pool => pool.GetHashSet<int>(), (pool, item) => pool.FreeHashSet((HashSet<int>)item), item => ((HashSet<int>)item).Add(1), item => ((HashSet<int>)item).Count == 0),
        (pool => pool.GetConcurrentHashSet<int>(), (pool, item) => pool.FreeConcurrentHashSet((ConcurrentHashSet<int>)item), item => ((ConcurrentHashSet<int>)item).Add(1), item => ((ConcurrentHashSet<int>)item).Count == 0),
        (pool => pool.GetDictionary<int, string>(), (pool, item) => pool.FreeDictionary((Dictionary<int, string>)item), item => ((Dictionary<int, string>)item).Add(1, "test"), item => ((Dictionary<int, string>)item).Count == 0),
        (pool => pool.GetConcurrentDictionary<int, string>(), (pool, item) => pool.FreeConcurrentDictionary((ConcurrentDictionary<int, string>)item), item => ((ConcurrentDictionary<int, string>)item).TryAdd(1, "test"), item => ((ConcurrentDictionary<int, string>)item).Count == 0),
        (pool => pool.GetHash<int, string>(), (pool, item) => pool.FreeHash((Hash<int, string>)item), item => ((Hash<int, string>)item).Add(1, "test"), item => ((Hash<int, string>)item).Count == 0),
        (pool => pool.GetStringBuilder(), (pool, item) => pool.FreeStringBuilder((StringBuilder)item), item => ((StringBuilder)item).Append("test"), item => ((StringBuilder)item).Length == 0),
        (pool => pool.GetMemoryStream(), (pool, item) => pool.FreeMemoryStream((MemoryStream)item), item => ((MemoryStream)item).Write([1], 0, 1), item => ((MemoryStream)item).Length == 0),
    ];

    [Theory]
    [MemberData(nameof(BasePoolables))]
    private void BasePoolable_PoolsCorrectly(Type type)
    {
        //Arrange
        PluginId id = PluginId.CreateInternal(nameof(PooledItem_IsReturned_FromPool));
        UiPluginPool pool = Singleton<UiPool>.Instance.GetOrCreate(id);
        MethodInfo get = pool.GetType().GetMethod("Get");
        MethodInfo getGeneric = get.MakeGenericMethod(type);
        MethodInfo free = pool.GetType().GetMethod("Free", BindingFlags.NonPublic | BindingFlags.Instance);
        MethodInfo freeGeneric = free.MakeGenericMethod(type);

        //Act
        object item1 = getGeneric.Invoke(pool, null);
        freeGeneric.Invoke(pool, [item1]);
        object item2 = getGeneric.Invoke(pool, null);
        freeGeneric.Invoke(pool, [item2]);

        //Assert
        Assert.Same(item1, item2);
        Assert.False(pool.HasLeaks());

        //Cleanup
        Singleton<UiPool>.Instance.RemovePool(id);
    }

    public static TheoryData<Type> BasePoolables() => new(GetBasePoolables());

    private static IEnumerable<Type> GetBasePoolables() => GetLoadableTypes(typeof(BasePoolable).Assembly)
        .Where(t => !t.IsAbstract && !t.ContainsGenericParameters && t.IsSubclassOf(typeof(BasePoolable)) && !IgnoredBasePoolables.Contains(t));

    private static readonly List<Type> IgnoredBasePoolables = [typeof(ExecutionData), typeof(CachedUiBuilder), typeof(CachedPieMenu)];

    private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
    {
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException e)
        {
            // Return the types that were successfully loaded
            return e.Types.Where(t => t != null);
        }
    }

    [Fact]
    private void CustomPool_PoolsCorrectly()
    {
        //Arrange
        PluginId id = PluginId.CreateInternal(nameof(CustomPool_PoolsCorrectly));
        UiPluginPool pool = Singleton<UiPool>.Instance.GetOrCreate(id);

        //Act
        MyCustomPool customPool = pool.CustomPool<MyCustomClass, MyCustomPool>();
        MyCustomClass customClass1 = customPool.Get();
        customPool.Free(customClass1);
        MyCustomClass customClass2 = pool.CustomGet<MyCustomClass, MyCustomPool>();
        pool.CustomFree<MyCustomClass, MyCustomPool>(customClass2);


        //Asset
        Assert.Same(customClass1, customClass2);
        Assert.False(pool.HasLeaks());

        //Cleanup
        Singleton<UiPool>.Instance.RemovePool(id);
    }

    private sealed class MyCustomPool() : CustomPool<MyCustomClass, MyCustomPool>(MyCustomPolicy.Instance)
    {

    }
    private sealed class MyCustomClass { }
    private sealed class MyCustomPolicy : IPooledObjectPolicy<MyCustomClass>
    {
        public static readonly MyCustomPolicy Instance = new();

        public int GetPoolSize(PoolSettings settings)
        {
            return 100;
        }

        public MyCustomClass Create()
        {
            return new MyCustomClass();
        }

        public void Get(MyCustomClass obj)
        {

        }

        public bool Return(MyCustomClass obj)
        {
            return true;
        }
    }
}