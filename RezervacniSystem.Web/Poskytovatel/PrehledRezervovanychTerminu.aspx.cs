using RezervacniSystem.Domain.Model.TerminyRezervaci;
using RezervacniSystem.Domain.Model.Udalosti;
using RezervacniSystem.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RezervacniSystem.Web.Poskytovatel
{
	public partial class PrehledRezervovanychTerminu : BasePage
	{
		protected IUdalostRepository UdalostRepository { get; set; }
		protected ITerminRezervaceRepository TerminRezervaceRepository { get; set; }

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
			if (!IsPostBack)
			{
				NactiUdalostiPoskytovatele();
			}
		}

		protected void chkPouzeZverejnene_CheckedChanged(object sender, EventArgs e)
		{
			NactiUdalostiPoskytovatele();
		}

		protected void gvUdalosti_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "ZobrazitTerminy")
			{
				gvUdalosti.SelectedIndex = int.Parse((String)e.CommandArgument);
				int idUdalosti = (int)gvUdalosti.DataKeys[int.Parse((String)e.CommandArgument)].Value;
				hdnIdUdalosti.Value = idUdalosti.ToString();
				ZobrazTerminyVTydnu(idUdalosti);
			}
		}

		protected void gvTerminy_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType != DataControlRowType.DataRow)
			{
				return;
			}

			dynamic termin = e.Row.DataItem;

			if (termin.Datum < DateTime.Now)
			{
				e.Row.CssClass = "expired";
			}
			else if (termin.Uzavreno)
			{
				e.Row.CssClass = "closed";
			}
		}

		protected void gvTerminy_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "PrehledRezervaci")
			{
				int idTerminuRezervace = (int)gvTerminy.DataKeys[int.Parse((String)e.CommandArgument)].Values[0];
				Response.Redirect("PrehledRezervaci.aspx?Id=" + idTerminuRezervace);
			}
		}

		protected void btnPredchoziTyden_Click(object sender, EventArgs e)
		{
			PrvniDenTydne = PrvniDenTydne.AddDays(-7);
			ZobrazTerminyVTydnu(int.Parse(hdnIdUdalosti.Value));
		}

		protected void btnDalsiTyden_Click(object sender, EventArgs e)
		{
			PrvniDenTydne = PrvniDenTydne.AddDays(7);
			ZobrazTerminyVTydnu(int.Parse(hdnIdUdalosti.Value));
		}

		private void NactiUdalostiPoskytovatele()
		{
			gvUdalosti.SelectedIndex = -1;

			gvUdalosti.DataSource = chkPouzeZverejnene.Checked ?
				UdalostRepository.VratZverejneneUdalostiDlePoskytovatele(SpravaUzivatelu.IdPoskytovatele) :
				UdalostRepository.VratUdalostiDlePoskytovatele(SpravaUzivatelu.IdPoskytovatele);
			gvUdalosti.DataBind();

			DateTime datum = DateTime.Now.Date;
			PrvniDenTydne = datum.AddDays(-(int)datum.DayOfWeek + 1);

			lblDatumOd.Text = "-";
			lblDatumDo.Text = "-";
			btnPredchoziTyden.Enabled = false;
			btnDalsiTyden.Enabled = false;

			gvTerminy.DataSource = null;
			gvTerminy.DataBind();
		}

		private void ZobrazTerminyVTydnu(int idUdalosti)
		{
			DateTime datumOd = PrvniDenTydne;
			DateTime datumDo = datumOd.AddDays(7);

			lblDatumOd.Text = datumOd.ToString("d");
			lblDatumDo.Text = datumDo.AddDays(-1).ToString("d");
			btnPredchoziTyden.Enabled = true;
			btnDalsiTyden.Enabled = true;

			IList<TerminRezervace> terminy = TerminRezervaceRepository.NajdiTerminyRezervaci(idUdalosti, datumOd, datumDo);
			gvTerminy.DataSource = UpravTerminyProZobrazeni(terminy);
			gvTerminy.DataBind();
		}

		private static IEnumerable UpravTerminyProZobrazeni(IList<TerminRezervace> terminy)
		{
			foreach (TerminRezervace t in terminy)
			{
				yield return new
				{
					IdTerminuRezervace = t.Id,
					Datum = t.Datum,
					DobaTrvani = DateTimeUtils.VypisDobu(t.Konec.Subtract(t.Datum)),
					Poznamka = t.TerminUdalosti.Poznamka,
					PocetRezervaci = t.PocetRezervaci,
					Uzavreno = t.Datum.Subtract(t.TerminUdalosti.UzaverkaRezervaci) < DateTime.Now
				};
			}
		}
	}
}