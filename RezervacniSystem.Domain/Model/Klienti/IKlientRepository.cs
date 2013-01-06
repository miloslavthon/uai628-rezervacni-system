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
		Klient VratProUpravy(int id);
		Klient VratKlientaDleUzivatelskehoJmena(String uzivatelskeJmeno);
		int VratIdKlientaDleUzivatelskehoJmena(String uzivatelskeJmeno);
		IList<Klient> VratRegistrovaneKlienty(int idPoskytovatele);
		void Uloz(Klient klient);
	}
}
