using RezervacniSystem.Application;
using RezervacniSystem.Domain.Model.Poskytovatele;
using RezervacniSystem.Domain.Model.Udalosti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RezervacniSystem.Web.Poskytovatel
{
	public partial class Udalosti : BasePage
	{
		public IUdalostRepository UdalostRepository { get; set; }
		public ISpravaUdalostiService SpravaUdalostiService { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				NacistUdalosti();
			}
		}

		protected void btnZalozitUdalost_Click(object sender, EventArgs e)
		{
			lblChybaPriZalozeniUdalosti.Visible = false;

			try
			{
				SpravaUdalostiService.ZalozitNovouUdalost(SpravaUzivatelu.IdPoskytovatele, txtNovaUdalost.Text);
				txtNovaUdalost.Text = null;
			}
			catch (ArgumentException ex)
			{
				lblChybaPriZalozeniUdalosti.Text = ex.Message;
				lblChybaPriZalozeniUdalosti.Visible = true;
			}

			NacistUdalosti();
		}

		protected void gvUdalosti_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "Upravit")
			{
				gvUdalosti.SelectedIndex = int.Parse((String)e.CommandArgument);
				int idUdalosti = (int)gvUdalosti.DataKeys[int.Parse((String)e.CommandArgument)].Value;

				Response.Redirect("UpravaUdalosti.aspx?Id=" + idUdalosti);
			}
		}

		private void NacistUdalosti()
		{
			gvUdalosti.DataSource = UdalostRepository.VratUdalostiDlePoskytovatele(SpravaUzivatelu.IdPoskytovatele);
			gvUdalosti.DataBind();
		}
	}
}