using RezervacniSystem.Domain.Model.Poskytovatele;
using RezervacniSystem.Domain.Model.Terminy;
using RezervacniSystem.Domain.Model.Udalosti;
using Spring.Transaction.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Application.Impl
{
	public class SpravaPoskytovateluService : ISpravaPoskytovateluService
	{
		private readonly IPoskytovatelRepository poskytovatelRepository;
		private readonly IUdalostRepository udalostRepository;
		private readonly ITerminUdalostiRepository terminUdalostiRepository;

		public SpravaPoskytovateluService(IPoskytovatelRepository poskytovatelRepository, IUdalostRepository udalostRepository, ITerminUdalostiRepository terminUdalostiRepository)
		{
			this.poskytovatelRepository = poskytovatelRepository;
			this.udalostRepository = udalostRepository;
			this.terminUdalostiRepository = terminUdalostiRepository;
		}

		[Transaction]
		public void ZrusitPoskytovatele(int idPoskytovatele)
		{
			// doplnit zrušení případných rezervací

			terminUdalostiRepository.OdstranVsechnyTerminyUdalostiProPoskytovatele(idPoskytovatele);
			udalostRepository.OdstranVsechnyUdalostiPoskytovatele(idPoskytovatele);
			poskytovatelRepository.Odstran(idPoskytovatele);
		}
	}
}
