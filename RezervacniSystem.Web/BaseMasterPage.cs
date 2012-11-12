using Spring.Context;
using Spring.Web.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace RezervacniSystem.Web
{
	public class BaseMasterPage : MasterPage, ISupportsWebDependencyInjection
	{
		public IApplicationContext DefaultApplicationContext
		{
			get;
			set;
		}
	}
}