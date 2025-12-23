using System.Collections.Generic;
using Oxide.Ext.UiFramework.Interfaces.Types;

namespace Oxide.Ext.UiFramework.Animation;

public interface IKeyFrame<T> : IEnumerable<KeyValuePair<float, T>>, ICssString;