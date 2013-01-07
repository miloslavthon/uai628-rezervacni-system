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

		public void OdstranTerminyRezervaciDleTerminuUdalosti(int idTerminuUdalosti)
		{
			CurrentSession.CreateQuery(@"
				delete TerminRezervace t
				where
					t.TerminUdalosti.Id = :idTerminuUdalosti
				")
				.SetParameter("idTerminuUdalosti", idTerminuUdalosti)
				.ExecuteUpdate();
		}

		public void OdstranTerminyRezervaciDleUdalosti(int idUdalosti)
		{
			CurrentSession.CreateQuery(@"
				delete TerminRezervace t
				where
					t.TerminUdalosti.Id in (
						select terminUdalosti.Id
						from TerminUdalosti terminUdalosti
						where terminUdalosti.Udalost.Id = :idUdalosti
					)
				")
				.SetParameter("idUdalosti", idUdalosti)
				.ExecuteUpdate();
		}

		public void OdstranTerminyRezervaciDlePoskytovatele(int idPoskytovatele)
		{
			CurrentSession.CreateQuery(@"
				delete TerminRezervace t
				where
					t.TerminUdalosti.Id in (
						select terminUdalosti.Id
						from TerminUdalosti terminUdalosti
							join terminUdalosti.Udalost udalost
						where udalost.Poskytovatel.Id = :idPoskytovatele
					)
				")
				.SetParameter("idPoskytovatele", idPoskytovatele)
				.ExecuteUpdate();
		}
	}
}
