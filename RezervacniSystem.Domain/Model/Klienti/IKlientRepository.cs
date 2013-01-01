using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.Klienti
{
	public interface IKlientRepository
	{
		Klient Vrat(int id);
		Klient NajdiDleUzivatelskehoJmena(String uzivatelskeJmeno);
		void Uloz(Klient klient);
	}
}
