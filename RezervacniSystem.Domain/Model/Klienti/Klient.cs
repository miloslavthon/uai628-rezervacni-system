using RezervacniSystem.Domain.Shared;
using RezervacniSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Model.Klienti
{
	public class Klient : DomainObject
	{
		public Klient()
		{
		}

		public Klient(String uzivatelskeJmeno, String jmeno, String prijmeni, String adresa)
		{
			Validate.NotNullOrEmpty(uzivatelskeJmeno, "Klient musí mít vyplněné uživatelské jméno.");
			this.UzivatelskeJmeno = uzivatelskeJmeno;
			NastavUdaje(jmeno, prijmeni, adresa);
		}

		public virtual String UzivatelskeJmeno { get; set; }
		public virtual String Jmeno { get; set; }
		public virtual String Prijmeni { get; set; }
		public virtual String Adresa { get; set; }

		public virtual void NastavUdaje(String jmeno, String prijmeni, String adresa)
		{
			Validate.NotNullOrEmpty(jmeno, "Klient musí mít vyplněné jméno.");
			Validate.NotNullOrEmpty(prijmeni, "Klient musí mít vyplněné příjmeni.");

			Jmeno = jmeno;
			Prijmeni = prijmeni;
			Adresa = adresa;
		}
	}
}
