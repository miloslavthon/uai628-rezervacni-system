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
				select r.Id, terminRezervace.Datum, terminUdalosti.CasTrvani.DobaTrvani, udalost.Popis, poskytovatel.Nazev, terminUdalosti.UzaverkaRezervaci
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
				.Select(o => new PrehledRezervace((int)o[0], (DateTime)o[1], (TimeSpan)o[2], (String)o[3], (String)o[4], (TimeSpan)o[5]))
				.ToList();
		}

		public IList<RezervaceTerminu> VratRezervaceDleTerminuRezervace(int idTerminuRezervace)
		{
			return CurrentSession.CreateQuery(@"
				select r
				from RezervaceTerminu r
					join r.Termin terminRezervace
					join r.Klient klient
				where
					terminRezervace.Id = :idTerminuRezervace
				order by
					klient.Prijmeni, klient.Jmeno")
				.SetInt32("idTerminuRezervace", idTerminuRezervace)
				.List<RezervaceTerminu>();
		}

		public IList<RezervaceTerminu> VratRezervaceDleTerminuUdalosti(int idTerminuUdalosti)
		{
			return CurrentSession.CreateQuery(@"
				select r
				from RezervaceTerminu r
					join r.Termin terminRezervace
					join terminRezervace.TerminUdalosti terminUdalosti
					join terminUdalosti.Udalost udalost
					join r.Klient klient
				where
					terminUdalosti.Id = :idTerminuUdalosti
				order by
					klient.Prijmeni, klient.Jmeno")
				.SetInt32("idTerminuUdalosti", idTerminuUdalosti)
				.List<RezervaceTerminu>();
		}

		public IList<RezervaceTerminu> VratRezervaceDleUdalosti(int idUdalosti)
		{
			return CurrentSession.CreateQuery(@"
				select r
				from RezervaceTerminu r
					join r.Termin terminRezervace
					join terminRezervace.TerminUdalosti terminUdalosti
					join terminUdalosti.Udalost udalost
					join r.Klient klient
				where
					udalost.Id = :idUdalosti
				order by
					klient.Prijmeni, klient.Jmeno")
				.SetInt32("idUdalosti", idUdalosti)
				.List<RezervaceTerminu>();
		}

		public IList<RezervaceTerminu> VratRezervaceDlePoskytovatele(int idPoskytovatele)
		{
			return CurrentSession.CreateQuery(@"
				select r
				from RezervaceTerminu r
					join r.Termin terminRezervace
					join terminRezervace.TerminUdalosti terminUdalosti
					join terminUdalosti.Udalost udalost
					join udalost.Poskytovatel poskytovatel
					join r.Klient klient
				where
					poskytovatel.Id = :idPoskytovatele
				order by
					klient.Prijmeni, klient.Jmeno")
				.SetInt32("idPoskytovatele", idPoskytovatele)
				.List<RezervaceTerminu>();
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

		public void OdstranRezervaceDleTerminuUdalosti(int idTerminuUdalosti)
		{
			CurrentSession.CreateQuery(@"
				delete RezervaceTerminu r
				where
					r.Termin.Id in (
						select terminRezervace.Id
						from TerminRezervace terminRezervace
						where terminRezervace.TerminUdalosti.Id = :idTerminuUdalosti
					)
				")
				.SetParameter("idTerminuUdalosti", idTerminuUdalosti)
				.ExecuteUpdate();
		}

		public void OdstranRezervaceDleUdalosti(int idUdalosti)
		{
			CurrentSession.CreateQuery(@"
				delete RezervaceTerminu r
				where
					r.Termin.Id in (
						select terminRezervace.Id
						from TerminRezervace terminRezervace
							join terminRezervace.TerminUdalosti terminUdalosti
						where terminUdalosti.Udalost.Id = :idUdalosti
					)
				")
				.SetParameter("idUdalosti", idUdalosti)
				.ExecuteUpdate();
		}

		public void OdstranRezervaceDlePoskytovatele(int idPoskytovatele)
		{
			CurrentSession.CreateQuery(@"
				delete RezervaceTerminu r
				where
					r.Termin.Id in (
						select terminRezervace.Id
						from TerminRezervace terminRezervace
							join terminRezervace.TerminUdalosti terminUdalosti
							join terminUdalosti.Udalost udalost
						where udalost.Poskytovatel.Id = :idPoskytovatele
					)
				")
				.SetParameter("idPoskytovatele", idPoskytovatele)
				.ExecuteUpdate();
		}
	}
}
