﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleAppCodingClub.Class
{
    public class Subject
    {
        public int Block { get; set; }
        public string SubjectName { get; set; }
        public TimeSpan BeginTime{ get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan PeriodLength { get; set; }

        // constructor
        public Subject(int block, string subjectName, TimeSpan beginTime, TimeSpan endTime, TimeSpan periodLength)
        {
            Block = block;
            SubjectName = subjectName;
            BeginTime = beginTime;
            EndTime = endTime;
            PeriodLength = periodLength;
        }

        // return true if it is a Aday block. false if it is on B day.
        public bool isA()
        {
            return Block % 2 == 0;
        }
    }
}
