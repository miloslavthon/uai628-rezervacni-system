using RezervacniSystem.Domain.Model.Klienti;
using RezervacniSystem.Domain.Model.PozadavkyNaRegistraciKlientu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Data.NHibernate
{
	public class PozadavekNaRegistraciKlientaRepository : DomainObjectRepository<PozadavekNaRegistraciKlienta>, IPozadavekNaRegistraciKlientaRepository
	{
		public PozadavekNaRegistraciKlienta VratPozadavekDleKlientaAPoskytovatele(int idKlienta, int idPoskytovatele)
		{
			return Query.Where(p => p.Klient.Id == idKlienta && p.Poskytovatel.Id == idPoskytovatele).SingleOrDefault();
		}

		public IList<PozadavekNaRegistraciKlienta> NajdiPozadavkyDlePoskytovatele(int idPoskytovatele)
		{
			return Query.Where(p => p.Poskytovatel.Id == idPoskytovatele).JoinQueryOver<Klient>(p => p.Klient).List();
		}
	}
}
