using RezervacniSystem.Domain.Model.Terminy;
using RezervacniSystem.Domain.Model.Udalosti;
using Spring.Transaction.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Application.Impl
{
	public class SpravaTerminuService : ISpravaTerminuService
	{
		private readonly IUdalostRepository udalostRepository;
		private readonly ITerminUdalostiRepository terminUdalostiRepository;

		public SpravaTerminuService(IUdalostRepository udalostRepository, ITerminUdalostiRepository terminUdalostiRepository)
		{
			this.udalostRepository = udalostRepository;
			this.terminUdalostiRepository = terminUdalostiRepository;
		}

		[Transaction]
		public TerminUdalosti ZverejnitJednorazovyTermin(int idUdalosti, DateTime datum, TimeSpan cas, TimeSpan dobaTrvani, TimeSpan uzaverkaRezervaci, String poznamka)
		{
			TerminUdalosti termin = new TerminUdalosti(udalostRepository.VratProUpravy(idUdalosti), datum, new CasTrvani(cas, dobaTrvani), uzaverkaRezervaci, poznamka);
			ZkontrolovatKolizeAUlozit(termin);
			return termin;
		}

		[Transaction]
		public TerminUdalosti ZverejnitOpakovanyTermin(int idUdalosti, DenVTydnu den, TimeSpan cas, DateTime platnyDo, TimeSpan dobaTrvani, TimeSpan uzaverkaRezervaci, String poznamka)
		{
			TerminUdalosti termin = new TerminUdalosti(udalostRepository.VratProUpravy(idUdalosti), den, platnyDo, new CasTrvani(cas, dobaTrvani), uzaverkaRezervaci, poznamka);
			ZkontrolovatKolizeAUlozit(termin);
			return termin;
		}

		[Transaction]
		public void ZrusitTermin(int idTerminu)
		{
			// doplnit zrušení případných rezervací

			terminUdalostiRepository.Odstran(idTerminu);
		}

		private void ZkontrolovatKolizeAUlozit(TerminUdalosti termin)
		{
			foreach (TerminUdalosti t in terminUdalostiRepository.VratAktualnePlatneTerminyDleUdalosti(termin.Udalost.Id))
			{
				if (termin.KolidujeS(t))
				{
					throw new ArgumentException("Termín koliduje s některým z již zveřejněných termínů.");
				}
			}

			terminUdalostiRepository.Uloz(termin);
		}
	}
}
