using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.Udalosti
{
	public interface IUdalostRepository
	{
		Udalost Vrat(int id);
		Udalost VratProUpravy(int id);
		void Uloz(Udalost udalost);
		IList<Udalost> VratUdalostiDlePoskytovatele(int idPoskytovatele);
		IList<Udalost> VratZverejneneUdalostiDlePoskytovatele(int idPoskytovatele);
		int VratPocetZverejnenychUdalosti(int idPoskytovatele);
		void Odstran(int id);
		void OdstranVsechnyUdalostiPoskytovatele(int idPoskytovatele);
	}
}
