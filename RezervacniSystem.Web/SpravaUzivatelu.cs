using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RezervacniSystem.Web
{
	public static class SpravaUzivatelu
	{
		public static int IdPoskytovatele
		{
			get
			{
				if (HttpContext.Current.Session["SpravaUzivatelu_IdPoskytovatele"] == null)
				{
					HttpContext.Current.Session["SpravaUzivatelu_IdPoskytovatele"] = Spring.Context.Support.ContextRegistry.GetContext().GetObject<Domain.Model.Poskytovatele.IPoskytovatelRepository>().VratIdPoskytovateleDleUzivatelskehoJmena(HttpContext.Current.User.Identity.Name);
				}
				return (int)HttpContext.Current.Session["SpravaUzivatelu_IdPoskytovatele"];
			}
			set
			{
				HttpContext.Current.Session["SpravaUzivatelu_IdPoskytovatele"] = value;
			}
		}
	}
}