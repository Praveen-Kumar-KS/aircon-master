using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Aircon.AutoMapper
{
    public class TimeZoneUtcToLocalContextDateTimeResolver : IMemberValueResolver<object, object, DateTime, object>, IMemberValueResolver<object, object, DateTime?, object>
    {
        public object Resolve(object source, object destination, DateTime sourceMember, object destMember, ResolutionContext context)
        {
            sourceMember = DateTime.SpecifyKind(sourceMember, DateTimeKind.Utc);
            string timeZone = string.Empty;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                timeZone = Helper.TimeZone;
            else
                timeZone = TimeZoneConverter.TZConvert.WindowsToIana(Helper.TimeZone);

            var currentUserTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone); // To get this from HttpContextHelper

            return TimeZoneInfo.ConvertTime(sourceMember, currentUserTimeZoneInfo);
        }

        public object Resolve(object source, object destination, DateTime? sourceMember, object destMember, ResolutionContext context)
        {
            if (sourceMember.HasValue)
            {
                return Resolve(source, destination, sourceMember.Value, destMember, context);
            }
            return null;
        }
    }

    public static class Helper
    {
        public static string TimeZone = "Central Standard Time";
        
    }

    public class TimeZoneLocalToUtcContextDateTimeResolver : IMemberValueResolver<object, object, DateTime, object>, IMemberValueResolver<object, object, DateTime?, object>
    {
        public object Resolve(object source, object destination, DateTime sourceMember, object destMember, ResolutionContext context)
        {
            string timeZone = string.Empty;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                timeZone = Helper.TimeZone;
            else
                timeZone = TimeZoneConverter.TZConvert.WindowsToIana(Helper.TimeZone);

            var currentUserTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone); // To get this from HttpContextHelper
            sourceMember = DateTime.SpecifyKind(sourceMember, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTimeToUtc(sourceMember, currentUserTimeZoneInfo);
        }

        public object Resolve(object source, object destination, DateTime? sourceMember, object destMember, ResolutionContext context)
        {
            if (sourceMember.HasValue)
            {
                return Resolve(source, destination, sourceMember.Value, destMember, context);
            }
            return null;
        }
    }
}
