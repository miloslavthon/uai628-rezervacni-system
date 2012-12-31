using RezervacniSystem.Domain.Model.Poskytovatele;
using RezervacniSystem.Domain.Model.Terminy;
using RezervacniSystem.Domain.Model.Udalosti;
using RezervacniSystem.Infrastructure;
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

		public RezervaceService(IPoskytovatelRepository poskytovatelRepository, IUdalostRepository udalostRepository, ITerminUdalostiRepository terminUdalostiRepository)
		{
			this.poskytovatelRepository = poskytovatelRepository;
			this.udalostRepository = udalostRepository;
			this.terminUdalostiRepository = terminUdalostiRepository;
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
					if (t.Datum.Value > datumOd && t.Datum.Value < datumDo)
					{
						volneTerminy.Add(new Tuple<DateTime, TerminUdalosti>(t.Datum.Value.Add(t.CasTrvani.Cas), t));
					}
				}
			}

			volneTerminy.Sort((a, b) => a.Item1.CompareTo(b.Item1));

			return volneTerminy;
		}

		public void RezervovatTermin(int idTerminuUdalosti, DateTime datum)
		{
			throw new ArgumentException("Rezervace termínu není prozatím k dispozici.");
		}
	}
}
