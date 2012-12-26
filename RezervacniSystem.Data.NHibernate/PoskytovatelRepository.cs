using RezervacniSystem.Domain.Model.Poskytovatele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Data.NHibernate
{
	public class PoskytovatelRepository : NHibernateRepository<Poskytovatel>, IPoskytovatelRepository
	{
		public override void Uloz(Poskytovatel domainObject)
		{
			if (!CurrentSession.Contains(domainObject) && Query.Where(p => p.Nazev == domainObject.Nazev).RowCount() > 0)
			{
				throw new ArgumentException("Poskytovatel s názvem " + domainObject.Nazev + " již existuje.");
			}

			base.Uloz(domainObject);
		}

		public override IList<Poskytovatel> VratVse()
		{
			return Query.OrderBy(p => p.Nazev).Asc.List();
		}

		public IList<Poskytovatel> VratDleNazvu(String nazev)
		{
			return Query.WhereRestrictionOn(p => p.Nazev).IsLike(nazev + "%").List();

			//var query = CurrentSession.CreateQuery("from Poskytovatel p where p.Nazev like :n");
			//query.SetString("n", nazev + "%");
			//return query.List<Poskytovatel>();
		}

		public int VratIdPoskytovateleDleUzivatelskehoJmena(String login)
		{
			return Query.Where(p => p.Login == login).Select(p => p.Id).SingleOrDefault<int>();
		}
	}
}
