using UnityEngine;

namespace Oxide.Ext.UiFramework
{
    public static class UiHelpers
    {
        public static int CalculateMaxPage(int count, int perPage)
        {
            int maxPage = count / perPage;
            if (count % perPage == 0)
            {
                maxPage -= 1;
            }

            return maxPage;
        }

        public static int TextOffsetWidth(int length, int fontSize, float padding = 0)
        {
            return Mathf.CeilToInt(length * fontSize * 0.5f + padding * 2) + 1;
            //return (int)(length * fontSize * 1f) + 1;
        }
        
        public static int TextOffsetHeight(int fontSize, float padding = 0)
        {
            return Mathf.CeilToInt(fontSize * 1.25f + padding * 2);
        }
    }
}