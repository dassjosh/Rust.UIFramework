namespace Oxide.Ext.UiFramework.Helpers;

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

    public static float TextOffsetWidth(int length, int fontSize, float padding = 0)
    {
        return length * fontSize * 0.5f + padding * 2 + 1;
        //return (int)(length * fontSize * 1f) + 1;
    }
        
    public static float TextOffsetHeight(int fontSize, float padding = 0)
    {
        return fontSize * 1.25f + padding * 2;
    }
}