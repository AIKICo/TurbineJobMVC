using System;
using MD.PersianDateTime.Standard;

namespace TurbineJobMVC.Services
{
    public interface IDateTimeService
    {
        PersianDateTime ConvertToPersianDateTime(DateTime? nullableDateTime);
        string ConvertToLongJalaliDateTimeString(DateTime? nullableDateTime);
        string ConvertToShortJalaliDateString(DateTime? nullableDateTime);
    }
}