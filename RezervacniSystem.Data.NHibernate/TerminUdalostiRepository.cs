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

			return Query.Where(t =>
				t.Udalost.Id == idUdalosti &&
				(t.Typ == TypTerminu.JEDNORAZOVY && t.Datum > datum || t.Typ == TypTerminu.OPAKOVANY && t.PlatnyDo > datum)
			).List();
		}

		public IList<TerminUdalosti> VratPlatneJednorazoveTerminyDleUdalosti(int idUdalosti, DateTime datumOd, DateTime datumDo)
		{
			return Query.Where(t =>
				t.Udalost.Id == idUdalosti &&
				t.Typ == TypTerminu.JEDNORAZOVY &&
				t.Datum > datumOd &&
				t.Datum < datumDo
			).List();
		}

		public IList<TerminUdalosti> VratPlatneOpakovaneTerminyDleUdalosti(int idUdalosti, DateTime datum)
		{
			return Query.Where(t =>
				t.Udalost.Id == idUdalosti &&
				t.Typ == TypTerminu.OPAKOVANY &&
				t.PlatnyDo > datum
			).List();
		}

		public void OdstranVsechnyTerminyUdalosti(int idUdalosti)
		{
			CurrentSession.CreateQuery("delete TerminUdalosti t where t.Udalost.Id = :id")
				.SetParameter("id", idUdalosti)
				.ExecuteUpdate();
		}

		public void OdstranVsechnyTerminyUdalostiProPoskytovatele(int idPoskytovatele)
		{
			CurrentSession.CreateQuery("delete TerminUdalosti t where t.Udalost.Id in (select u.Id from Udalost u where u.Poskytovatel.Id = :id)")
				.SetParameter("id", idPoskytovatele)
				.ExecuteUpdate();
		}
	}
}
