using Common.Logging;
using RezervacniSystem.Application;
using RezervacniSystem.Domain.Model.Terminy;
using RezervacniSystem.Domain.Model.Udalosti;
using RezervacniSystem.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RezervacniSystem.Web.Poskytovatel
{
	public partial class UpravaUdalosti : BasePage
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(UpravaUdalosti));

		public IUdalostRepository UdalostRepository { get; set; }
		public ITerminUdalostiRepository TerminUdalostiRepository { get; set; }
		public ISpravaUdalostiService SpravaUdalostiService { get; set; }
		public ISpravaTerminuService SpravaTerminuService { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			lblChybaPriUkladaniUdalosti.Visible = false;
			lblChybaPriZverejneniTerminu.Visible = false;

			if (!IsPostBack)
			{
				int id;
				Udalost udalost = null;
				if (Request["Id"] != null && int.TryParse(Request["Id"], out id))
				{
					udalost = UdalostRepository.Vrat(id);
				}

				if (udalost == null)
				{
					GoToErrorPage("Požadovaná událost neexistuje.");
				}
				else
				{
					ZobrazUdalost(udalost);
				}
			}
		}

		protected void btnUlozit_Click(object sender, EventArgs e)
		{
			try
			{
				SpravaUdalostiService.ZmenitUdalost(
					int.Parse(Request["Id"]),
					txtNazev.Text == String.Empty ? null : txtNazev.Text,
					txtMaximalniPocetUcastniku.Text == String.Empty ? 0 : int.Parse(txtMaximalniPocetUcastniku.Text),
					chkZverejneno.Checked,
					txtPopis.Text == String.Empty ? null : txtPopis.Text);

				Response.Redirect("Udalosti.aspx");
			}
			catch (ArgumentException ex)
			{
				lblChybaPriUkladaniUdalosti.Text = ex.Message;
				lblChybaPriUkladaniUdalosti.Visible = true;

				log.Warn("Při ukládání události došlo k chybě.", ex);
			}
			catch (Exception ex)
			{
				lblChybaPriUkladaniUdalosti.Text = "Při provádění změny došlo k neočekávané chybě.";
				lblChybaPriUkladaniUdalosti.Visible = true;

				log.Error("Při ukládání události došlo k chybě.", ex);
			}
		}

		protected void btnZrusitUdalost_Click(object sender, EventArgs e)
		{
			try
			{
				SpravaUdalostiService.ZrusitUdalost(int.Parse(Request["Id"]));

				Response.Redirect("Udalosti.aspx");
			}
			catch (ArgumentException ex)
			{
				lblChybaPriUkladaniUdalosti.Text = ex.Message;
				lblChybaPriUkladaniUdalosti.Visible = true;

				log.Warn("Při zrušení události došlo k chybě.", ex);
			}
			catch (Exception ex)
			{
				lblChybaPriUkladaniUdalosti.Text = "Při provádění změny došlo k neočekávané chybě.";
				lblChybaPriUkladaniUdalosti.Visible = true;

				log.Error("Při zrušení události došlo k chybě.", ex);
			}
		}

		protected void btnZverejnitNovyTermin_Click(object sender, EventArgs e)
		{
			try
			{
				int idUdalosti = int.Parse(Request["Id"]);

				if (mvTermin.GetActiveView().Equals(viewJednorazovyTermin))
				{
					SpravaTerminuService.ZverejnitJednorazovyTermin(
						idUdalosti,
						DateTime.Parse(txtTermin_Datum.Text),
						TimeSpan.Parse(txtTermin_Cas.Text),
						new TimeSpan(int.Parse(txtDobaTrvani_Hodiny.Text), int.Parse(txtDobaTrvani_Minuty.Text), 0),
						new TimeSpan(int.Parse(txtUzaverkaRezervaci_Dny.Text), int.Parse(txtUzaverkaRezervaci_Hodiny.Text), int.Parse(txtUzaverkaRezervaci_Minuty.Text), 0),
						txtPoznamka.Text == String.Empty ? null : txtPoznamka.Text);
				}
				else
				{
					SpravaTerminuService.ZverejnitOpakovanyTermin(
						idUdalosti,
						(DenVTydnu)Enum.Parse(typeof(DenVTydnu), cmbDen.SelectedValue),
						TimeSpan.Parse(txtTermin_Cas.Text),
						DateTime.Parse(txtPlatnyDo.Text),
						new TimeSpan(int.Parse(txtDobaTrvani_Hodiny.Text), int.Parse(txtDobaTrvani_Minuty.Text), 0),
						new TimeSpan(int.Parse(txtUzaverkaRezervaci_Dny.Text), int.Parse(txtUzaverkaRezervaci_Hodiny.Text), int.Parse(txtUzaverkaRezervaci_Minuty.Text), 0),
						txtPoznamka.Text == String.Empty ? null : txtPoznamka.Text);
				}

				NactiTerminyUdalosti(idUdalosti);
			}
			catch (ArgumentException ex)
			{
				lblChybaPriZverejneniTerminu.Text = ex.Message;
				lblChybaPriZverejneniTerminu.Visible = true;

				log.Warn("Při zveřejnění termínu došlo k chybě.", ex);
			}
			catch (Exception ex)
			{
				lblChybaPriZverejneniTerminu.Text = "Při provádění změny došlo k neočekávané chybě.";
				lblChybaPriZverejneniTerminu.Visible = true;

				log.Warn("Při zveřejnění termínu došlo k chybě.", ex);
			}
		}

		protected void gvTerminy_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType != DataControlRowType.DataRow)
			{
				return;
			}

			LinkButton lnkZrusit = (LinkButton)e.Row.Cells[4].Controls[0];
			lnkZrusit.OnClientClick = "if (!confirm('Opravdu chcete zrušit vybraný termín se všmi případnými rezervacemi?')) { return false; }";
		}

		protected void gvTerminy_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "Zrusit")
			{
				int idTerminu = (int)gvTerminy.DataKeys[int.Parse((String)e.CommandArgument)].Value;

				try
				{
					SpravaTerminuService.ZrusitTermin(idTerminu);
				}
				catch (ArgumentException ex)
				{
					lblChybaPriZverejneniTerminu.Text = ex.Message;
					lblChybaPriZverejneniTerminu.Visible = true;

					log.Warn("Při zrušení termínu došlo k chybě.", ex);
				}
				catch (Exception ex)
				{
					lblChybaPriZverejneniTerminu.Text = "Při zrušení termínu došlo k neočekávané chybě.";
					lblChybaPriZverejneniTerminu.Visible = true;

					log.Warn("Při zrušení termínu došlo k chybě.", ex);
				}

				NactiTerminyUdalosti(int.Parse(Request["Id"]));
			}
		}

		private void ZobrazUdalost(Udalost udalost)
		{
			txtNazev.Text = udalost.Nazev;
			txtMaximalniPocetUcastniku.Text = udalost.MaximalniPocetUcastniku.ToString();
			chkZverejneno.Checked = udalost.Zverejneno;
			txtPopis.Text = udalost.Popis;
			lblOpakovanyTermin.Text = udalost.OpakovanyTermin ? "Ano" : "Ne";

			if (!udalost.Poskytovatel.TypRezervace.UdalostiProViceOsob)
			{
				txtMaximalniPocetUcastniku.Enabled = false;
			}

			mvTermin.SetActiveView(udalost.OpakovanyTermin ? viewOpakovanyTermin : viewJednorazovyTermin);
			spanPlatnyDo.Visible = udalost.OpakovanyTermin;

			NactiTerminyUdalosti(udalost.Id);
		}

		private void NactiTerminyUdalosti(int idUdalosti)
		{
			gvTerminy.DataSource = UpravTerminyProZobrazeni(TerminUdalostiRepository.VratTerminyDleUdalosti(idUdalosti));
			gvTerminy.DataBind();
		}

		private static IEnumerable UpravTerminyProZobrazeni(IList<TerminUdalosti> terminy)
		{
			foreach (TerminUdalosti t in terminy)
			{
				yield return new
				{
					Id = t.Id,
					Termin = t.Typ == TypTerminu.JEDNORAZOVY ?
						t.Datum.Value.ToString("d") + " " + t.CasTrvani.Cas.ToString(@"hh\:mm") :
						DenVTydnuPopis.ResourceManager.GetString(t.Den.Value.ToString()) + " " + t.CasTrvani.Cas.ToString(@"hh\:mm") + ", do " + t.PlatnyDo.Value.ToString("d"),
					DobaTrvani = DateTimeUtils.VypisDobu(t.CasTrvani.DobaTrvani),
					UzaverkaRezervaci = DateTimeUtils.VypisDobu(t.UzaverkaRezervaci),
					Poznamka = t.Poznamka
				};
			}
		}
	}
}