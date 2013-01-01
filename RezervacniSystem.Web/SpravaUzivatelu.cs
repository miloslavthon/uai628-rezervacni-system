using Spring.Context;
using Spring.Context.Support;
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
					HttpContext.Current.Session["SpravaUzivatelu_IdPoskytovatele"] = GetObject<Domain.Model.Poskytovatele.IPoskytovatelRepository>().VratIdPoskytovateleDleUzivatelskehoJmena(HttpContext.Current.User.Identity.Name);
				}
				return (int)HttpContext.Current.Session["SpravaUzivatelu_IdPoskytovatele"];
			}
			set
			{
				HttpContext.Current.Session["SpravaUzivatelu_IdPoskytovatele"] = value;
			}
		}

		public static int IdKlienta
		{
			get
			{
				if (HttpContext.Current.Session["SpravaUzivatelu_IdKlienta"] == null)
				{
					HttpContext.Current.Session["SpravaUzivatelu_IdKlienta"] = GetObject<Domain.Model.Klienti.IKlientRepository>().VratIdKlientaDleUzivatelskehoJmena(HttpContext.Current.User.Identity.Name);
				}
				return (int)HttpContext.Current.Session["SpravaUzivatelu_IdKlienta"];
			}
			set
			{
				HttpContext.Current.Session["SpravaUzivatelu_IdKlienta"] = value;
			}
		}

		private static IApplicationContext Context
		{
			get
			{
				return ContextRegistry.GetContext();
			}
		}

		private static T GetObject<T>()
		{
			return Context.GetObject<T>();
		}
	}
}