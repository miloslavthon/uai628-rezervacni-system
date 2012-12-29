﻿using RezervacniSystem.Application;
using RezervacniSystem.Domain.Model.Poskytovatele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RezervacniSystem.Web.Administrace
{
	public partial class Poskytovatele : BasePage
	{
		protected IPoskytovatelRepository PoskytovatelRepository { get; set; }
		protected ISpravaPoskytovateluService SpravaPoskytovateluService { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				ZobrazSeznamPoskytovatelu();
				ZobrazPoskytovatele(null);
			}
		}

		private void ZobrazSeznamPoskytovatelu()
		{
			gvPoskytovatele.SelectedIndex = -1;
			gvPoskytovatele.DataSource = PoskytovatelRepository.VratVse();
			gvPoskytovatele.DataBind();
		}

		private void ZobrazPoskytovatele(Domain.Model.Poskytovatele.Poskytovatel poskytovatel)
		{
			pnlZadnyPoskytovatele.Visible = poskytovatel == null;
			pnlDetailPoskytovatele.Visible = !pnlZadnyPoskytovatele.Visible;

			if (poskytovatel == null)
			{
				hdnIdPoskytovatele.Value = null;
				lblNazev.Text = null;
				txtLoginPoskytovatele.Text = null;
				txtMaximalniPocetZverejnenychUdalosti.Text = null;
				txtMaximalniPocetRezervaciJednohoKlienta.Text = null;
				chkRegistraceKlientuPodlehaSchvaleni.Checked = false;
				chkUdalostiProViceOsob.Checked = false;
				chkUdalostiMajiOpakovanyTermin.Checked = false;
			}
			else
			{
				hdnIdPoskytovatele.Value = poskytovatel.Id.ToString();
				lblNazev.Text = poskytovatel.Nazev;
				txtLoginPoskytovatele.Text = poskytovatel.Login;
				txtMaximalniPocetZverejnenychUdalosti.Text = poskytovatel.MaximalniPocetZverejnenychUdalosti.ToString();
				txtMaximalniPocetRezervaciJednohoKlienta.Text = poskytovatel.MaximalniPocetRezervaciJednohoKlienta.ToString();
				chkRegistraceKlientuPodlehaSchvaleni.Checked = poskytovatel.RegistraceKlientuPodlehaSchvaleni;
				chkUdalostiProViceOsob.Checked = poskytovatel.TypRezervace.UdalostiProViceOsob;
				chkUdalostiMajiOpakovanyTermin.Checked = poskytovatel.TypRezervace.UdalostiMajiOpakovanyTermin;
			}
		}

		protected void btnVytvoritPoskytovatele_Click(object sender, EventArgs e)
		{
			lblChybaPriVytvoreniPoskytovatele.Visible = false;

			Domain.Model.Poskytovatele.Poskytovatel poskytovatel  = null;
			try
			{
				poskytovatel = new Domain.Model.Poskytovatele.Poskytovatel(txtNazevPoskytovatele.Text);
				PoskytovatelRepository.Uloz(poskytovatel);
				ZobrazPoskytovatele(poskytovatel);
				txtNazevPoskytovatele.Text = null;
			}
			catch (ArgumentException ex)
			{
				lblChybaPriVytvoreniPoskytovatele.Text = ex.Message;
				lblChybaPriVytvoreniPoskytovatele.Visible = true;
			}

			ZobrazSeznamPoskytovatelu();
			ZobrazPoskytovatele(poskytovatel);
			if (poskytovatel != null)
			{
				for (int i = 0; i < gvPoskytovatele.Rows.Count; i++)
				{
					if (gvPoskytovatele.DataKeys[i].Value.Equals(poskytovatel.Id))
					{
						gvPoskytovatele.SelectedIndex = i;
					}
				}
			}
		}

		protected void gvPoskytovatele_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			int id = (int)gvPoskytovatele.DataKeys[int.Parse((String)e.CommandArgument)].Value;
			if (e.CommandName == "Upravit")
			{
				gvPoskytovatele.SelectedIndex = int.Parse((String)e.CommandArgument);
				Domain.Model.Poskytovatele.Poskytovatel poskytovatel = PoskytovatelRepository.Vrat(id);
				ZobrazPoskytovatele(poskytovatel);
			}
		}

		protected void btnUlozit_Click(object sender, EventArgs e)
		{
			if (hdnIdPoskytovatele.Value == String.Empty)
			{
				return;
			}

			int id = int.Parse(hdnIdPoskytovatele.Value);

			Domain.Model.Poskytovatele.Poskytovatel poskytovatel = PoskytovatelRepository.Vrat(id);
			poskytovatel.Login = String.IsNullOrEmpty(txtLoginPoskytovatele.Text) ? null : txtLoginPoskytovatele.Text;
			poskytovatel.MaximalniPocetZverejnenychUdalosti = String.IsNullOrEmpty(txtMaximalniPocetZverejnenychUdalosti.Text) ? 1 : int.Parse(txtMaximalniPocetZverejnenychUdalosti.Text);
			poskytovatel.MaximalniPocetRezervaciJednohoKlienta = String.IsNullOrEmpty(txtMaximalniPocetRezervaciJednohoKlienta.Text) ? 1 : int.Parse(txtMaximalniPocetRezervaciJednohoKlienta.Text);
			poskytovatel.RegistraceKlientuPodlehaSchvaleni = chkRegistraceKlientuPodlehaSchvaleni.Checked;
			poskytovatel.TypRezervace.UdalostiProViceOsob = chkUdalostiProViceOsob.Checked;
			poskytovatel.TypRezervace.UdalostiMajiOpakovanyTermin = chkUdalostiMajiOpakovanyTermin.Checked;

			PoskytovatelRepository.Uloz(poskytovatel);
			ZobrazPoskytovatele(poskytovatel);
		}

		protected void btnOdstranit_Click(object sender, EventArgs e)
		{
			int id = int.Parse(hdnIdPoskytovatele.Value);
			SpravaPoskytovateluService.ZrusitPoskytovatele(id);
			ZobrazSeznamPoskytovatelu();
			ZobrazPoskytovatele(null);
		}
	}
}