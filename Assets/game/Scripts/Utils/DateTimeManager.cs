using System;
using UnityEngine;

namespace Ab001.Util
{
    public class DateTimeManager
    {
        // Global variables
        private static DateTimeManager instance = null;

        public static DateTimeManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (typeof(DateTimeManager))
                    {
                        if (instance == null)
                        {
                            instance = new DateTimeManager();
                        }
                    }
                }
                return instance;
            }
        }

        private DateTimeManager()
        {
        }

        public DateTime getKoreaTimeFromUTCNow()
        {
            DateTime time = DateTime.UtcNow;
            return time.AddHours(9);
        }

        public int GetWeekCnt(DateTime dt)
        {
            int week = Enum.GetValues(typeof(DayOfWeek)).Length;
            int dayOffset = (int)dt.AddDays(-(dt.Day - 1)).DayOfWeek;
            int weekCnt = (dt.Day + dayOffset) / week;
            weekCnt += ((dt.Day + dayOffset) % week) > 0 ? 1 : 0;

            return weekCnt;
        }


        public int GetWeeksOfYear(DateTime date)
        {
            System.Globalization.CultureInfo cult_info = System.Globalization.CultureInfo.CreateSpecificCulture("ko");
            System.Globalization.Calendar cal = cult_info.Calendar;
            int weekNo = cal.GetWeekOfYear(date, cult_info.DateTimeFormat.CalendarWeekRule, cult_info.DateTimeFormat.FirstDayOfWeek);
            return weekNo;
            //int week1day = cal.GetWeekOfYear(date.AddDays(-(date.Day + 1)), cult_info.DateTimeFormat.CalendarWeekRule, cult_info.DateTimeFormat.FirstDayOfWeek);
            //Debug.Log("week1day : " + week1day);
            //return weekNo - week1day + 1;
        }

        public DateTime GetFirstDateOfWeek(int year, int week)
        {
            DateTime firstDateOfYear = new DateTime(year, 1, 1);
            DateTime firstDateOfFirstWeek = firstDateOfYear.AddDays(7 - (int)(firstDateOfYear.DayOfWeek) + 1);
            return firstDateOfFirstWeek.AddDays(7 * (week - 1));
        }
    }
}
