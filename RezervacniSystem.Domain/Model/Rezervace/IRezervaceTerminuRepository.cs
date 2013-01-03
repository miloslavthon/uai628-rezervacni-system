using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.Rezervace
{
	public interface IRezervaceTerminuRepository
	{
		IList<PrehledRezervace> VratRezervace(int idKlienta, bool pouzeAktualnePlatne);
		int VratPocetAktualnePlatnychRezervaci(int idKlienta, int idPoskytovatele);
		bool ExistujePlatnaRezervaceProDanyCas(int idKlienta, DateTime datumOd, DateTime datumDo);
		void Uloz(RezervaceTerminu rezervaceTerminu);
	}
}
