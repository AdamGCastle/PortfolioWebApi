using System.Security.Claims;

namespace PortfolioWebApi.Extensions
{
    internal static class ExtensionMethods
    {
        internal static string ToStringOrEmpty(this object obj)
        {
            return obj == null ? "" : obj.ToString();
        }

        public static int? GetAccountIdOrNull(this ClaimsPrincipal user)
        {
            if (user?.Identity?.IsAuthenticated == true)
            {
                var userIdentifier = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(userIdentifier, out int accountId))
                {
                    return accountId;
                }
            }

            return null;
        }
    }
}