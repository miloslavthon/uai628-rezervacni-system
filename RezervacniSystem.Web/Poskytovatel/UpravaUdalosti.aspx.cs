using Common.Logging;
using RezervacniSystem.Application;
using RezervacniSystem.Domain.Model.Udalosti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RezervacniSystem.Web.Poskytovatel
{
	public partial class UpravaUdalosti : BasePage
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(UpravaUdalosti));

		public IUdalostRepository UdalostRepository { get; set; }
		public ISpravaUdalostiService SpravaUdalostiService { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			lblChyba.Visible = false;

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

				ZobrazUdalost(udalost);
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
				lblChyba.Text = ex.Message;
				lblChyba.Visible = true;

				log.Warn("Při ukládání události došlo k chybě.", ex);
			}
			catch (Exception ex)
			{
				lblChyba.Text = "Při provádění změny došlo k neočekávané chybě.";
				lblChyba.Visible = true;

				log.Error("Při ukládání události došlo k chybě.", ex);
			}
		}

		protected void btnZrusitUdalost_Click(object sender, EventArgs e)
		{
			try
			{
				//SpravaUdalostiService.ZrusitUdalost(int.Parse(Request["Id"]));
			}
			catch (ArgumentException ex)
			{
				lblChyba.Text = ex.Message;
				lblChyba.Visible = true;
			}
			catch
			{
				lblChyba.Text = "Při provádění změny došlo k neočekávané chybě.";
				lblChyba.Visible = true;
			}
		}

		protected void gvTerminy_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "Zrusit")
			{

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

			gvTerminy.DataSource = new List<Object>()
			{
				new { Id = 1, Zacatek = DateTime.Now, Konec = DateTime.Now.AddDays(1), UzaverkaRezervaci = DateTime.Now.AddDays(-1), Poznamka = "Poznámka k termínu 1" },
				new { Id = 2, Zacatek = DateTime.Now, Konec = DateTime.Now.AddDays(1), UzaverkaRezervaci = DateTime.Now.AddDays(-1), Poznamka = "Poznámka k termínu 2" },
				new { Id = 3, Zacatek = DateTime.Now, Konec = DateTime.Now.AddDays(1), UzaverkaRezervaci = DateTime.Now.AddDays(-1), Poznamka = "Poznámka k termínu 3" }
			};
			gvTerminy.DataBind();
		}
	}
}