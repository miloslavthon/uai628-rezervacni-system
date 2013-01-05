using Common.Logging;
using RezervacniSystem.Application;
using RezervacniSystem.Domain.Model.Rezervace;
using RezervacniSystem.Domain.Model.TerminyRezervaci;
using RezervacniSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RezervacniSystem.Web.Poskytovatel
{
	public partial class PrehledRezervaci : BasePage
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(PrehledRezervaci));

		public ITerminRezervaceRepository TerminRezervaceRepository { get; set; }
		public IRezervaceTerminuRepository RezervaceTerminuRepository { get; set; }
		public IRezervaceService RezervaceService { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			lblZpravaOZruseniRezervace.Visible = false;
			lblChybaPriZruseniRezervace.Visible = false;

			if (!IsPostBack)
			{
				int id;
				TerminRezervace terminRezervace = null;
				if (Request["Id"] != null && int.TryParse(Request["Id"], out id))
				{
					terminRezervace = TerminRezervaceRepository.Vrat(id);
				}

				if (terminRezervace == null)
				{
					GoToErrorPage("Požadovaný termín neexistuje.");
				}
				else
				{
					ZobrazitTerminRezervace(terminRezervace);
				}
			}
		}

		protected void gvRezervace_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType != DataControlRowType.DataRow)
			{
				return;
			}

			LinkButton lnkZrusit = (LinkButton)e.Row.Cells[2].Controls[0];
			lnkZrusit.OnClientClick = "if (!confirm('Opravdu chcete zrušit vybranou rezervaci?')) { return false; }";
		}

		protected void gvRezervace_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "ZrusitRezervaci")
			{
				int idRezervace = (int)gvRezervace.DataKeys[int.Parse((String)e.CommandArgument)].Values[0];
				RezervaceTerminu rezervace = RezervaceTerminuRepository.Vrat(idRezervace);

				try
				{
					RezervaceService.ZrusitRezervaciZeStranyPoskytovatele(idRezervace);

					//ZobrazitTerminRezervace(TerminRezervaceRepository.Vrat(rezervace.Termin.Id));
					ZobrazitTerminRezervace(rezervace.Termin);

					lblZpravaOZruseniRezervace.Text = "Rezervace pro klienta - " + rezervace.Klient.Prijmeni + " " + rezervace.Klient.Jmeno + " - byla zrušena.";
					lblZpravaOZruseniRezervace.Visible = true;
				}
				catch (ArgumentException ex)
				{
					lblChybaPriZruseniRezervace.Text = ex.Message;
					lblChybaPriZruseniRezervace.Visible = true;

					log.Warn("Při zrušení rezervace došlo k chybě.", ex);
				}
				catch (Exception ex)
				{
					lblChybaPriZruseniRezervace.Text = "Při zrušení rezervace došlo k neočekávané chybě.";
					lblChybaPriZruseniRezervace.Visible = true;

					log.Error("Při zrušení rezervace došlo k chybě.", ex);
				}
			}
		}

		private void ZobrazitTerminRezervace(TerminRezervace terminRezervace)
		{
			litUdalost.Text = terminRezervace.TerminUdalosti.Udalost.Nazev;
			litPopisUdalosti.Text = terminRezervace.TerminUdalosti.Udalost.Popis;
			lblDatum.Text = terminRezervace.Datum.ToString("g");
			lblDobaTrvani.Text = DateTimeUtils.VypisDobu(terminRezervace.TerminUdalosti.CasTrvani.DobaTrvani);
			litPoznamka.Text = terminRezervace.TerminUdalosti.Poznamka;
			litMaximalniPocetUcastniku.Text = terminRezervace.TerminUdalosti.Udalost.MaximalniPocetUcastniku == 0 ? "neomezeno" : terminRezervace.TerminUdalosti.Udalost.MaximalniPocetUcastniku.ToString();
			litPocetRezervaci.Text = terminRezervace.PocetRezervaci.ToString();

			gvRezervace.DataSource = RezervaceTerminuRepository.VratRezervace(terminRezervace.Id);
			gvRezervace.DataBind();
		}
	}
}