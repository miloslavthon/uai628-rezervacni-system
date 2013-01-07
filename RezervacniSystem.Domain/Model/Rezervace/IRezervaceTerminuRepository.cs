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
		IList<RezervaceTerminu> VratRezervaceDleTerminuRezervace(int idTerminuRezervace);
		IList<RezervaceTerminu> VratRezervaceDleTerminuUdalosti(int idTerminuUdalosti);
		IList<RezervaceTerminu> VratRezervaceDleUdalosti(int idUdalosti);
		IList<RezervaceTerminu> VratRezervaceDlePoskytovatele(int idPoskytovatele);
		int VratPocetAktualnePlatnychRezervaci(int idKlienta, int idPoskytovatele);
		bool ExistujePlatnaRezervaceProDanyCas(int idKlienta, DateTime datumOd, DateTime datumDo);
		void Uloz(RezervaceTerminu rezervaceTerminu);
		void Odstran(RezervaceTerminu rezervace);
		void OdstranRezervaceDleTerminuUdalosti(int idTerminuUdalosti);
		void OdstranRezervaceDleUdalosti(int idUdalosti);
		void OdstranRezervaceDlePoskytovatele(int idPoskytovatele);
	}
}
