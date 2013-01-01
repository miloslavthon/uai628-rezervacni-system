using RezervacniSystem.Domain.Model.Klienti;
using System;
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

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				Domain.Model.Klienti.Klient klient = KlientRepository.NajdiDleUzivatelskehoJmena(User.Identity.Name);
				secUpozorneni.Visible = klient == null;
			}
		}
	}
}