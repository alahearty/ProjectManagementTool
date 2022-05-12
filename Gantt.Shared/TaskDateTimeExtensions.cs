using System;

namespace Gantt.Shared
{
    public static class TaskDateTimeExtensions
    {
        /// <summary>
        /// Returns end-date for the specified start-date and duration.
        /// </summary>
        /// <param name="startDate">Specify start date</param>
        /// <param name="duration">Specify duration in [days]</param>
        /// <returns></returns>
        public static DateTime ToEndDate(this DateTime startDate, double duration)
        {
            return startDate.AddDays(duration - 1);
        }

        /// <summary>
        /// Returns start-date for the specified end-date and duration.
        /// </summary>
        /// <param name="endDate">Specify end date.</param>
        /// <param name="duration">Specify duration in [days].</param>
        /// <returns></returns>
        public static DateTime ToStartDate(this DateTime endDate, double duration)
        {
            return endDate.AddDays(-duration + 1);
        }


        /// <summary>
        /// Returns duration in [days].
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static float ToDuration(this DateTime startDate, DateTime endDate)
        {
            return (endDate - startDate).Days + 1;
        }
    }
}
