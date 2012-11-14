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
		public IList<Poskytovatel> VratDleNazvu(String nazev)
		{
			return CurrentSession.QueryOver<Poskytovatel>().WhereRestrictionOn(p => p.Nazev).IsLike(nazev + "%").List();

			//var query = CurrentSession.CreateQuery("from Poskytovatel p where p.Nazev like :n");
			//query.SetString("n", nazev + "%");
			//return query.List<Poskytovatel>();
		}
	}
}
