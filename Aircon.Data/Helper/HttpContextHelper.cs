using Aircon.Core.Security;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Aircon.Data.Helper
{
    public static class HttpContextHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext Current
        {
            get
            {
                if (_httpContextAccessor != null)
                    return _httpContextAccessor.HttpContext;
                else
                    return null;
            }
        }

        public static int? UserId
        {
            get
            {
                if (_httpContextAccessor != null)
                {
                    var userId =  Current.User.FindFirst(AirconClaimType.UserId);
                    if (userId != null)
                    {
                        return int.Parse(userId.Value);
                    }
                    else
                        return null;
                    
                }
                else
                    return null;
            }
        }

        public static string FullName
        {
            get
            {
                if (_httpContextAccessor != null)
                {
                    var fullName = Current.User.FindFirst(AirconClaimType.FullName);
                    if (fullName != null)
                    {
                        return fullName.Value;
                    }
                    else
                        return string.Empty;

                }
                else
                    return string.Empty;
            }
        }

        public static int? CustomerId
        {
            get
            {
                if (_httpContextAccessor != null)
                {
                    var customerId = Current.User.FindFirst(AirconClaimType.CustomerId);
                    if (customerId != null)
                    {
                        try
                        {
                            var custId = int.Parse(customerId.Value);
                            return custId;
                        }
                        catch {
                            return null;
                        }
                    }
                    else
                        return null;
                    ;
                }
                else
                    return null;
            }
        }
    }
}
