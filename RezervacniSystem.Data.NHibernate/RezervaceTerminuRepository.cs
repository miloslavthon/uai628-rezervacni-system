using NHibernate;
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
		public IList<PrehledRezervace> VratRezervace(int idKlienta, bool pouzeAktualnePlatne)
		{
			return CurrentSession.CreateQuery(@"
				select r.Id, terminRezervace.Datum, terminUdalosti.CasTrvani.DobaTrvani, udalost.Popis, poskytovatel.Nazev
				from RezervaceTerminu r
					join r.Termin terminRezervace
					join r.Poskytovatel poskytovatel
					join terminRezervace.TerminUdalosti terminUdalosti
					join terminUdalosti.Udalost udalost
				where r.Klient.Id = :idKlienta" +
				(pouzeAktualnePlatne ? " and terminRezervace.Datum >= getdate()" : null) +
				" order by terminRezervace.Datum")
				.SetInt32("idKlienta", idKlienta)
				.Enumerable<Object[]>()
				.Select(o => new PrehledRezervace((int)o[0], (DateTime)o[1], (TimeSpan)o[2], (String)o[3], (String)o[4]))
				.ToList();
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
