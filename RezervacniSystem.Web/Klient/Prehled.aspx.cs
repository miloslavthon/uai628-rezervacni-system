using RezervacniSystem.Domain.Model.Klienti;
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
		public IKlientRepository KlientRepository { get; set; }
		public IRezervaceTerminuRepository RezervaceTerminuRepository { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				int idKlienta = KlientRepository.VratIdKlientaDleUzivatelskehoJmena(User.Identity.Name);
				if (idKlienta != 0)
				{
					secUpozorneni.Visible = false;
					SpravaUzivatelu.IdKlienta = idKlienta;
				}

				NactiRezervace();
			}
		}

		protected void chkPouzePlatne_CheckedChanged(object sender, EventArgs e)
		{
			NactiRezervace();
		}

		protected void gvRezervace_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "Zrusit")
			{
				gvRezervace.SelectedIndex = int.Parse((String)e.CommandArgument);
				int idRezervace = (int)gvRezervace.DataKeys[int.Parse((String)e.CommandArgument)].Value;

				// zrušení rezervace

				NactiRezervace();
			}
		}

		private void NactiRezervace()
		{
			gvRezervace.DataSource = RezervaceTerminuRepository.VratRezervace(SpravaUzivatelu.IdKlienta, chkPouzePlatne.Checked);
			gvRezervace.DataBind();
		}
	}
}