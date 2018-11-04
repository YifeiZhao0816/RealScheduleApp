using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAppCodingClub.Class
{

    // A custom class that save the data on local machine so that the data will not lose after restarting the app
    // For this part, refer to description of localSettings on Microsoft document:
    // https://docs.microsoft.com/en-us/windows/uwp/design/app-settings/store-and-retrieve-app-data

    static class StoreData
    {
        private static string[] nameSets = { "s1", "s2", "s3", "s4", "s5", "s6", "s7", "s8", "s9", "s10", "s11" };


        private static string ConvertDateToString(DateTime dateTime)
        {
            CultureInfo culture = new CultureInfo("en-US");
            return dateTime.ToString(culture);
        }

        private static string ConvertDateToString(TimeSpan dateTime)
        {
            CultureInfo culture = new CultureInfo("en-US");
            return dateTime.ToString("c",culture);
        }

        private static DateTime ConvertStringToDate(string str)
        {
            CultureInfo culture = new CultureInfo("en-US");
            return DateTime.Parse(str, culture);
        }

        private static TimeSpan ConvertStringToTime(string str)
        {
            CultureInfo culture = new CultureInfo("en-US");
            return TimeSpan.Parse(str, culture);
        }

        // save the current date to the localSettings
        public static void SaveDate()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["Date"] = ConvertDateToString(DateTime.Now);
        }

        // Save a boolean with the saving name 
        public static void SaveData(bool value, string name)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values[name] = value;
        }

        // load the date last time the program saved, if there's not any, return today's date.
        public static DateTime LoadDate()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            CultureInfo culture = new CultureInfo("en-US");
            if (localSettings.Values["Date"] != null)
            {
                string temp = (string)localSettings.Values["Date"];
                return ConvertStringToDate(temp);
            }
            else
                return DateTime.Today;
        }

        // load specific data from localSettings called name.
        public static bool LoadData(string name)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values[name] != null)
            {
                bool value = (bool)localSettings.Values[name];
                return value;
            }
            else
                return false;
        }

        public static void StoreSchedule(List<Subject> schedule)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Windows.Storage.ApplicationDataCompositeValue composite = new Windows.Storage.ApplicationDataCompositeValue();
            int total = schedule.Count, count = 0;
            localSettings.Values["Total"] = total;
            foreach (Subject subject in schedule)
            {
                composite["Block"] = subject.Block;
                composite["SubjectName"] = subject.SubjectName;
                composite["BeginTime"] = ConvertDateToString(subject.BeginTime);
                composite["EndTime"] = ConvertDateToString(subject.EndTime);
                composite["period"] = ConvertDateToString(subject.PeriodLength);

                localSettings.Values[nameSets[count]] = composite;
            }
        }

        public static List<Subject> LoadSchedule()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Windows.Storage.ApplicationDataCompositeValue composite = new Windows.Storage.ApplicationDataCompositeValue();
            List<Subject> result = new List<Subject>();
            int total = (int)localSettings.Values["Total"];
            for (int i = 0; i < total; i++)
            {
                composite = (Windows.Storage.ApplicationDataCompositeValue)localSettings.Values[nameSets[i]];
                result.Add(new Subject((int)composite["Block"], 
                                       (string)composite["SubjectName"],
                                       ConvertStringToTime((string)composite["BeginTime"]),
                                       ConvertStringToTime((string)composite["EndTime"]),
                                       ConvertStringToTime((string)composite["PeriodLength"])
                                       ));
            }
            return result;
        }
    }
}
