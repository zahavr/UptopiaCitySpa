using System;

namespace API.Extensions
{
	public static class DateTimeExtensions
	{
		public static double CalculateWorkExpirience(this DateTime date, DateTime startWork)
		{
			return Math.Round((date - startWork).TotalDays, 0, MidpointRounding.AwayFromZero);
		}
	}
}
