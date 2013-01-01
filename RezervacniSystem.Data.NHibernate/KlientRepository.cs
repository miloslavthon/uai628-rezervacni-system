using RezervacniSystem.Domain.Model.Klienti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Data.NHibernate
{
	public class KlientRepository : NHibernateRepository<Klient>, IKlientRepository
	{
		public Klient NajdiDleUzivatelskehoJmena(String uzivatelskeJmeno)
		{
			return Query.Where(k => k.UzivatelskeJmeno == uzivatelskeJmeno).SingleOrDefault();
		}
	}
}
