using System;

namespace Books.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dob)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var age = today.Year - dob.Year;

            return age;
        }
    }
}
