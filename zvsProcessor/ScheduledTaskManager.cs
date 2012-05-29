﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using zVirtualScenesModel;

namespace zVirtualScenes
{
    public class ScheduledTaskManager
    {
        private bool isRunning = false;
        private Core Core;
        Timer delayTimer;

        public ScheduledTaskManager(Core Core, bool autoStart = true)
        {
            this.Core = Core;

            if (autoStart)
            {
                Start();
                
                //Load the tasks into the context.
                Core.context.scheduled_tasks.ToList();
            }
        }

        public void Start()
        {
            if (!isRunning)
            {
                delayTimer = new Timer((state) =>
                {
                    foreach (scheduled_tasks task in Core.context.scheduled_tasks.Local)
                    {
                        if (task.Enabled)
                        {
                            if (task.Frequency.HasValue)
                            {
                                switch ((scheduled_tasks.frequencys)task.Frequency)
                                {
                                    case scheduled_tasks.frequencys.Seconds:
                                        {
                                            if (task.StartTime.HasValue)
                                            {
                                                int sec = (int)(DateTime.Now - task.StartTime.Value).TotalSeconds;
                                                if (sec % task.RecurSeconds == 0)
                                                    task.Run(Core.context);
                                            }
                                            break;
                                        }
                                    case scheduled_tasks.frequencys.Daily:
                                        {
                                            if (task.StartTime.HasValue)
                                            {
                                                //Logger.WriteToLog(Urgency.INFO,"totaldays:" + (DateTime.Now.Date - task.StartTime.Value.Date).TotalDays);
                                                if (task.RecurDays > 0 && ((DateTime.Now.Date - task.StartTime.Value.Date).TotalDays % task.RecurDays == 0))
                                                {
                                                    TimeSpan TimeNowToTheSeconds = DateTime.Now.TimeOfDay;
                                                    TimeNowToTheSeconds = new TimeSpan(TimeNowToTheSeconds.Hours, TimeNowToTheSeconds.Minutes, TimeNowToTheSeconds.Seconds); //remove milli seconds

                                                    //Logger.WriteToLog(Urgency.INFO,string.Format("taskTofD: {0}, nowTofD: {1}", task.StartTime.Value.TimeOfDay, TimeNowToTheSeconds));                                            
                                                    if (TimeNowToTheSeconds.Equals(task.StartTime.Value.TimeOfDay))
                                                        task.Run(Core.context);
                                                }
                                            }
                                            break;
                                        }
                                    case scheduled_tasks.frequencys.Weekly:
                                        {
                                            if (task.StartTime.HasValue)
                                            {
                                                if (task.RecurWeeks > 0 && (((Int32)(DateTime.Now.Date - task.StartTime.Value.Date).TotalDays / 7) % task.RecurWeeks == 0))  //IF RUN THIS WEEK
                                                {
                                                    if (ShouldRunToday(task))  //IF RUN THIS DAY 
                                                    {
                                                        TimeSpan TimeNowToTheSeconds = DateTime.Now.TimeOfDay;
                                                        TimeNowToTheSeconds = new TimeSpan(TimeNowToTheSeconds.Hours, TimeNowToTheSeconds.Minutes, TimeNowToTheSeconds.Seconds);

                                                        if (TimeNowToTheSeconds.Equals(task.StartTime.Value.TimeOfDay))
                                                            task.Run(Core.context);
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case scheduled_tasks.frequencys.Monthly:
                                        {
                                            if (task.StartTime.HasValue)
                                            {
                                                int monthsapart = ((DateTime.Now.Year - task.StartTime.Value.Year) * 12) + DateTime.Now.Month - task.StartTime.Value.Month;
                                                //Logger.WriteToLog(Urgency.INFO,string.Format("Months Apart: {0}", monthsapart));
                                                if (task.RecurMonth > 0 && monthsapart > -1 && monthsapart % task.RecurMonth == 0)  //IF RUN THIS Month
                                                {
                                                    if (ShouldRunThisDayOfMonth(task))  //IF RUN THIS DAY 
                                                    {
                                                        TimeSpan TimeNowToTheSeconds = DateTime.Now.TimeOfDay;
                                                        TimeNowToTheSeconds = new TimeSpan(TimeNowToTheSeconds.Hours, TimeNowToTheSeconds.Minutes, TimeNowToTheSeconds.Seconds);

                                                        if (TimeNowToTheSeconds.Equals(task.StartTime.Value.TimeOfDay))
                                                            task.Run(Core.context);
                                                    }
                                                }
                                            }

                                            break;
                                        }
                                    case scheduled_tasks.frequencys.Once:
                                        {
                                            if (task.StartTime.HasValue)
                                            {
                                                TimeSpan TimeNowToTheSeconds = DateTime.Now.TimeOfDay;
                                                TimeNowToTheSeconds = new TimeSpan(TimeNowToTheSeconds.Hours, TimeNowToTheSeconds.Minutes, TimeNowToTheSeconds.Seconds);

                                                if (TimeNowToTheSeconds.Equals(task.StartTime.Value.TimeOfDay))
                                                    task.Run(Core.context);
                                            }
                                            break;
                                        }
                                }
                            }
                        }
                    }
                }, null, 0, 1000);

                isRunning = true;
            }
        }

        public void Stop()
        {
            if (isRunning)
            {
                delayTimer.Dispose();
                isRunning = false;
            }
        }

        private bool ShouldRunThisDayOfMonth(scheduled_tasks task)
        {
            switch (DateTime.Now.Day)
            {
                case 1:
                    if (task.RecurDay01.HasValue && task.RecurDay01.Value) { return true; }
                    break;
                case 2:
                    if (task.RecurDay02.HasValue && task.RecurDay02.Value) { return true; }
                    break;
                case 3:
                    if (task.RecurDay03.HasValue && task.RecurDay03.Value) { return true; }
                    break;
                case 4:
                    if (task.RecurDay04.HasValue && task.RecurDay04.Value) { return true; }
                    break;
                case 5:
                    if (task.RecurDay05.HasValue && task.RecurDay05.Value) { return true; }
                    break;
                case 6:
                    if (task.RecurDay06.HasValue && task.RecurDay06.Value) { return true; }
                    break;
                case 7:
                    if (task.RecurDay07.HasValue && task.RecurDay07.Value) { return true; }
                    break;
                case 8:
                    if (task.RecurDay08.HasValue && task.RecurDay08.Value) { return true; }
                    break;
                case 9:
                    if (task.RecurDay09.HasValue && task.RecurDay09.Value) { return true; }
                    break;
                case 10:
                    if (task.RecurDay10.HasValue && task.RecurDay10.Value) { return true; }
                    break;
                case 11:
                    if (task.RecurDay11.HasValue && task.RecurDay11.Value) { return true; }
                    break;
                case 12:
                    if (task.RecurDay12.HasValue && task.RecurDay12.Value) { return true; }
                    break;
                case 13:
                    if (task.RecurDay13.HasValue && task.RecurDay13.Value) { return true; }
                    break;
                case 14:
                    if (task.RecurDay14.HasValue && task.RecurDay14.Value) { return true; }
                    break;
                case 15:
                    if (task.RecurDay15.HasValue && task.RecurDay15.Value) { return true; }
                    break;
                case 16:
                    if (task.RecurDay16.HasValue && task.RecurDay16.Value) { return true; }
                    break;
                case 17:
                    if (task.RecurDay17.HasValue && task.RecurDay17.Value) { return true; }
                    break;
                case 18:
                    if (task.RecurDay18.HasValue && task.RecurDay18.Value) { return true; }
                    break;
                case 19:
                    if (task.RecurDay19.HasValue && task.RecurDay19.Value) { return true; }
                    break;
                case 20:
                    if (task.RecurDay20.HasValue && task.RecurDay20.Value) { return true; }
                    break;
                case 21:
                    if (task.RecurDay21.HasValue && task.RecurDay21.Value) { return true; }
                    break;
                case 22:
                    if (task.RecurDay22.HasValue && task.RecurDay22.Value) { return true; }
                    break;
                case 23:
                    if (task.RecurDay23.HasValue && task.RecurDay23.Value) { return true; }
                    break;
                case 24:
                    if (task.RecurDay24.HasValue && task.RecurDay24.Value) { return true; }
                    break;
                case 25:
                    if (task.RecurDay25.HasValue && task.RecurDay25.Value) { return true; }
                    break;
                case 26:
                    if (task.RecurDay26.HasValue && task.RecurDay26.Value) { return true; }
                    break;
                case 27:
                    if (task.RecurDay27.HasValue && task.RecurDay27.Value) { return true; }
                    break;
                case 28:
                    if (task.RecurDay28.HasValue && task.RecurDay28.Value) { return true; }
                    break;
                case 29:
                    if (task.RecurDay29.HasValue && task.RecurDay29.Value) { return true; }
                    break;
                case 30:
                    if (task.RecurDay30.HasValue && task.RecurDay30.Value) { return true; }
                    break;
                case 31:
                    if (task.RecurDay31.HasValue && task.RecurDay31.Value) { return true; }
                    break;
            }
            return false;
        }

        private bool ShouldRunToday(scheduled_tasks task)
        {
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    if (task.RecurMonday.HasValue && task.RecurMonday.Value)
                        return true;
                    break;

                case DayOfWeek.Tuesday:
                    if (task.RecurTuesday.HasValue && task.RecurTuesday.Value)
                        return true;
                    break;

                case DayOfWeek.Wednesday:
                    if (task.RecurWednesday.HasValue && task.RecurWednesday.Value)
                        return true;
                    break;

                case DayOfWeek.Thursday:
                    if (task.RecurThursday.HasValue && task.RecurThursday.Value)
                        return true;
                    break;

                case DayOfWeek.Friday:
                    if (task.RecurFriday.HasValue && task.RecurFriday.Value)
                        return true;
                    break;

                case DayOfWeek.Saturday:
                    if (task.RecurSaturday.HasValue && task.RecurSaturday.Value)
                        return true;
                    break;

                case DayOfWeek.Sunday:
                    //BUG FIX 48:  Sunday not working as directed
                    if (task.RecurSunday.HasValue && task.RecurSunday.Value)
                        return true;
                    break;
            }

            return false;
        }

        private int NumberOfWeeks(DateTime dateFrom, DateTime dateTo)
        {
            TimeSpan Span = dateTo.Subtract(dateFrom);

            if (Span.Days <= 7)
            {
                if (dateFrom.DayOfWeek > dateTo.DayOfWeek)
                {
                    return 2;
                }

                return 1;
            }

            int Days = Span.Days - 7 + (int)dateFrom.DayOfWeek;
            int WeekCount = 1;
            int DayCount = 0;

            for (WeekCount = 1; DayCount < Days; WeekCount++)
            {
                DayCount += 7;
            }

            return WeekCount;
        }
    }
}