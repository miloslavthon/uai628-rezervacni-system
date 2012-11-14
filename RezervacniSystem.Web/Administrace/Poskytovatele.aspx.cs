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
		protected void Page_Load(object sender, EventArgs e)
		{
			//var repository = DefaultApplicationContext.GetObject<RezervacniSystem.Domain.Model.Poskytovatele.IPoskytovatelRepository>();
			//repository.Uloz(new Domain.Model.Poskytovatele.Poskytovatel("Test"));
			//var poskytovatele = repository.VratVse();
		}
	}
}