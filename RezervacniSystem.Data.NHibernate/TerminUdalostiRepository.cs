using RezervacniSystem.Domain.Model.Terminy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Data.NHibernate
{
	public class TerminUdalostiRepository : NHibernateRepository<TerminUdalosti>, ITerminUdalostiRepository
	{
		public IList<TerminUdalosti> VratTerminyDleUdalosti(int idUdalosti)
		{
			return Query.Where(t => t.Udalost.Id == idUdalosti).List();
		}

		public IList<TerminUdalosti> VratAktualnePlatneTerminyDleUdalosti(int idUdalosti)
		{
			DateTime datum = DateTime.Now;

			return Query.Where(
				t => t.Udalost.Id == idUdalosti &&
				(t.Typ == TypTerminu.JEDNORAZOVY && t.Datum > datum || t.Typ == TypTerminu.OPAKOVANY && t.PlatnyDo > datum)
			).List();
		}
	}
}
