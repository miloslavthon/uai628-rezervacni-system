using RezervacniSystem.Domain.Model.Klienti;
using RezervacniSystem.Domain.Model.KlientskeZpravy;
using RezervacniSystem.Domain.Model.Poskytovatele;
using RezervacniSystem.Domain.Model.RegistraceKlienta;
using RezervacniSystem.Domain.Model.Rezervace;
using RezervacniSystem.Domain.Model.Terminy;
using RezervacniSystem.Domain.Model.TerminyRezervaci;
using RezervacniSystem.Domain.Model.Udalosti;
using RezervacniSystem.Infrastructure;
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
		private readonly IRezervaceTerminuRepository rezervaceTerminuRepository;
		private readonly IKlientskaZpravaRepository klientskaZpravaRepository;
		private readonly ITerminRezervaceRepository terminRezervaceRepository;
		private readonly IRegistraceKlientaRepository registraceKlientaRepository;
		private readonly IKlientRepository klientRepository;

		public SpravaPoskytovateluService(IPoskytovatelRepository poskytovatelRepository, IUdalostRepository udalostRepository, ITerminUdalostiRepository terminUdalostiRepository, IRezervaceTerminuRepository rezervaceTerminuRepository, IKlientskaZpravaRepository klientskaZpravaRepository, ITerminRezervaceRepository terminRezervaceRepository, IRegistraceKlientaRepository registraceKlientaRepository, IKlientRepository klientRepository)
		{
			this.poskytovatelRepository = poskytovatelRepository;
			this.udalostRepository = udalostRepository;
			this.terminUdalostiRepository = terminUdalostiRepository;
			this.rezervaceTerminuRepository = rezervaceTerminuRepository;
			this.klientskaZpravaRepository = klientskaZpravaRepository;
			this.terminRezervaceRepository = terminRezervaceRepository;
			this.registraceKlientaRepository = registraceKlientaRepository;
			this.klientRepository = klientRepository;
		}

		[Transaction]
		public void ZrusitPoskytovatele(int idPoskytovatele)
		{
			Poskytovatel poskytovatel = poskytovatelRepository.Vrat(idPoskytovatele);
			Validate.NotNull(poskytovatel, "Musí být určen platný poskytovatel.");

			foreach (Klient k in rezervaceTerminuRepository.VratRezervaceDlePoskytovatele(idPoskytovatele).Select(r => r.Klient).Distinct())
			{
				klientskaZpravaRepository.Uloz(new KlientskaZprava(k, "Všechny rezervace u poskytovatele služeb " + poskytovatel.Nazev + " byly zrušeny z důvodu zániku poskytovatele."));
			}

			// zrušení rezervací
			rezervaceTerminuRepository.OdstranRezervaceDlePoskytovatele(idPoskytovatele);
			// zrušení termínů rezervací
			terminRezervaceRepository.OdstranTerminyRezervaciDlePoskytovatele(idPoskytovatele);
			// zrušení registrací klientů
			registraceKlientaRepository.ZrusitRegistraceKlientuDlePoskytovatele(idPoskytovatele);

			terminUdalostiRepository.OdstranVsechnyTerminyUdalostiProPoskytovatele(idPoskytovatele);
			udalostRepository.OdstranVsechnyUdalostiPoskytovatele(idPoskytovatele);
			poskytovatelRepository.Odstran(idPoskytovatele);
		}
	}
}
