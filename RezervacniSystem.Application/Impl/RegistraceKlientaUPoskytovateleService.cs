using RezervacniSystem.Domain.Model.Klienti;
using RezervacniSystem.Domain.Model.KlientskeZpravy;
using RezervacniSystem.Domain.Model.Poskytovatele;
using RezervacniSystem.Domain.Model.PozadavkyNaRegistraciKlientu;
using RezervacniSystem.Domain.Model.RegistraceKlienta;
using RezervacniSystem.Infrastructure;
using Spring.Transaction.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Application.Impl
{
	public class RegistraceKlientaUPoskytovateleService : IRegistraceKlientaUPoskytovateleService
	{
		private readonly IPoskytovatelRepository poskytovatelRepository;
		private readonly IKlientRepository klientRepository;
		private readonly IRegistraceKlientaRepository registraceKlientaRepository;
		private readonly IPozadavekNaRegistraciKlientaRepository pozadavekNaRegistraciKlientaRepository;
		private readonly IKlientskaZpravaRepository klientskaZpravaRepository;

		public RegistraceKlientaUPoskytovateleService(IPoskytovatelRepository poskytovatelRepository, IKlientRepository klientRepository, IRegistraceKlientaRepository registraceKlientaRepository, IPozadavekNaRegistraciKlientaRepository pozadavekNaRegistraciKlientaRepository, IKlientskaZpravaRepository klientskaZpravaRepository)
		{
			this.poskytovatelRepository = poskytovatelRepository;
			this.klientRepository = klientRepository;
			this.registraceKlientaRepository = registraceKlientaRepository;
			this.pozadavekNaRegistraciKlientaRepository = pozadavekNaRegistraciKlientaRepository;
			this.klientskaZpravaRepository = klientskaZpravaRepository;
		}

		[Transaction]
		public void RegistrovatKlientaUPoskytovatele(int idKlienta, int idPoskytovatele)
		{
			Klient klient = klientRepository.VratProUpravy(idKlienta);

			if (registraceKlientaRepository.MaKlientRegistraci(idKlienta, idPoskytovatele))
			{
				throw new ArgumentException("Registrace u daného poskytovatele již byla provedena.");
			}
			else if (pozadavekNaRegistraciKlientaRepository.VratPozadavekDleKlientaAPoskytovatele(idKlienta, idPoskytovatele) != null)
			{
				throw new ArgumentException("Požadavek na registraci u daného poskytovatele je již vytvořen.");
			}
			else
			{
				pozadavekNaRegistraciKlientaRepository.Uloz(new PozadavekNaRegistraciKlienta(klient, poskytovatelRepository.Vrat(idPoskytovatele)));
			}
		}

		[Transaction]
		public Klient SchvalitRegistraciKlienta(int idPozadavkuNaRegistraci)
		{
			PozadavekNaRegistraciKlienta pozadavek = pozadavekNaRegistraciKlientaRepository.Vrat(idPozadavkuNaRegistraci);
			Validate.NotNull(pozadavek, "Musí být určen platný požadavek na registraci.");

			registraceKlientaRepository.UlozRegistraciKlienta(pozadavek.Klient.Id, pozadavek.Poskytovatel.Id);
			pozadavekNaRegistraciKlientaRepository.Odstran(idPozadavkuNaRegistraci);

			klientskaZpravaRepository.Uloz(new KlientskaZprava(pozadavek.Klient, "Registrace u poskytovatele " + pozadavek.Poskytovatel.Nazev + " byla provedena."));

			return pozadavek.Klient;
		}

		[Transaction]
		public Klient OdmitnoutRegistraciKlienta(int idPozadavkuNaRegistraci)
		{
			PozadavekNaRegistraciKlienta pozadavek = pozadavekNaRegistraciKlientaRepository.Vrat(idPozadavkuNaRegistraci);
			Validate.NotNull(pozadavek, "Musí být určen platný požadavek na registraci.");

			registraceKlientaRepository.OdstranRegistraciKlienta(pozadavek.Klient.Id, pozadavek.Poskytovatel.Id);
			pozadavekNaRegistraciKlientaRepository.Odstran(idPozadavkuNaRegistraci);

			klientskaZpravaRepository.Uloz(new KlientskaZprava(pozadavek.Klient, "Registrace u poskytovatele " + pozadavek.Poskytovatel.Nazev + " byla zamítnuta."));

			return pozadavek.Klient;
		}

		[Transaction]
		public void ZrusitRegistraciKlienta(int idKlienta, int idPoskytovatele)
		{
			Klient klient = klientRepository.Vrat(idKlienta);
			Poskytovatel poskytovatel = poskytovatelRepository.Vrat(idPoskytovatele);
			Validate.NotNull(klient, "Musí být určen platný klient.");
			Validate.NotNull(poskytovatel, "Musí být určen platný poskytovatel.");

			registraceKlientaRepository.OdstranRegistraciKlienta(idKlienta, idPoskytovatele);

			klientskaZpravaRepository.Uloz(new KlientskaZprava(klient, "Registrace u poskytovatele " + poskytovatel.Nazev + " Vám byla zrušena."));
		}
	}
}
