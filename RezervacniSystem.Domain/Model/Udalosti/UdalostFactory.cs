using RezervacniSystem.Domain.Model.Poskytovatele;
using RezervacniSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.Udalosti
{
	public class UdalostFactory
	{
		private readonly IPoskytovatelRepository poskytovatelRepository;

		public UdalostFactory(IPoskytovatelRepository poskytovatelRepository)
		{
			this.poskytovatelRepository = poskytovatelRepository;
		}

		public Udalost VytvorUdalost(int idPoskytovatele, String nazev)
		{
			Poskytovatel poskytovatel = poskytovatelRepository.Vrat(idPoskytovatele);
			Udalost udalost = new Udalost(poskytovatel, nazev);
			udalost.OpakovanyTermin = poskytovatel.TypRezervace.UdalostiMajiOpakovanyTermin;
			udalost.MaximalniPocetUcastniku = poskytovatel.TypRezervace.UdalostiProViceOsob ? 0 : 1;
			return udalost;
		}
	}
}
