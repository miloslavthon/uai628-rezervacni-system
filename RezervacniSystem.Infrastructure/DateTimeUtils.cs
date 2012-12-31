using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Infrastructure
{
	public static class DateTimeUtils
	{
		public static String VypisDobu(TimeSpan timeSpan)
		{
			StringBuilder sb = new StringBuilder();

			if (timeSpan.Days > 0)
			{
				sb.Append(timeSpan.Days).Append(" d. ");
			}
			if (timeSpan.Hours > 0)
			{
				sb.Append(timeSpan.Hours).Append(" hod. ");
			}
			if (timeSpan.Minutes > 0)
			{
				sb.Append(timeSpan.Minutes).Append(" min. ");
			}

			return sb.ToString().TrimEnd();
		}
	}
}
