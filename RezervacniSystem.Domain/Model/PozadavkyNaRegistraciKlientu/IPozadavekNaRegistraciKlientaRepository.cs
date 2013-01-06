using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.PozadavkyNaRegistraciKlientu
{
	public interface IPozadavekNaRegistraciKlientaRepository
	{
		PozadavekNaRegistraciKlienta Vrat(int id);
		PozadavekNaRegistraciKlienta VratPozadavekDleKlientaAPoskytovatele(int idKlienta, int idPoskytovatele);
		IList<PozadavekNaRegistraciKlienta> NajdiPozadavkyDlePoskytovatele(int idPoskytovatele);
		void Uloz(PozadavekNaRegistraciKlienta pozadavek);
		void Odstran(int id);
	}
}
