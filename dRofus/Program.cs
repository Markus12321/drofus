using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace dRofus
{
    class Program
    {
        static void Main(string[] args)
        {
            String s = "";
          
            s += "Sun 10:00-20:00\n";
            s += ("Fri 05:00-10:00\n");
            s += ("Fri 16:30-23:50\n");
            s += ("Sat 10:00-24:00\n");
            s += ("Sun 01:00-04:00\n");
            s += ("Sat 02:00-06:00\n");
            s += ("Tue 03:30-18:15\n");
            s += ("Tue 19:00-20:00\n");
            s += ("Wed 04:25-15:14\n");
            s += ("Wed 15:14-22:40\n");
            s += ("Thu 00:00-23:59\n");
            s += ("Mon 05:00-13:00\n");
            s += ("Mon 15:00-21:00");
       


            //s += "Mon 01:00-23:00\n";
            //s += ("Tue 01:00-23:00\n");
            //s += ("Wed 01:00-23:00\n");
            //s += ("Thu 01:00-23:00\n");
            //s += ("Fri 01:00-23:00\n");
            //s += ("Sat 01:00-23:00\n");
            //s += ("Sun 01:00-21:00");



            Solution solution = new Solution();
            int longestBreak = solution.solution(s);
            Console.WriteLine(longestBreak);



        }
    }

    class Solution
    {
        public int solution(String s)
        {

            String[] meetingsArray = s.Split("\n");

            List<Meeting> meetings = new List<Meeting>();

            foreach (String meeting in meetingsArray)
            {
                meetings.Add(new Meeting(meeting));
            }

            meetings.Sort();


            int longestBreak = 0;
            int breakStart = 0;
            int breakTime;

            foreach (Meeting meeting in meetings)
            {
                breakTime = meeting.TimeMeetingStartsInMinutesSinceMonday - breakStart;

                if (breakTime > longestBreak)
                    longestBreak = breakTime;
                
                breakStart = meeting.TimeMeetingEndsInMinutesSinceMonday;

            }

            const int MinutesInAWeek = 6 * 1440 + 24 * 60;

            breakTime = MinutesInAWeek - breakStart; 
            if (breakTime > longestBreak)
            {
                longestBreak = breakTime;
            }

            return longestBreak;

        }



    }

    class Meeting : IComparable<Meeting>
    {
        private const int MinutesInADay = 1440;
        //wtf do i call these 2 props? i mean time in minutes the meeting start since 00:00 monday
        public int TimeMeetingStartsInMinutesSinceMonday { get; set; }
        public int TimeMeetingEndsInMinutesSinceMonday { get; set; }

        private int _dayOfWeek;

        public int DayOfWeek
        {
            get { return _dayOfWeek; }
            set
            {
                if (value < 0 || value > 6)
                    throw new ArgumentOutOfRangeException(
                          $"{nameof(value)} must be between 0 and 6.");

                _dayOfWeek = value;
            }
        }

        public Meeting(string meeting)
        {

            String day = meeting.Substring(0, 3);

            switch (day)
            {
                case "Mon":
                    DayOfWeek = 0;
                    break;
                case "Tue":
                    DayOfWeek = 1;
                    break;
                case "Wed":
                    DayOfWeek = 2;
                    break;
                case "Thu":
                    DayOfWeek = 3;
                    break;
                case "Fri":
                    DayOfWeek = 4;
                    break;
                case "Sat":
                    DayOfWeek = 5;
                    break;
                case "Sun":
                    DayOfWeek = 6;
                    break;
                default:
                    break;
            }

            //Could have used RegEx for a more maintainable code, but the problem specified the format of the string, so this was not strictly necessary

            //wtf do i call these variables

            int hourOfDayMeetingStarts = int.Parse(meeting.Substring(4, 2));
            int minutesOfHourMeetingStarts = int.Parse(meeting.Substring(7, 2));

            int hourOfDayMeetingEnds = int.Parse(meeting.Substring(10, 2));
            int minutesOfHourMeetingEnds = int.Parse(meeting.Substring(13, 2));

            this.TimeMeetingStartsInMinutesSinceMonday += MinutesInADay * DayOfWeek + hourOfDayMeetingStarts * 60 + minutesOfHourMeetingStarts;
            this.TimeMeetingEndsInMinutesSinceMonday += MinutesInADay * DayOfWeek + hourOfDayMeetingEnds * 60 + minutesOfHourMeetingEnds;
        }


        public int CompareTo(Meeting other)
        {
            return this.TimeMeetingStartsInMinutesSinceMonday - other.TimeMeetingStartsInMinutesSinceMonday;
        }  
    }



}
