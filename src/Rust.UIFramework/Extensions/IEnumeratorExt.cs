using System;
using System.Collections;
using Oxide.Ext.UiFramework.Logging;

namespace Oxide.Ext.UiFramework.Extensions;

public static class IEnumeratorExt
{
    extension(IEnumerator routine)
    {
        public IEnumerator SafeCoroutine(Action onComplete = null)
        {
            while (true)
            {
                bool moveNext;
                object current = null;
                try
                {
                    moveNext = routine.MoveNext();
                    if (moveNext)
                    {
                        current = routine.Current;
                    }
                }
                catch (Exception ex)
                {
                    UiFrameworkExtension.GlobalLogger.Exception("Coroutine error", ex);
                    break;
                }

                if (!moveNext) break;
                yield return current;
            }

            onComplete?.Invoke();
        }
    }
}