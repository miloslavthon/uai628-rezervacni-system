using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Infrastructure
{
	public static class Validate
	{
		public static void NotNull(Object obj, String message)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(message);
			}
		}

		public static void NotNullOrEmpty(String value, String message)
		{
			if (String.IsNullOrEmpty(value))
			{
				throw new ArgumentException(message);
			}
		}
	}
}
