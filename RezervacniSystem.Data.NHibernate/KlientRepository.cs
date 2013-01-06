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

		public IList<Klient> VratRegistrovaneKlienty(int idPoskytovatele)
		{
			return CurrentSession.CreateSQLQuery(@"
					SELECT
						{Klient.*}
					FROM
						Klient
						INNER JOIN RegistraceKlienta ON RegistraceKlienta.IdKlienta = Klient.Id
					WHERE
						RegistraceKlienta.IdPoskytovatele = :idPoskytovatele
				")
				.AddEntity(typeof(Klient))
				.SetInt32("idPoskytovatele", idPoskytovatele)
				.List<Klient>();
		}
	}
}
