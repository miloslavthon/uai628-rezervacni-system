using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.KlientskeZpravy
{
	public interface IKlientskaZpravaRepository
	{
		IList<KlientskaZprava> VratZpravy(int idKlienta);
		void Uloz(KlientskaZprava zprava);
		void Odstran(int id);
	}
}
