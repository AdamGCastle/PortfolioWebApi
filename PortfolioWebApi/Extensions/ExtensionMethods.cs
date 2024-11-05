namespace PortfolioWebApi.Extensions
{
    internal static class ExtensionMethods
    {
        internal static string ToStringOrEmpty(this object obj)
        {
            return obj == null ? "" : obj.ToString();
        }
    }
}