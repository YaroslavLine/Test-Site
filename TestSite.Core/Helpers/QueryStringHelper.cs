using System.Web;

namespace TestSite.Core.Helpers
{
    public static class QueryStringHelper
    {
        public static int GetIntFromQueryString(HttpRequestBase request, string key, int fallback = 0)
        {
            var stringValue = request.QueryString[key];
            if (stringValue != null && !string.IsNullOrWhiteSpace(stringValue) && int.TryParse(stringValue, out var numericValue))
            {
                return numericValue;
            }
            return fallback;
        }
    }
}