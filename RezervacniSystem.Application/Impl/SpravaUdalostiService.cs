using RezervacniSystem.Domain.Model.Poskytovatele;
using RezervacniSystem.Domain.Model.Udalosti;
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
		private readonly UdalostFactory udalostFactory;

		public SpravaUdalostiService(IUdalostRepository udalostRepository, IPoskytovatelRepository poskytovatelRepository, UdalostFactory udalostFactory)
		{
			this.udalostRepository = udalostRepository;
			this.poskytovatelRepository = poskytovatelRepository;
			this.udalostFactory = udalostFactory;
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
				poskytovatelRepository.ReadLock(udalost.Poskytovatel);
				int pocetUdalosti = udalostRepository.VratPocetZverejnenychUdalosti(udalost.Poskytovatel.Id);
				if (pocetUdalosti > udalost.Poskytovatel.MaximalniPocetZverejnenychUdalosti)
				{
					throw new ArgumentException("Byl překročen maximální počet zveřejněných událostí.");
				}
			}

			udalost.Nazev = nazev;
			udalost.MaximalniPocetUcastniku = maximalniPocetUcastniku;
			udalost.Zverejneno = zverejneno;
			udalost.Popis = popis;

			udalostRepository.Uloz(udalost);
		}
	}
}
