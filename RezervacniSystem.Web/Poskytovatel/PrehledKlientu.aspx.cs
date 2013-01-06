using Common.Logging;
using RezervacniSystem.Application;
using RezervacniSystem.Domain.Model.Klienti;
using RezervacniSystem.Domain.Model.PozadavkyNaRegistraciKlientu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RezervacniSystem.Web.Poskytovatel
{
	public partial class PrehledKlientu : BasePage
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(PrehledKlientu));

		protected IKlientRepository KlientRepository { get; set; }
		protected IPozadavekNaRegistraciKlientaRepository PozadavekNaRegistraciKlientaRepository { get; set; }
		protected IRegistraceKlientaUPoskytovateleService RegistraceKlientaUPoskytovateleService { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			lblZpravaORegistraci.Visible = false;
			lblChybaPriRegistraci.Visible = false;

			if (!IsPostBack)
			{
				ZobrazitPozadavkyNaRegistraci();
				ZobrazitRegistrovaneKlienty();
			}
		}

		protected void gvPozadavkyNaRegistraci_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType != DataControlRowType.DataRow)
			{
				return;
			}

			LinkButton lnkSchvalit = (LinkButton)e.Row.Cells[3].Controls[0];
			lnkSchvalit.OnClientClick = "if (!confirm('Opravdu chcete schválit vybraný požadavek na registraci?')) { return false; }";

			LinkButton lnkZamitnout = (LinkButton)e.Row.Cells[4].Controls[0];
			lnkZamitnout.OnClientClick = "if (!confirm('Opravdu chcete zamítnout vybraný požadavek na registraci?')) { return false; }";
		}

		protected void gvPozadavkyNaRegistraci_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "Schvalit")
			{
				int idPozadavku = (int)gvPozadavkyNaRegistraci.DataKeys[int.Parse((String)e.CommandArgument)].Value;

				try
				{
					Domain.Model.Klienti.Klient klient = RegistraceKlientaUPoskytovateleService.SchvalitRegistraciKlienta(idPozadavku);

					lblZpravaORegistraci.Visible = true;
					lblZpravaORegistraci.Text = "Požadavek na registraci klienta " + klient.CeleJmeno + " byl schválen.";

					ZobrazitPozadavkyNaRegistraci();
					ZobrazitRegistrovaneKlienty();
				}
				catch (ArgumentException ex)
				{
					lblChybaPriRegistraci.Text = ex.Message;
					lblChybaPriRegistraci.Visible = true;
					log.Warn("Při schválení požadavku na registraci klienta u poskytovatele došlo k chybě.", ex);
				}
				catch (Exception ex)
				{
					lblChybaPriRegistraci.Text = "Při vytváření požadavku na registraci klienta u poskytovatele došlo k neočekávané chybě.";
					lblChybaPriRegistraci.Visible = true;
					log.Error("Při schválení požadavku na registraci klienta u poskytovatele došlo k chybě.", ex);
				}
			}
			else if (e.CommandName == "Zamitnout")
			{
				int idPozadavku = (int)gvPozadavkyNaRegistraci.DataKeys[int.Parse((String)e.CommandArgument)].Value;

				try
				{
					Domain.Model.Klienti.Klient klient = RegistraceKlientaUPoskytovateleService.OdmitnoutRegistraciKlienta(idPozadavku);

					lblZpravaORegistraci.Visible = true;
					lblZpravaORegistraci.Text = "Požadavek na registraci klienta " + klient.CeleJmeno + " byl zamítnut.";

					ZobrazitPozadavkyNaRegistraci();
				}
				catch (ArgumentException ex)
				{
					lblChybaPriRegistraci.Text = ex.Message;
					lblChybaPriRegistraci.Visible = true;
					log.Warn("Při zamítnutí požadavku na registraci klienta u poskytovatele došlo k chybě.", ex);
				}
				catch (Exception ex)
				{
					lblChybaPriRegistraci.Text = "Při vytváření požadavku na registraci klienta u poskytovatele došlo k neočekávané chybě.";
					lblChybaPriRegistraci.Visible = true;
					log.Error("Při zamítnutí požadavku na registraci klienta u poskytovatele došlo k chybě.", ex);
				}
			}
		}

		protected void gvKlienti_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType != DataControlRowType.DataRow)
			{
				return;
			}

			LinkButton lnkZrusitRegistraci = (LinkButton)e.Row.Cells[2].Controls[0];
			lnkZrusitRegistraci.OnClientClick = "if (!confirm('Opravdu chcete zrušit registraci vybraného klienta?')) { return false; }";
		}

		protected void gvKlienti_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "ZrusitRegistraci")
			{
				int idKlienta = (int)gvKlienti.DataKeys[int.Parse((String)e.CommandArgument)].Value;

				try
				{
					RegistraceKlientaUPoskytovateleService.ZrusitRegistraciKlienta(idKlienta, SpravaUzivatelu.IdPoskytovatele);
					Domain.Model.Klienti.Klient klient = KlientRepository.Vrat(idKlienta);

					lblZpravaORegistraci.Visible = true;
					lblZpravaORegistraci.Text = "Registrace klienta " + klient.CeleJmeno + " byla zrušena.";

					ZobrazitPozadavkyNaRegistraci();
					ZobrazitRegistrovaneKlienty();
				}
				catch (ArgumentException ex)
				{
					lblChybaPriRegistraci.Text = ex.Message;
					lblChybaPriRegistraci.Visible = true;
					log.Warn("Při zrušení registrace klienta u poskytovatele došlo k chybě.", ex);
				}
				catch (Exception ex)
				{
					lblChybaPriRegistraci.Text = "Při zrušení registrace klienta u poskytovatele došlo k neočekávané chybě.";
					lblChybaPriRegistraci.Visible = true;
					log.Error("Při zrušení registrace klienta u poskytovatele došlo k chybě.", ex);
				}
			}
		}

		private void ZobrazitPozadavkyNaRegistraci()
		{
			gvPozadavkyNaRegistraci.DataSource = PozadavekNaRegistraciKlientaRepository.NajdiPozadavkyDlePoskytovatele(SpravaUzivatelu.IdPoskytovatele);
			gvPozadavkyNaRegistraci.DataBind();
		}

		private void ZobrazitRegistrovaneKlienty()
		{
			gvKlienti.DataSource = KlientRepository.VratRegistrovaneKlienty(SpravaUzivatelu.IdPoskytovatele);
			gvKlienti.DataBind();
		}
	}
}