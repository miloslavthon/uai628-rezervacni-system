using RezervacniSystem.Domain.Model.KlientskeZpravy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Data.NHibernate
{
	public class KlientskaZpravaRepository : DomainObjectRepository<KlientskaZprava>, IKlientskaZpravaRepository
	{
		public IList<KlientskaZprava> VratZpravy(int idKlienta)
		{
			return Query.Where(z => z.Klient.Id == idKlienta).OrderBy(z => z.Datum).Asc.List();
		}
	}
}
