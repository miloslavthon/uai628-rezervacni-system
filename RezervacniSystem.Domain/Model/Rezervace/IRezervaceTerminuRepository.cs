using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.Rezervace
{
	public interface IRezervaceTerminuRepository
	{
		RezervaceTerminu Vrat(int idRezervace);
		IList<PrehledRezervace> VratRezervace(int idKlienta, bool pouzeAktualnePlatne);
		IList<RezervaceTerminu> VratRezervace(int idTerminuRezervace);
		int VratPocetAktualnePlatnychRezervaci(int idKlienta, int idPoskytovatele);
		bool ExistujePlatnaRezervaceProDanyCas(int idKlienta, DateTime datumOd, DateTime datumDo);
		void Uloz(RezervaceTerminu rezervaceTerminu);
		void Odstran(RezervaceTerminu rezervace);
	}
}
