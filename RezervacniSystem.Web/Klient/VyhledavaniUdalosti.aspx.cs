using RezervacniSystem.Domain.Model.Poskytovatele;
using RezervacniSystem.Domain.Model.Udalosti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RezervacniSystem.Web.Klient
{
	public partial class VyhledavaniUdalosti : BasePage
	{
		protected IPoskytovatelRepository PoskytovatelRepository { get; set; }
		protected IUdalostRepository UdalostRepository { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				NacistPoskytovatele(null);
			}
		}

		protected void btnHledatPoskytovatele_Click(object sender, EventArgs e)
		{
			NacistPoskytovatele(txtHledatPoskytovatele.Text);
		}

		protected void btnZrusitFiltr_Click(object sender, EventArgs e)
		{
			txtHledatPoskytovatele.Text = null;
			NacistPoskytovatele(null);
		}

		private void NacistPoskytovatele(String filtr)
		{
			gvPoskytovatele.SelectedIndex = -1;
			gvPoskytovatele.DataSource = String.IsNullOrEmpty(filtr) ? PoskytovatelRepository.VratVse() : PoskytovatelRepository.VratDleNazvu(txtHledatPoskytovatele.Text);
			gvPoskytovatele.DataBind();

			gvUdalosti.EmptyDataText = "Není vybrán poskytovatel";
			gvUdalosti.DataBind();
		}

		protected void gvPoskytovatele_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "ZobrazitUdalosti")
			{
				gvPoskytovatele.SelectedIndex = int.Parse((String)e.CommandArgument);
				int idPoskytovatele = (int)gvPoskytovatele.DataKeys[int.Parse((String)e.CommandArgument)].Value;

				NacistUdalostiPoskytovatele(idPoskytovatele);
			}
		}

		protected void gvUdalosti_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "ZobrazitTerminy")
			{
				gvUdalosti.SelectedIndex = int.Parse((String)e.CommandArgument);
				int idPoskytovatele = (int)gvPoskytovatele.SelectedDataKey.Value;
				int idUdalosti = (int)gvUdalosti.DataKeys[int.Parse((String)e.CommandArgument)].Value;

				// 
			}
		}

		private void NacistUdalostiPoskytovatele(int idPoskytovatele)
		{
			gvUdalosti.EmptyDataText = "Nejsou k dispozici žádné události pro vybraného poskytovatele.";
			gvUdalosti.DataSource = UdalostRepository.VratUdalostiDlePoskytovatele(idPoskytovatele);
			gvUdalosti.DataBind();
		}
	}
}