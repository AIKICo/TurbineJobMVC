using System;
using AutoMapper;
using MD.PersianDateTime.Standard;

namespace TurbineJobMVC.Conventer
{
    public class DateTimeToPersianDateTimeConverter : ITypeConverter<DateTime, string>
    {
        public string Convert(DateTime source, string destination, ResolutionContext context)
        {
            return new PersianDateTime(source).ToLongDateString();
        }
    }
}