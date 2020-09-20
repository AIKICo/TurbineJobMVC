using System;
using MD.PersianDateTime.Standard;

namespace TurbineJobMVC.Services
{
    public class DateTimeService : IDateTimeService
    {
        public string ConvertToLongJalaliDateTimeString(DateTime? nullableDateTime)
        {
            return ConvertToPersianDateTime(nullableDateTime).ToLongDateTimeString();
        }

        public string ConvertToShortJalaliDateString(DateTime? nullableDateTime)
        {
            return ConvertToPersianDateTime(nullableDateTime).ToShortDateString();
        }

        public PersianDateTime ConvertToPersianDateTime(DateTime? nullableDateTime)
        {
            return new PersianDateTime(nullableDateTime);
        }
    }
}