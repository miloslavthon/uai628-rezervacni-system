using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezervacniSystem.Domain.Shared
{
	public abstract class DomainObject
	{
		public virtual int Id { get; set; }
	}
}
