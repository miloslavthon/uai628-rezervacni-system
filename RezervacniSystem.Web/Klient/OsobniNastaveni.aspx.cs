using Common.Logging;
using RezervacniSystem.Domain.Model.Klienti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RezervacniSystem.Web.Klient
{
	public partial class OsobniNastaveni : BasePage
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(OsobniNastaveni));

		public IKlientRepository KlientRepository { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			lblZpravaOUlozeni.Visible = false;
			lblChybaPriUlozeni.Visible = false;

			if (!IsPostBack)
			{
				Domain.Model.Klienti.Klient klient = KlientRepository.NajdiDleUzivatelskehoJmena(User.Identity.Name);
				if (klient != null)
				{
					ZobrazUdaje(klient);
				}
			}
		}

		protected void btnUlozit_Click(object sender, EventArgs e)
		{
			try
			{
				Domain.Model.Klienti.Klient klient;
				if (hdnIdKlienta.Value == String.Empty)
				{
					klient = new Domain.Model.Klienti.Klient(User.Identity.Name, txtJmeno.Text, txtPrijmeni.Text, txtAdresa.Text == String.Empty ? null : txtAdresa.Text);
				}
				else
				{
					klient = KlientRepository.Vrat(int.Parse(hdnIdKlienta.Value));
					klient.NastavUdaje(txtJmeno.Text, txtPrijmeni.Text, txtAdresa.Text == String.Empty ? null : txtAdresa.Text);
				}

				KlientRepository.Uloz(klient);
				ZobrazUdaje(klient);

				lblZpravaOUlozeni.Text = "Změny byly uloženy.";
				lblZpravaOUlozeni.Visible = true;
			}
			catch (ArgumentException ex)
			{
				lblChybaPriUlozeni.Text = ex.Message;
				lblChybaPriUlozeni.Visible = true;

				log.Warn("Při uložení osobního nastavení došlo k chybě.", ex);
			}
			catch (Exception ex)
			{
				lblChybaPriUlozeni.Text = "Při uložení osobního nastavení došlo k neočekávané chybě.";
				lblChybaPriUlozeni.Visible = true;

				log.Error("Při uložení osobního nastavení došlo k chybě.", ex);
			}
		}

		private void ZobrazUdaje(Domain.Model.Klienti.Klient klient)
		{
			hdnIdKlienta.Value = klient.Id.ToString();

			txtJmeno.Text = klient.Jmeno;
			txtPrijmeni.Text = klient.Prijmeni;
			txtAdresa.Text = klient.Adresa;
		}
	}
}