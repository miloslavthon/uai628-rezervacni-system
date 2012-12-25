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
			return CurrentSession.QueryOver<Udalost>().Where(u => u.Poskytovatel.Id == idPoskytovatele).OrderBy(u => u.Nazev).Asc.List();
		}

		public int VratPocetZverejnenychUdalosti(int idPoskytovatele)
		{
			return CurrentSession.QueryOver<Udalost>().Where(u => u.Poskytovatel.Id.Equals(idPoskytovatele) && u.Zverejneno).RowCount();
		}
	}
}
