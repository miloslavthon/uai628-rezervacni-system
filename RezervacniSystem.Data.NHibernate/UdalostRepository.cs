using RezervacniSystem.Domain.Model.Udalosti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Data.NHibernate
{
	public class UdalostRepository : NHibernateRepository<Udalost>, IUdalostRepository
	{
		public IList<Udalost> VratUdalostiDlePoskytovatele(int idPoskytovatele)
		{
			return Query.Where(u => u.Poskytovatel.Id == idPoskytovatele).OrderBy(u => u.Nazev).Asc.List();
		}

		public IList<Udalost> VratZverejneneUdalostiDlePoskytovatele(int idPoskytovatele)
		{
			return Query.Where(u => u.Poskytovatel.Id == idPoskytovatele && u.Zverejneno).OrderBy(u => u.Nazev).Asc.List();
		}

		public int VratPocetZverejnenychUdalosti(int idPoskytovatele)
		{
			return Query.Where(u => u.Poskytovatel.Id == idPoskytovatele && u.Zverejneno).RowCount();
		}

		public void OdstranVsechnyUdalostiPoskytovatele(int idPoskytovatele)
		{
			CurrentSession.CreateQuery("delete Udalost u where u.Poskytovatel.Id = :id")
				.SetParameter("id", idPoskytovatele)
				.ExecuteUpdate();
		}
	}
}
