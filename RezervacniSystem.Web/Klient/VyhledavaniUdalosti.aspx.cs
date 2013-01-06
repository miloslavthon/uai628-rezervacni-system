using Common.Logging;
using RezervacniSystem.Application;
using RezervacniSystem.Domain.Model.Poskytovatele;
using RezervacniSystem.Domain.Model.RegistraceKlienta;
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
		private static readonly ILog log = LogManager.GetLogger(typeof(VyhledavaniUdalosti));

		protected IPoskytovatelRepository PoskytovatelRepository { get; set; }
		protected IUdalostRepository UdalostRepository { get; set; }
		protected IRegistraceKlientaRepository RegistraceKlientaRepository { get; set; }
		protected IRegistraceKlientaUPoskytovateleService RegistraceKlientaUPoskytovateleService { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			lblZpravaORegistrani.Visible = false;
			lblChybaPriRegistraci.Visible = false;

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

		protected void btnRegistrovat_Click(object sender, EventArgs e)
		{
			try
			{
				int idPoskytovatele = (int)gvPoskytovatele.SelectedValue;
				Domain.Model.Poskytovatele.Poskytovatel poskytovatel = PoskytovatelRepository.Vrat(idPoskytovatele);

				RegistraceKlientaUPoskytovateleService.RegistrovatKlientaUPoskytovatele(SpravaUzivatelu.IdKlienta, idPoskytovatele);

				divRegistrace.Visible = false;
				lblZpravaORegistrani.Visible = true;
				lblZpravaORegistrani.Text = "Požadavek na registraci u poskytovatele " + poskytovatel.Nazev + " byl uložen. O potvrzení registrace budete informováni.";
			}
			catch (ArgumentException ex)
			{
				lblChybaPriRegistraci.Text = ex.Message;
				lblChybaPriRegistraci.Visible = true;
				log.Warn("Při vytváření požadavku na registraci klienta u poskytovatele došlo k chybě.", ex);
			}
			catch (Exception ex)
			{
				lblChybaPriRegistraci.Text = "Při vytváření požadavku na registraci klienta u poskytovatele došlo k neočekávané chybě.";
				lblChybaPriRegistraci.Visible = true;
				log.Error("Při vytváření požadavku na registraci klienta u poskytovatele došlo k chybě.", ex);
			}
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

				Domain.Model.Poskytovatele.Poskytovatel poskytovatel = PoskytovatelRepository.Vrat(idPoskytovatele);
				divRegistrace.Visible = poskytovatel.RegistraceKlientuPodlehaSchvaleni && !RegistraceKlientaRepository.MaKlientRegistraci(SpravaUzivatelu.IdKlienta, idPoskytovatele);

				NacistUdalostiPoskytovatele(idPoskytovatele);
			}
		}

		protected void gvUdalosti_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "VyhledatTerminy")
			{
				int idUdalosti = (int)gvUdalosti.DataKeys[int.Parse((String)e.CommandArgument)].Value;
				Response.Redirect("VyhledaniTerminu.aspx?Id=" + idUdalosti);
			}
		}

		private void NacistUdalostiPoskytovatele(int idPoskytovatele)
		{
			gvUdalosti.EmptyDataText = "Nejsou k dispozici žádné události pro vybraného poskytovatele.";
			gvUdalosti.DataSource = UdalostRepository.VratZverejneneUdalostiDlePoskytovatele(idPoskytovatele);
			gvUdalosti.DataBind();
		}
	}
}