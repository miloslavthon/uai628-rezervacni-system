using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Data.NHibernate
{
	public abstract class NHibernateRepository
	{
		public ISessionFactory SessionFactory { get; set; }

		protected ISession CurrentSession
		{
			get
			{
				return SessionFactory.GetCurrentSession();
			}
		}
	}
}
