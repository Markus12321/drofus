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
            /*
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
            */


            s += "Mon 01:00-23:00\n";
            s += ("Tue 01:00-23:00\n");
            s += ("Wed 01:00-23:00\n");
            s += ("Thu 01:00-23:00\n");
            s += ("Fri 01:00-23:00\n");
            s += ("Sat 01:00-23:00\n");
            s += ("Sun 01:00-21:00");



            Solution solution = new Solution();
            solution.solution(s);
            Console.Read();



        }
    }

    class Solution
    {
        public int solution(String s)
        {
            String[] meetings = s.Split("\n");
            List<Meeting> meetingsList = new List<Meeting>();

            foreach (String meeting in meetings) {
                meetingsList.Add(new Meeting(meeting));
            }

            meetingsList.Sort();


            int longestBreak = 0;
            int breakStart = 0;
            int breakTime;
            foreach (Meeting meeting in meetingsList)
            {
                Console.WriteLine(meeting);
                breakTime = meeting.MeetingStartInMinutesSinceMonday - breakStart;
                if (breakTime > longestBreak)
                {
                    longestBreak = breakTime;
                    Console.WriteLine("Current longest break: " + longestBreak);

                }
                breakStart = meeting.MeetingEndInMinutesSinceMonday;

            }

            breakTime = 6 * 1440 + 24 * 60 - breakStart;
            if(breakTime > longestBreak)
            {
                longestBreak = breakTime;
            }

            Console.WriteLine("LONGEST BREAK: " + longestBreak);




            return longestBreak;

        }



    }

    class Meeting : IComparable<Meeting>
    {
        private const int MinutesInADay = 1440; 
        public int MeetingStartInMinutesSinceMonday { get; set; }
        public int MeetingEndInMinutesSinceMonday { get; set; }

        private int dayOfWeek;



        public int CompareTo(Meeting other)
        {
            return this.MeetingStartInMinutesSinceMonday - other.MeetingStartInMinutesSinceMonday;
        }

        public override string ToString() {
            return ($"MeetingStartInMinutesSinceMonday{MeetingStartInMinutesSinceMonday}:MeetingEndInMinutesSinceMonday{MeetingEndInMinutesSinceMonday}");

        }


        public Meeting(string meeting)
        {
                String day = meeting.Substring(0, 3);

            switch (day)
            {
                case "Mon": 
                    this.dayOfWeek = 0;
                    break;
                case "Tue":
                    this.dayOfWeek = 1;
                    break;
                case "Wed":
                    this.dayOfWeek = 2;
                    break;
                case "Thu":
                    this.dayOfWeek = 3;
                    break;
                case "Fri":
                    this.dayOfWeek = 4;
                    break;
                case "Sat":
                    this.dayOfWeek = 5;
                    break;
                case "Sun":
                    this.dayOfWeek = 6;
                    break;
                default:
                    break;
            }



            int hourOfDayMeetingStart = int.Parse(meeting.Substring(4, 2));
            int minutesOfHourMeetingStart = int.Parse(meeting.Substring(7, 2));

            int hourOfDayMeetingEnd = int.Parse(meeting.Substring(10, 2));
            int minutesOfHourMeetingEnd = int.Parse(meeting.Substring(13, 2));

            //Console.WriteLine($"DAY:{day} {dayOfWeek}  {hourOfDayMeetingStart}:{minutesOfHourMeetingStart}-{hourOfDayMeetingEnd}:{minutesOfHourMeetingEnd}");

            this.MeetingStartInMinutesSinceMonday += MinutesInADay * dayOfWeek + hourOfDayMeetingStart * 60 + minutesOfHourMeetingStart;
            this.MeetingEndInMinutesSinceMonday += MinutesInADay * dayOfWeek + hourOfDayMeetingEnd * 60 + minutesOfHourMeetingEnd;

           // Console.WriteLine($"MeetingStartInMinutesSinceMonday{MeetingStartInMinutesSinceMonday}:MeetingEndInMinutesSinceMonday{MeetingEndInMinutesSinceMonday}");





        }

    }

    

}
