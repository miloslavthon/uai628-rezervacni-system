using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RezervacniSystem.Web.Account
{
	public partial class Login : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			RegisterHyperLink.NavigateUrl = "Register.aspx";

			var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
			if (!String.IsNullOrEmpty(returnUrl))
			{
				RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
			}
		}

		protected void login_LoggedIn(object sender, EventArgs e)
		{
			if (login.UserName == "admin" && !Roles.IsUserInRole("admin", "Administrator"))
			{
				Roles.AddUserToRole("admin", "Administrator");
			}

			int idPoskytovatele = DefaultApplicationContext.GetObject<Domain.Model.Poskytovatele.IPoskytovatelRepository>().VratIdPoskytovateleDleUzivatelskehoJmena(login.UserName);
			if (!Roles.IsUserInRole(login.UserName, "Poskytovatel") && idPoskytovatele != 0)
			{
				Roles.AddUserToRole(login.UserName, "Poskytovatel");
			}
			else if (Roles.IsUserInRole(login.UserName, "Poskytovatel") && idPoskytovatele == 0)
			{
				Roles.RemoveUserFromRole(login.UserName, "Poskytovatel");
			}

			if (idPoskytovatele != 0)
			{
				SpravaUzivatelu.IdPoskytovatele = idPoskytovatele;
			}
		}
	}
}