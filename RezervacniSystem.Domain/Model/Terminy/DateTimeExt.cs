using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.Terminy
{
	public static class DateTimeExt
	{
		public static DenVTydnu VratDenVTydnu(this DateTime datum)
		{
			return datum.DayOfWeek == 0 ? DenVTydnu.Ne : (DenVTydnu)(datum.DayOfWeek - 1);
		}
	}
}
