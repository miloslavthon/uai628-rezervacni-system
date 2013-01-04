using RezervacniSystem.Domain.Model.Terminy;
using RezervacniSystem.Domain.Shared;
using RezervacniSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.TerminyRezervaci
{
	public class TerminRezervace : DomainObject
	{
		public TerminRezervace()
		{
		}

		public TerminRezervace(TerminUdalosti terminUdalosti, DateTime datum)
		{
			Validate.NotNull(terminUdalosti, "Musí být určen termín události");
			Validate.IsTrue(terminUdalosti.OdpovidaDatumTerminu(datum), "Zadané datum neodpovídá zadanému termínu.");

			this.TerminUdalosti = terminUdalosti;
			this.Datum = datum;
			this.Konec = datum.Add(terminUdalosti.CasTrvani.DobaTrvani);
			this.PocetRezervaci = 1;
		}

		public virtual TerminUdalosti TerminUdalosti { get; set; }
		public virtual DateTime Datum { get; set; }
		public virtual DateTime Konec { get; set; }
		public virtual int PocetRezervaci { get; set; }

		public virtual void NavysitPocetRezervaci()
		{
			if (TerminUdalosti.Udalost.MaximalniPocetUcastniku == 0 || PocetRezervaci < TerminUdalosti.Udalost.MaximalniPocetUcastniku)
			{
				PocetRezervaci++;
			}
			else
			{
				throw new ArgumentException("Byl dosažen maximální počet rezervací pro daný termín.");
			}
		}

		/// <summary>
		/// Sníží počet rezervací na daný termín a vrátí příznak, zda ještě nějaké rezervace tohoto termínu existují
		/// </summary>
		/// <returns></returns>
		public virtual bool SnizitPocetRezervaci()
		{
			if (PocetRezervaci > 0)
			{
				PocetRezervaci--;
			}

			return PocetRezervaci > 0;
		}
	}
}
