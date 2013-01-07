using RezervacniSystem.Domain.Model.Klienti;
using RezervacniSystem.Domain.Model.KlientskeZpravy;
using RezervacniSystem.Domain.Model.Poskytovatele;
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
	public class SpravaUdalostiService : ISpravaUdalostiService
	{
		private readonly IUdalostRepository udalostRepository;
		private readonly IPoskytovatelRepository poskytovatelRepository;
		private readonly ITerminUdalostiRepository terminUdalostiRepository;
		private readonly UdalostFactory udalostFactory;
		private readonly IRezervaceTerminuRepository rezervaceTerminuRepository;
		private readonly IKlientskaZpravaRepository klientskaZpravaRepository;
		private readonly ITerminRezervaceRepository terminRezervaceRepository;

		public SpravaUdalostiService(IUdalostRepository udalostRepository, IPoskytovatelRepository poskytovatelRepository, ITerminUdalostiRepository terminUdalostiRepository, UdalostFactory udalostFactory, IRezervaceTerminuRepository rezervaceTerminuRepository, IKlientskaZpravaRepository klientskaZpravaRepository, ITerminRezervaceRepository terminRezervaceRepository)
		{
			this.udalostRepository = udalostRepository;
			this.poskytovatelRepository = poskytovatelRepository;
			this.terminUdalostiRepository = terminUdalostiRepository;
			this.udalostFactory = udalostFactory;
			this.rezervaceTerminuRepository = rezervaceTerminuRepository;
			this.klientskaZpravaRepository = klientskaZpravaRepository;
			this.terminRezervaceRepository = terminRezervaceRepository;
		}

		[Transaction]
		public Udalost ZalozitNovouUdalost(int idPoskytovatele, String nazev)
		{
			Udalost udalost = udalostFactory.VytvorUdalost(idPoskytovatele, nazev);
			udalostRepository.Uloz(udalost);
			return udalost;
		}

		[Transaction]
		public void ZmenitUdalost(int idUdalosti, String nazev, int maximalniPocetUcastniku, bool zverejneno, String popis)
		{
			Udalost udalost = udalostRepository.Vrat(idUdalosti);
			if (!udalost.Zverejneno && zverejneno)
			{
				poskytovatelRepository.Lock(udalost.Poskytovatel);
				int pocetUdalosti = udalostRepository.VratPocetZverejnenychUdalosti(udalost.Poskytovatel.Id);
				if (pocetUdalosti >= udalost.Poskytovatel.MaximalniPocetZverejnenychUdalosti)
				{
					throw new ArgumentException("Byl překročen maximální počet zveřejněných událostí.");
				}
			}

			udalost.Nazev = nazev;
			udalost.NastavMaximalniPocetUcastniku(maximalniPocetUcastniku);
			udalost.Zverejneno = zverejneno;
			udalost.Popis = popis;

			udalostRepository.Uloz(udalost);
		}

		[Transaction]
		public void ZrusitUdalost(int idUdalosti)
		{
			Udalost udalost = udalostRepository.Vrat(idUdalosti);
			Validate.NotNull(udalost, "Musí být určena platná událost.");

			foreach (Klient k in rezervaceTerminuRepository.VratRezervaceDleUdalosti(idUdalosti).Select(r => r.Klient).Distinct())
			{
				klientskaZpravaRepository.Uloz(new KlientskaZprava(k, "Všechny rezervace události " + udalost.Nazev + " poskytovatele " + udalost.Poskytovatel.Nazev + " byly zrušeny z důvodu zrušení události poskytovatelem."));
			}

			// zrušení rezervací
			rezervaceTerminuRepository.OdstranRezervaceDleUdalosti(idUdalosti);
			// zrušení termínů rezervací
			terminRezervaceRepository.OdstranTerminyRezervaciDleUdalosti(idUdalosti);
			// zrušení termínu události
			terminUdalostiRepository.OdstranVsechnyTerminyUdalosti(idUdalosti);

			udalostRepository.Odstran(idUdalosti);
		}
	}
}
