using NHibernate;
using RezervacniSystem.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Data.NHibernate
{
	public abstract class NHibernateRepository<T> where T : DomainObject
	{
		public ISessionFactory SessionFactory { get; set; }

		protected ISession CurrentSession
		{
			get
			{
				return SessionFactory.GetCurrentSession();
			}
		}

		public virtual T Vrat(int id)
		{
			return CurrentSession.Get<T>(id);
		}

		public virtual IList<T> VratVse()
		{
			return CurrentSession.QueryOver<T>().List<T>();
		}

		public virtual void Uloz(T domainObject)
		{
			CurrentSession.SaveOrUpdate(domainObject);
			CurrentSession.Flush();
		}

		public virtual void Odstran(int id)
		{
			Odstran(Vrat(id));
		}

		public virtual void Odstran(T domainObject)
		{
			CurrentSession.Delete(domainObject);
			CurrentSession.Flush();
		}
	}
}
