using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Infrastructure
{
	public static class Validate
	{
		public static void NotNull(Object value, String message)
		{
			if (value == null)
			{
				throw new ArgumentException(message);
			}
		}

		public static void IsNull(Object value, String message)
		{
			if (value != null)
			{
				throw new ArgumentException(message);
			}
		}

		public static void NotNullOrEmpty(String value, String message)
		{
			if (String.IsNullOrEmpty(value))
			{
				throw new ArgumentException(message);
			}
		}

		public static void IsTrue(bool predicate, String message)
		{
			if (!predicate)
			{
				throw new ArgumentException(message);
			}
		}

		public static void IsNotTrue(bool predicate, String message)
		{
			IsTrue(!predicate, message);
		}

		public static void AreEqual(Object a, Object b, String message)
		{
			if (!Object.Equals(a, b))
			{
				throw new ArgumentException(message);
			}
		}

		public static void AreNotEqual(Object a, Object b, String message)
		{
			if (Object.Equals(a, b))
			{
				throw new ArgumentException(message);
			}
		}
	}
}
