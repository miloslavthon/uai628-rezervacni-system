using RezervacniSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.Terminy
{
	public class CasTrvani
	{
		public CasTrvani()
		{
		}

		public CasTrvani(TimeSpan cas, TimeSpan dobaTrvani)
		{
			Validate.IsTrue(cas < TimeSpan.FromDays(1), "Čas nemůže být větší než 1 den.");
			Validate.IsTrue(dobaTrvani < TimeSpan.FromDays(1), "Doba trvání nemůže být větší než 1 den.");
			Validate.IsTrue(cas.Add(dobaTrvani) < TimeSpan.FromDays(1), "Doba trvání přesahuje do dalšího dne.");

			this.Cas = cas;
			this.DobaTrvani = dobaTrvani;
		}

		public virtual TimeSpan Cas { get; set; }
		public virtual TimeSpan DobaTrvani { get; set; }

		public virtual TimeSpan Konec
		{
			get
			{
				return Cas.Add(DobaTrvani);
			}
		}

		public virtual bool KolidujeS(CasTrvani casTrvani)
		{
			return this.Cas < casTrvani.Cas ?
				this.Konec >= casTrvani.Cas :
				this.Cas <= casTrvani.Konec;
		}
	}
}
