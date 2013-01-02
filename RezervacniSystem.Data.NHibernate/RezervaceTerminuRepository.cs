using RezervacniSystem.Domain.Model.Rezervace;
using RezervacniSystem.Domain.Model.TerminyRezervaci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Data.NHibernate
{
	public class RezervaceTerminuRepository : DomainObjectRepository<RezervaceTerminu>, IRezervaceTerminuRepository
	{
		public IList<RezervaceTerminu> VratRezervace(int idKlienta, bool pouzeAktualnePlatne)
		{
			var query = Query
				.Where(r => r.Klient.Id == idKlienta)
				.JoinQueryOver<TerminRezervace>(r => r.Termin);

			if (pouzeAktualnePlatne)
			{
				query = query.And(r => r.Datum >= DateTime.Now);
			}

			return query
				.JoinQueryOver<RezervacniSystem.Domain.Model.Terminy.TerminUdalosti>(t => t.TerminUdalosti)
				.JoinQueryOver<RezervacniSystem.Domain.Model.Udalosti.Udalost>(t => t.Udalost)
				.List();
		}

		public int VratPocetAktualnePlatnychRezervaci(int idKlienta, int idPoskytovatele)
		{
			return Query
				.Where(r => r.Klient.Id == idKlienta && r.Poskytovatel.Id == idPoskytovatele)
				.JoinQueryOver<TerminRezervace>(r => r.Termin)
				.Where(t => t.Datum >= DateTime.Now)
				.RowCount();
		}

		public bool ExistujePlatnaRezervaceProDanyCas(int idKlienta, DateTime datumOd, DateTime datumDo)
		{
			return Query
				.Where(r => r.Klient.Id == idKlienta)
				.JoinQueryOver<TerminRezervace>(r => r.Termin)
				.Where(t =>
					t.Datum >= DateTime.Now && (
						t.Datum >= datumOd && t.Datum < datumDo ||	// termín začíná v zadaném intervalu
						t.Konec >= datumOd && t.Konec < datumDo ||	// termín končí v zadaném intervalu
						t.Datum <= datumOd && t.Konec >= datumDo	// termín úplně překrývá zadaný interval
					)
				)
				.RowCount() > 0;
		}
	}
}
