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
    }
}