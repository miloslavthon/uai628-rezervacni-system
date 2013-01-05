using RezervacniSystem.Domain.Model.Terminy;
using RezervacniSystem.Domain.Model.TerminyRezervaci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Data.NHibernate
{
	public class TerminRezervaceRepository : DomainObjectRepository<TerminRezervace>, ITerminRezervaceRepository
	{
		public TerminRezervace VratTerminRezervaceDleDataProUpravy(int idTerminuUdalosti, DateTime datum)
		{
			return Query.Where(t => t.TerminUdalosti.Id == idTerminuUdalosti && t.Datum == datum).Lock().Upgrade.SingleOrDefault();
		}

		public IList<TerminRezervace> NajdiTerminyRezervaci(int idUdalosti, DateTime datumOd, DateTime datumDo)
		{
			return Query.Where(t => t.Datum >= datumOd && t.Datum < datumDo)
				.JoinQueryOver<TerminUdalosti>(t => t.TerminUdalosti)
				.And(t => t.Udalost.Id == idUdalosti)
				.List();
		}
	}
}
