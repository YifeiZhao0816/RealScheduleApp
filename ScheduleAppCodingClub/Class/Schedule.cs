using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAppCodingClub.Class
{
    public enum SchoolStatus { THED, TED, normal }                                  // Enumeration of school Status
                                                                                    //（basically saying there's only 3 certain value for this variable）

    static class Schedule
    {
        public static bool isADay { get; set; }                                     // indicates whether the current date is a A day
        public static List<Subject> subjects { get; set; }                          // A list that contains all classes
        public static List<Subject> todayClass { get; set; }                        // A list that contains all current day classes
        public static SchoolStatus SchoolStatus { get; set; }                       // current day school status
        public static TimeSpan Cache { get; set; }

        public static bool LoadScehdule()
        {
            SchoolStatus = SchoolStatus.normal;
            subjects = new List<Subject>();

            List<Subject> StoredSchedule = StoreData.LoadSchedule();
            foreach (Subject item in StoredSchedule)
            {
                subjects.Add(item);
            }

            /*                                                                      // load hardcoded schedule
            if (subjects.Count == 0) {
                subjects.Add(new Subject(1, "Calculus AB", new TimeSpan(8, 20, 0), new TimeSpan(9, 55, 0), new TimeSpan(1, 35, 0)));
                subjects.Add(new Subject(3, "Study Hall", new TimeSpan(10, 00, 0), new TimeSpan(11, 35, 0), new TimeSpan(1, 35, 0)));
                subjects.Add(new Subject(5, "APCS", new TimeSpan(11, 40, 0), new TimeSpan(13, 15, 0), new TimeSpan(1, 35, 0)));
                subjects.Add(new Subject(0, "3nd Lunch", new TimeSpan(13, 15, 0), new TimeSpan(13, 45, 0), new TimeSpan(0, 30, 0)));
                subjects.Add(new Subject(7, "CAD Drawing", new TimeSpan(13, 50, 0), new TimeSpan(15, 25, 0), new TimeSpan(1, 35, 0)));
                subjects.Add(new Subject(2, "Pre-AP Chemistry", new TimeSpan(8, 20, 0), new TimeSpan(9, 55, 0), new TimeSpan(1, 35, 0)));
                subjects.Add(new Subject(4, "French II", new TimeSpan(10, 00, 0), new TimeSpan(11, 35, 0), new TimeSpan(1, 35, 0)));
                subjects.Add(new Subject(6, "AP US History", new TimeSpan(11, 40, 0), new TimeSpan(13, 45, 0), new TimeSpan(2, 05, 0)));
                subjects.Add(new Subject(0, "2nd Lunch", new TimeSpan(12, 25, 0), new TimeSpan(12, 55, 0), new TimeSpan(0, 30, 0)));
                subjects.Add(new Subject(8, "English", new TimeSpan(13, 50, 0), new TimeSpan(15, 25, 0), new TimeSpan(1, 35, 0)));
            }
            */

            return (subjects.Count == 0);           //if false, the program has loaded the schedule from computer. 
                                                    //if true, no schedule already exist, ask user to create a new schedule.
        }

        public static void LoadTodayClass()
        {
            todayClass = new List<Subject>();                            // instantiation 
            foreach (Subject subject in subjects)                        // pick out all current day classes depending on the daytype
            {
                if (subject.isA() == isADay)
                {
                    todayClass.Add(subject);
                }
            }
            switch (SchoolStatus)                                        // edit the Schedule depending on the schoolstatus             
            {
                case SchoolStatus.THED:                                  // Two Hour Early Dismissal
                    Cache = new TimeSpan(8, 20, 0);                      // School Begin time
                    foreach (Subject item in todayClass)                 // for every block in the list todayClass
                    {
                        if (item.Block != 0)                             // for every block that is not a lunch block, reduce the block length by half hour
                        {
                            item.BeginTime = Cache;
                            item.EndTime = item.BeginTime.Add(item.PeriodLength.Subtract(new TimeSpan(0, 30, 0)));
                            Cache = item.EndTime.Add(new TimeSpan(0, 5, 0));
                        }
                        else                                             // edit lunch block depending on the lunch type
                        {
                            if (item.SubjectName.Equals("1nd Lunch"))
                            {
                                item.BeginTime = new TimeSpan(10, 35, 0);
                                item.EndTime = item.BeginTime.Add(item.PeriodLength);
                                Cache = item.EndTime.Add(new TimeSpan(0, 35, 0));
                            }
                            else if (item.SubjectName.Equals("2nd Lunch"))
                            {
                                item.BeginTime = new TimeSpan(11, 15, 0);
                                item.EndTime = item.BeginTime.Add(item.PeriodLength);
                            }
                            else
                            {
                                item.BeginTime = new TimeSpan(11, 45, 0);
                                item.EndTime = item.BeginTime.Add(item.PeriodLength);
                                Cache = item.EndTime.Add(new TimeSpan(0, 5, 0));
                            }
                        }

                    }
                    break;
                case SchoolStatus.TED:                                     // Same thing for Two hour delay
                    Cache = new TimeSpan(10, 20, 0);
                    foreach (Subject item in todayClass)
                    {
                        item.BeginTime = Cache;
                        if (item.Block != 0)
                        {
                            item.BeginTime = Cache;
                            item.EndTime = item.BeginTime.Add(item.PeriodLength.Subtract(new TimeSpan(0, 30, 0)));
                            Cache = item.EndTime.Add(new TimeSpan(0, 5, 0));
                        }
                        else
                        {
                            if (item.SubjectName.Equals("1nd Lunch"))
                            {
                                item.BeginTime = new TimeSpan(10, 35, 0);
                                item.EndTime = item.BeginTime.Add(item.PeriodLength);
                                Cache = item.EndTime.Add(new TimeSpan(0, 35, 0));
                            }
                            else if (item.SubjectName.Equals("2nd Lunch"))
                            {
                                item.BeginTime = new TimeSpan(11, 15, 0);
                                item.EndTime = item.BeginTime.Add(item.PeriodLength);
                            }
                            else
                            {
                                item.BeginTime = new TimeSpan(11, 45, 0);
                                item.EndTime = item.BeginTime.Add(item.PeriodLength);
                                Cache = item.EndTime.Add(new TimeSpan(0, 5, 0));
                            }
                        }
                    }
                    break;
                case SchoolStatus.normal:
                    break;
                default:
                    break;
            }
        }


    }
}

