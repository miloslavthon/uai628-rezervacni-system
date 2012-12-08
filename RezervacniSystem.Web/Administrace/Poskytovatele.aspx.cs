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
			gvPoskytovatele.DataSource = PoskytovatelRepository.VratVse();
			gvPoskytovatele.DataBind();
		}

		private void ZobrazPoskytovatele(Domain.Model.Poskytovatele.Poskytovatel poskytovatel)
		{
			lblVyberPoskytovatele.Visible = poskytovatel == null;
			if (poskytovatel == null)
			{
				hdnIdPoskytovatele.Value = null;
				lblNazev.Text = null;
				txtLoginPoskytovatele.Text = null;
			}
			else
			{
				hdnIdPoskytovatele.Value = poskytovatel.Id.ToString();
				lblNazev.Text = poskytovatel.Nazev;
				txtLoginPoskytovatele.Text = poskytovatel.Login;
			}
		}

		protected void btnVytvoritPoskytovatele_Click(object sender, EventArgs e)
		{
			lblChybaPriVytvoreniPoskytovatele.Visible = false;

			try
			{
				Domain.Model.Poskytovatele.Poskytovatel poskytovatel = new Domain.Model.Poskytovatele.Poskytovatel(txtNazevPoskytovatele.Text);
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
		}

		protected void gvPoskytovatele_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			int id = (int)gvPoskytovatele.DataKeys[int.Parse((String)e.CommandArgument)].Value;
			if (e.CommandName == "Nastavit")
			{
				Domain.Model.Poskytovatele.Poskytovatel poskytovatel = PoskytovatelRepository.Vrat(id);
				ZobrazPoskytovatele(poskytovatel);
			}
			else if (e.CommandName == "Odstranit")
			{
				PoskytovatelRepository.Odstran(id);
				ZobrazSeznamPoskytovatelu();
				if (id.ToString().Equals(hdnIdPoskytovatele.Value))
				{
					ZobrazPoskytovatele(null);
				}
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

			PoskytovatelRepository.Uloz(poskytovatel);
			ZobrazPoskytovatele(poskytovatel);
		}
	}
}