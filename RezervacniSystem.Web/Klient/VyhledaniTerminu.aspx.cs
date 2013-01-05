using Common.Logging;
using RezervacniSystem.Application;
using RezervacniSystem.Domain.Model.Terminy;
using RezervacniSystem.Domain.Model.Udalosti;
using RezervacniSystem.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RezervacniSystem.Web.Klient
{
	public partial class VyhledaniTerminu : BasePage
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(VyhledaniTerminu));

		public IUdalostRepository UdalostRepository { get; set; }
		public IRezervaceService RezervaceService { get; set; }

		private DateTime PrvniDenTydne
		{
			get
			{
				return (DateTime)ViewState["PrvniDenTydne"];
			}
			set
			{
				ViewState["PrvniDenTydne"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			lblZpravaORezervaci.Visible = false;
			lblChybaPriRezervaciTerminu.Visible = false;

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

					DateTime datum = DateTime.Now.Date;
					PrvniDenTydne = datum.AddDays(-(int)datum.DayOfWeek + 1);
					ZobrazTerminyVTydnu(udalost.Id);
				}
			}
		}

		protected void gvTerminy_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType != DataControlRowType.DataRow)
			{
				return;
			}

			dynamic termin = e.Row.DataItem;

			LinkButton lnkRezervovat = (LinkButton)e.Row.Cells[4].Controls[0];

			if (termin.Uzavreno)
			{
				e.Row.CssClass = "closed";
				lnkRezervovat.Visible = false;
			}
		}

		protected void gvTerminy_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "Rezervovat")
			{
				int idTerminuUdalosti = (int)gvTerminy.DataKeys[int.Parse((String)e.CommandArgument)].Values[0];
				DateTime datum = (DateTime)gvTerminy.DataKeys[int.Parse((String)e.CommandArgument)].Values[1];

				try
				{
					RezervaceService.RezervovatTermin(SpravaUzivatelu.IdKlienta, idTerminuUdalosti, datum);

					lblZpravaORezervaci.Text = "Termín " + datum.ToString("g") + " byl rezervován.";
					lblZpravaORezervaci.Visible = true;
				}
				catch (ArgumentException ex)
				{
					lblChybaPriRezervaciTerminu.Text = ex.Message;
					lblChybaPriRezervaciTerminu.Visible = true;

					log.Warn("Při rezervaci termínu došlo k chybě.", ex);
				}
				catch (Exception ex)
				{
					lblChybaPriRezervaciTerminu.Text = "Při rezervaci termínu došlo k neočekávané chybě.";
					lblChybaPriRezervaciTerminu.Visible = true;

					log.Error("Při rezervaci termínu došlo k chybě.", ex);
				}
			}
		}

		protected void btnPredchoziTyden_Click(object sender, EventArgs e)
		{
			PrvniDenTydne = PrvniDenTydne.AddDays(-7);
			ZobrazTerminyVTydnu(int.Parse(Request["Id"]));

			btnPredchoziTyden.Enabled = PrvniDenTydne > DateTime.Now;
		}

		protected void btnDalsiTyden_Click(object sender, EventArgs e)
		{
			PrvniDenTydne = PrvniDenTydne.AddDays(7);
			ZobrazTerminyVTydnu(int.Parse(Request["Id"]));

			btnPredchoziTyden.Enabled = PrvniDenTydne > DateTime.Now;
		}

		private void ZobrazUdalost(Udalost udalost)
		{
			litUdalost.Text = udalost.Nazev;
			litPoskytovatel.Text = udalost.Poskytovatel.Nazev;
			litPopisUdalosti.Text = udalost.Popis;
			litMaximalniPocetUcastniku.Text = udalost.MaximalniPocetUcastniku == 0 ? "neomezeno" : udalost.MaximalniPocetUcastniku.ToString();
		}

		private void ZobrazTerminyVTydnu(int idUdalosti)
		{
			DateTime datumOd = PrvniDenTydne;
			DateTime datumDo = datumOd.AddDays(7);

			lblDatumOd.Text = datumOd.ToString("d");
			lblDatumDo.Text = datumDo.AddDays(-1).ToString("d");

			List<Tuple<DateTime, TerminUdalosti>> terminy = RezervaceService.VyhledatVolneTerminy(idUdalosti, datumOd, datumDo);
			gvTerminy.DataSource = UpravTerminyProZobrazeni(terminy);
			gvTerminy.DataBind();
		}

		private static IEnumerable UpravTerminyProZobrazeni(IList<Tuple<DateTime, TerminUdalosti>> terminy)
		{
			foreach (Tuple<DateTime, TerminUdalosti> t in terminy)
			{
				yield return new
				{
					IdTerminuUdalosti = t.Item2.Id,
					Datum = t.Item1,
					DobaTrvani = DateTimeUtils.VypisDobu(t.Item2.CasTrvani.DobaTrvani),
					UzaverkaRezervaci = DateTimeUtils.VypisDobu(t.Item2.UzaverkaRezervaci),
					Poznamka = t.Item2.Poznamka,
					Uzavreno = t.Item1.Subtract(t.Item2.UzaverkaRezervaci) < DateTime.Now
				};
			}
		}
	}
}