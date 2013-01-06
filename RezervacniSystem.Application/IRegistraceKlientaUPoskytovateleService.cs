using RezervacniSystem.Domain.Model.Klienti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Application
{
	public interface IRegistraceKlientaUPoskytovateleService
	{
		void RegistrovatKlientaUPoskytovatele(int idKlienta, int idPoskytovatele);
		Klient SchvalitRegistraciKlienta(int idPozadavkuNaRegistraci);
		Klient OdmitnoutRegistraciKlienta(int idPozadavkuNaRegistraci);
		void ZrusitRegistraciKlienta(int idKlienta, int idPoskytovatele);
	}
}
