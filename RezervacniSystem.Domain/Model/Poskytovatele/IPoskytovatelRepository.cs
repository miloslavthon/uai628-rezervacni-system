using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.Poskytovatele
{
	public interface IPoskytovatelRepository
	{
		Poskytovatel Vrat(int id);
		IList<Poskytovatel> VratVse();
		void Uloz(Poskytovatel poskytovatel);
		IList<Poskytovatel> VratDleNazvu(String nazev);
		int VratIdPoskytovateleDleUzivatelskehoJmena(String login);
		void Odstran(int id);
		void ReadLock(Poskytovatel poskytovatel);
	}
}
