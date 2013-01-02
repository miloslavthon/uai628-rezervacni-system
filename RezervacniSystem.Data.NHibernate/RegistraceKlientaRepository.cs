using NHibernate;
using RezervacniSystem.Domain.Model.RegistraceKlienta;
using RezervacniSystem.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Data.NHibernate
{
	public class RegistraceKlientaRepository : NHibernateRepository, IRegistraceKlientaRepository
	{
		public bool MaKlientRegistraci(int idKlienta, int idPoskytovatele)
		{
			return CurrentSession.CreateSQLQuery("SELECT IdKlienta FROM RegistraceKlienta WHERE idKlienta = :idKlienta AND idPoskytovatele = :idPoskytovatele")
				.AddScalar("IdKlienta", NHibernateUtil.Int32)
				.SetInt32("idKlienta", idKlienta)
				.SetInt32("idPoskytovatele", idPoskytovatele)
				.UniqueResult<int>() == idKlienta;
		}

		public void UlozRegistraciKlienta(int idKlienta, int idPoskytovatele)
		{
			CurrentSession.CreateSQLQuery("INSERT INTO RegistraceKlienta VALUES (:idKlienta, :idPoskytovatele)")
				.SetInt32("idKlienta", idKlienta)
				.SetInt32("idPoskytovatele", idPoskytovatele)
				.ExecuteUpdate();
		}

		public void OdstranRegistraciKlienta(int idKlienta, int idPoskytovatele)
		{
			CurrentSession.CreateSQLQuery("DELETE FROM RegistraceKlienta WHERE idKlienta = :idKlienta AND idPoskytovatele = :idPoskytovatele")
				.SetInt32("idKlienta", idKlienta)
				.SetInt32("idPoskytovatele", idPoskytovatele)
				.ExecuteUpdate();
		}
	}
}
