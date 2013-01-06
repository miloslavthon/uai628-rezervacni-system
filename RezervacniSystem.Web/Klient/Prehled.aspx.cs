using Common.Logging;
using RezervacniSystem.Application;
using RezervacniSystem.Domain.Model.Klienti;
using RezervacniSystem.Domain.Model.KlientskeZpravy;
using RezervacniSystem.Domain.Model.Rezervace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RezervacniSystem.Web.Klient
{
	public partial class Prehled : BasePage
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(Prehled));

		public IKlientRepository KlientRepository { get; set; }
		public IKlientskaZpravaRepository KlientskaZpravaRepository { get; set; }
		public IRezervaceTerminuRepository RezervaceTerminuRepository { get; set; }
		public IRezervaceService RezervaceService { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			lblChybaPriSmazaniZpravy.Visible = false;
			lblChybaPriZruseniTerminu.Visible = false;

			if (!IsPostBack)
			{
				int idKlienta = KlientRepository.VratIdKlientaDleUzivatelskehoJmena(User.Identity.Name);
				if (idKlienta != 0)
				{
					secUpozorneni.Visible = false;
					SpravaUzivatelu.IdKlienta = idKlienta;
				}

				NactiZpravy();
				NactiRezervace();
			}
		}

		protected void gvZpravy_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType != DataControlRowType.DataRow)
			{
				return;
			}

			LinkButton lnkSmazat = (LinkButton)e.Row.Cells[2].Controls[0];
			lnkSmazat.OnClientClick = "if (!confirm('Opravdu chcete smazat vybranou zprávu?')) { return false; }";
		}

		protected void gvZpravy_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "Smazat")
			{
				gvZpravy.SelectedIndex = int.Parse((String)e.CommandArgument);
				int idZpravy = (int)gvZpravy.DataKeys[int.Parse((String)e.CommandArgument)].Value;

				try
				{
					KlientskaZpravaRepository.Odstran(idZpravy);
				}
				catch (ArgumentException ex)
				{
					lblChybaPriSmazaniZpravy.Text = ex.Message;
					lblChybaPriSmazaniZpravy.Visible = true;

					log.Warn("Při smazání zprávy došlo k chybě.", ex);
				}
				catch (Exception ex)
				{
					lblChybaPriSmazaniZpravy.Text = "Při smazání zprávy došlo k neočekávané chybě.";
					lblChybaPriSmazaniZpravy.Visible = true;

					log.Warn("Při smazání zprávy došlo k chybě.", ex);
				}

				NactiZpravy();
			}
		}

		protected void chkPouzePlatne_CheckedChanged(object sender, EventArgs e)
		{
			NactiRezervace();
		}

		protected void gvRezervace_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType != DataControlRowType.DataRow)
			{
				return;
			}

			PrehledRezervace rezervace = (PrehledRezervace)e.Row.DataItem;

			LinkButton lnkZrusit = (LinkButton)e.Row.Cells[4].Controls[0];

			DateTime datum = DateTime.Now;
			if (rezervace.Datum < datum)
			{
				e.Row.CssClass = "expired";
				lnkZrusit.Visible = false;
			}
			else if (rezervace.Datum.Subtract(rezervace.UzaverkaRezervaci) < datum)
			{
				e.Row.CssClass = "closed";
				lnkZrusit.Visible = false;
			}
			else
			{
				lnkZrusit.OnClientClick = "if (!confirm('Opravdu chcete zrušit vybranou rezervaci?')) { return false; }";
			}
		}

		protected void gvRezervace_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "Zrusit")
			{
				gvRezervace.SelectedIndex = int.Parse((String)e.CommandArgument);
				int idRezervace = (int)gvRezervace.DataKeys[int.Parse((String)e.CommandArgument)].Value;

				try
				{
					RezervaceService.ZrusitRezervaciZeStranyKlienta(idRezervace);
				}
				catch (ArgumentException ex)
				{
					lblChybaPriZruseniTerminu.Text = ex.Message;
					lblChybaPriZruseniTerminu.Visible = true;

					log.Warn("Při zrušení rezervace došlo k chybě.", ex);
				}
				catch (Exception ex)
				{
					lblChybaPriZruseniTerminu.Text = "Při zrušení rezervace došlo k neočekávané chybě.";
					lblChybaPriZruseniTerminu.Visible = true;

					log.Warn("Při zrušení rezervace došlo k chybě.", ex);
				}

				NactiRezervace();
			}
		}

		private void NactiZpravy()
		{
			gvZpravy.DataSource = KlientskaZpravaRepository.VratZpravy(SpravaUzivatelu.IdKlienta);
			gvZpravy.DataBind();
		}

		private void NactiRezervace()
		{
			gvRezervace.DataSource = RezervaceTerminuRepository.VratRezervace(SpravaUzivatelu.IdKlienta, chkPouzePlatne.Checked);
			gvRezervace.DataBind();
		}
	}
}