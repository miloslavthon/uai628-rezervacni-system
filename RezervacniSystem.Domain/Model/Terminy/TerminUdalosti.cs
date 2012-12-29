using RezervacniSystem.Domain.Model.Udalosti;
using RezervacniSystem.Domain.Shared;
using RezervacniSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.Terminy
{
	public class TerminUdalosti : DomainObject
	{
		public TerminUdalosti()
		{
		}

		public TerminUdalosti(Udalost udalost, DateTime datum, CasTrvani casTrvani, TimeSpan uzaverkaRezervaci, String poznamka)
			: this(udalost, casTrvani, uzaverkaRezervaci, poznamka)
		{
			Validate.IsTrue(datum > DateTime.Now, "Zadané datum již nastalo.");
			Validate.IsTrue(DateTime.Now < datum.Subtract(uzaverkaRezervaci), "Čas uzávěrky rezervací pro zadaný termín již nastal.");

			this.Typ = TypTerminu.JEDNORAZOVY;
			this.Datum = datum;
		}

		public TerminUdalosti(Udalost udalost, DenVTydnu den, DateTime platnyDo, CasTrvani casTrvani, TimeSpan uzaverkaRezervaci, String poznamka)
			: this(udalost, casTrvani, uzaverkaRezervaci, poznamka)
		{
			Validate.IsTrue(platnyDo > DateTime.Now, "Platnost opakovaného termínu již skončila.");

			KontrolaOpakovanehoTerminu(den, platnyDo, casTrvani.Cas, uzaverkaRezervaci);

			this.Typ = TypTerminu.OPAKOVANY;
			this.Den = den;
			this.PlatnyDo = platnyDo;
		}

		private TerminUdalosti(Udalost udalost, CasTrvani casTrvani, TimeSpan uzaverkaRezervaci, String poznamka)
		{
			Validate.NotNull(udalost, "Pro vytvoření termínu musí být určena událost.");
			Validate.IsTrue(uzaverkaRezervaci < TimeSpan.FromDays(99), "Doba trvání nemůže být větší než 1 den.");

			this.Udalost = udalost;
			this.CasTrvani = casTrvani;
			this.UzaverkaRezervaci = uzaverkaRezervaci;
			this.Poznamka = poznamka;
		}

		public virtual Udalost Udalost { get; set; }
		public virtual TypTerminu Typ { get; set; }
		public virtual DateTime? Datum { get; set; }
		public virtual DenVTydnu? Den { get; set; }
		public virtual DateTime? PlatnyDo { get; set; }
		public virtual CasTrvani CasTrvani { get; set; }
		public virtual TimeSpan UzaverkaRezervaci { get; set; }
		public virtual String Poznamka { get; set; }

		public virtual bool KolidujeS(TerminUdalosti termin)
		{
			if (Typ != termin.Typ)
			{
				throw new ArgumentException("Není možné ověřit kolize termínů odlišných typů.");
			}

			if (Typ == TypTerminu.JEDNORAZOVY && Datum.Value.Date != termin.Datum.Value.Date ||
				Typ == TypTerminu.OPAKOVANY && Den.Value != termin.Den.Value)
			{
				return false;
			}

			return CasTrvani.KolidujeS(termin.CasTrvani);
		}

		private static void KontrolaOpakovanehoTerminu(DenVTydnu denVTydnu, DateTime platnyDo, TimeSpan cas, TimeSpan uzaverkaRezervaci)
		{
			int rozdilDnuVTydnu = platnyDo.VratDenVTydnu() - denVTydnu;
			if (rozdilDnuVTydnu < 0)
			{
				rozdilDnuVTydnu += 7;
			}

			DateTime posledniMoznyTermin = platnyDo.Date.AddDays(-rozdilDnuVTydnu).Add(cas);
			DateTime dnes = DateTime.Now;

			if (posledniMoznyTermin < dnes)
			{
				throw new ArgumentException("Zadaný termín již nemůže před koncem platnosti nastat.");
			}
			else if (posledniMoznyTermin.Subtract(uzaverkaRezervaci) < dnes)
			{
				throw new ArgumentException("Zadaný termín již není možné rezervovat před koncem platnosti.");
			}
		}
	}
}
