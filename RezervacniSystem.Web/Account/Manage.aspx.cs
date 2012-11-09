using System;
using System.Collections.Generic;
using System.Linq;

namespace RezervacniSystem.Web.Account
{
	public partial class Manage : System.Web.UI.Page
	{
		protected string SuccessMessage
		{
			get;
			private set;
		}

		protected void Page_Load()
		{
			if (!IsPostBack)
			{
				// Render success message
				var message = Request.QueryString["m"];
				if (message != null)
				{
					// Strip the query string from action
					Form.Action = ResolveUrl("~/Account/Manage.aspx");

					SuccessMessage =
						message == "ChangePwdSuccess" ? "Vaše heslo bylo změněno."
						: message == "SetPwdSuccess" ? "Vaše heslo bylo nastaveno."
						: message == "RemoveLoginSuccess" ? "Přihlašovací účet byl odstraněn."
						: String.Empty;
					successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
				}
			}
		}
	}
}