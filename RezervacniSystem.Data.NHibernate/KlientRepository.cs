using RezervacniSystem.Domain.Model.Klienti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Data.NHibernate
{
	public class KlientRepository : DomainObjectRepository<Klient>, IKlientRepository
	{
		public Klient VratKlientaDleUzivatelskehoJmena(String uzivatelskeJmeno)
		{
			return Query.Where(k => k.UzivatelskeJmeno == uzivatelskeJmeno).SingleOrDefault();
		}

		public int VratIdKlientaDleUzivatelskehoJmena(String uzivatelskeJmeno)
		{
			return Query.Where(k => k.UzivatelskeJmeno == uzivatelskeJmeno).Select(k => k.Id).SingleOrDefault<int>();
		}
	}
}
