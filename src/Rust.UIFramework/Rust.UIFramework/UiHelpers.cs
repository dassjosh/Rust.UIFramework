namespace Oxide.Ext.UiFramework
{
    public static class UiHelpers
    {
        public static int CalculateMaxPage(int count, int perPage)
        {
            int maxPage = count / perPage + 1;
            if (count == perPage)
            {
                maxPage -= 1;
            }

            return maxPage;
        }
    }
}