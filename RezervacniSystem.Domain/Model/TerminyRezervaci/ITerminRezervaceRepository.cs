using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.TerminyRezervaci
{
	public interface ITerminRezervaceRepository
	{
		TerminRezervace Vrat(int id);
		TerminRezervace VratTerminRezervaceDleDataProUpravy(int idTerminuUdalosti, DateTime datum);
		IList<TerminRezervace> NajdiTerminyRezervaci(int idUdalosti, DateTime datumOd, DateTime datumDo);
		void Uloz(TerminRezervace terminRezervace);
		void Lock(TerminRezervace terminRezervace);
		void Odstran(TerminRezervace terminRezervace);
		void OdstranTerminyRezervaciDleTerminuUdalosti(int idTerminuUdalosti);
		void OdstranTerminyRezervaciDleUdalosti(int idUdalosti);
		void OdstranTerminyRezervaciDlePoskytovatele(int idPoskytovatele);
	}
}
