using RezervacniSystem.Domain.Model.Klienti;
using RezervacniSystem.Domain.Model.Poskytovatele;
using RezervacniSystem.Domain.Model.RegistraceKlienta;
using RezervacniSystem.Domain.Model.Rezervace;
using RezervacniSystem.Domain.Model.Terminy;
using RezervacniSystem.Domain.Model.TerminyRezervaci;
using RezervacniSystem.Domain.Model.Udalosti;
using RezervacniSystem.Infrastructure;
using Spring.Transaction.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Application.Impl
{
	public class RezervaceService : IRezervaceService
	{
		private readonly IPoskytovatelRepository poskytovatelRepository;
		private readonly IUdalostRepository udalostRepository;
		private readonly ITerminUdalostiRepository terminUdalostiRepository;
		private readonly IKlientRepository klientRepository;
		private readonly ITerminRezervaceRepository terminRezervaceRepository;
		private readonly IRezervaceTerminuRepository rezervaceTerminuRepository;
		private readonly IRegistraceKlientaRepository registraceKlientaRepository;

		public RezervaceService(IPoskytovatelRepository poskytovatelRepository, IUdalostRepository udalostRepository, ITerminUdalostiRepository terminUdalostiRepository, IKlientRepository klientRepository, ITerminRezervaceRepository terminRezervaceRepository, IRezervaceTerminuRepository rezervaceTerminuRepository, IRegistraceKlientaRepository registraceKlientaRepository)
		{
			this.poskytovatelRepository = poskytovatelRepository;
			this.udalostRepository = udalostRepository;
			this.terminUdalostiRepository = terminUdalostiRepository;
			this.klientRepository = klientRepository;
			this.terminRezervaceRepository = terminRezervaceRepository;
			this.rezervaceTerminuRepository = rezervaceTerminuRepository;
			this.registraceKlientaRepository = registraceKlientaRepository;
		}

		public List<Tuple<DateTime, TerminUdalosti>> VyhledatVolneTerminy(int idUdalosti, DateTime datumOd, DateTime datumDo)
		{
			DateTime datum = DateTime.Now;
			Validate.IsTrue(datumDo > datum, "Není možné vyhledat volné termíny pro již uplynulé dny.");
			if (datumOd < datum)
			{
				datumOd = datum;
			}

			List<Tuple<DateTime, TerminUdalosti>> volneTerminy = new List<Tuple<DateTime, TerminUdalosti>>();

			Udalost udalost = udalostRepository.Vrat(idUdalosti);
			if (udalost.OpakovanyTermin)
			{
				foreach (TerminUdalosti t in terminUdalostiRepository.VratPlatneOpakovaneTerminyDleUdalosti(idUdalosti, datumOd))
				{
					int rozdilDnuVTydnu = t.Den.Value - datumOd.VratDenVTydnu();
					if (rozdilDnuVTydnu < 0)
					{
						rozdilDnuVTydnu += 7;
					}
					DateTime nasledujiciTermin = datumOd.Date.AddDays(rozdilDnuVTydnu).Add(t.CasTrvani.Cas);
					if (nasledujiciTermin < datumOd)
					{
						nasledujiciTermin = nasledujiciTermin.AddDays(7);
					}

					while (nasledujiciTermin < datumDo)
					{
						volneTerminy.Add(new Tuple<DateTime, TerminUdalosti>(nasledujiciTermin, t));
						nasledujiciTermin = nasledujiciTermin.AddDays(7);
					}
				}
			}
			else
			{
				foreach (TerminUdalosti t in terminUdalostiRepository.VratPlatneJednorazoveTerminyDleUdalosti(idUdalosti, datumOd, datumDo))
				{
					if (t.Datum.Value >= datumOd && t.Datum.Value < datumDo)
					{
						volneTerminy.Add(new Tuple<DateTime, TerminUdalosti>(t.Datum.Value.Add(t.CasTrvani.Cas), t));
					}
				}
			}

			volneTerminy.Sort((a, b) => a.Item1.CompareTo(b.Item1));

			return volneTerminy;
		}

		[Transaction]
		public void RezervovatTermin(int idKlienta, int idTerminuUdalosti, DateTime datum)
		{
			Klient klient = klientRepository.VratProUpravy(idKlienta);
			Validate.NotNull(klient, "Pro provedení rezervace musíte provést osobní nastavení klienta.");

			TerminUdalosti terminUdalosti = terminUdalostiRepository.Vrat(idTerminuUdalosti);

			// kontrola uzávěrky rezervací
			Validate.IsTrue(DateTime.Now < datum.Subtract(terminUdalosti.UzaverkaRezervaci), "Pro daný termín již není možné provádět rezervace.");

			// kontrola registrace klienta
			Poskytovatel poskytovatel = terminUdalosti.Udalost.Poskytovatel;
			if (poskytovatel.RegistraceKlientuPodlehaSchvaleni && !registraceKlientaRepository.MaKlientRegistraci(idKlienta, poskytovatel.Id))
			{
				throw new ArgumentException("Rezervace termínu je podmíněna registrací u příslušného poskytovatele služeb.");
			}

			// nalezení, popř. vytvoření, konkrétního termínu pro rezervaci
			TerminRezervace terminRezervace = terminRezervaceRepository.VratTerminRezervaceDleDataProUpravy(idTerminuUdalosti, datum);
			if (terminRezervace == null)
			{
				terminUdalostiRepository.Lock(terminUdalosti);
				terminRezervace = terminRezervaceRepository.VratTerminRezervaceDleDataProUpravy(idTerminuUdalosti, datum);
			}
			if (terminRezervace == null)
			{
				terminRezervace = new TerminRezervace(terminUdalosti, datum);
			}
			else
			{
				terminRezervace.NavysitPocetRezervaci();
			}
			terminRezervaceRepository.Uloz(terminRezervace);

			// kontrola maximálního počtu rezervací klienta pro jednoho poskytovatele
			if (rezervaceTerminuRepository.VratPocetAktualnePlatnychRezervaci(idKlienta, poskytovatel.Id) >= poskytovatel.MaximalniPocetRezervaciJednohoKlienta)
			{
				throw new ArgumentException("Byl překročen maximální počet aktuálně platných rezervací pro daného poskytovatele.");
			}

			// kontrola kolize již provedených rezervací klienta
			if (rezervaceTerminuRepository.ExistujePlatnaRezervaceProDanyCas(idKlienta, datum, datum.Add(terminUdalosti.CasTrvani.DobaTrvani)))
			{
				throw new ArgumentException("Ve vybraném termínu už máte provedenou jinou rezervaci.");
			}

			// vytvoření rezervace
			RezervaceTerminu rezervace = new RezervaceTerminu(klient, terminRezervace);
			rezervaceTerminuRepository.Uloz(rezervace);
		}

		[Transaction]
		public void ZrusitRezervaciZeStranyKlienta(int idRezervace)
		{
			RezervaceTerminu rezervace = rezervaceTerminuRepository.Vrat(idRezervace);
			Validate.NotNull(rezervace, "Není určena platná rezervace.");

			TerminRezervace terminRezervace = rezervace.Termin;

			// kontrola uzávěrky rezervací
			Validate.IsTrue(DateTime.Now < terminRezervace.Datum.Subtract(terminRezervace.TerminUdalosti.UzaverkaRezervaci), "Rezervaci není možné zrušit po uzávěrce rezervací na daný termín.");

			rezervaceTerminuRepository.Odstran(rezervace);

			// snížení počtu rezervací u danémo termínu rezervace, popř. prušení termínu rezervace, pokud byla zrušena poslední rezervace na daný termín
			terminRezervaceRepository.Lock(terminRezervace);
			if (!terminRezervace.SnizitPocetRezervaci())
			{
				terminRezervaceRepository.Odstran(terminRezervace);
			}
			else
			{
				terminRezervaceRepository.Uloz(terminRezervace);
			}
		}

		public void RegistrovatKlientaUPoskytovatele(int idKlienta, int idPoskytovatele)
		{

		}

		public void SchvalitRegistraciKlienta(int idPozadavkuNaRegistraci)
		{

		}

		public void OdmitnoutRegistraciKlienta(int idPozadavkuNaRegistraci)
		{

		}
	}
}
