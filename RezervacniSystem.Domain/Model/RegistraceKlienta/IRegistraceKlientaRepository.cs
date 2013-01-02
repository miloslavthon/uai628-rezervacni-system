using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.RegistraceKlienta
{
	public interface IRegistraceKlientaRepository
	{
		bool MaKlientRegistraci(int idKlienta, int idPoskytovatele);
		void UlozRegistraciKlienta(int idKlienta, int idPoskytovatele);
		void OdstranRegistraciKlienta(int idKlienta, int idPoskytovatele);
	}
}
