using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioVoalle.Backend.Models;

namespace Reinvindique.Backend.Models.API
{
	public class User
	{
		public EAuthType Type { get; set; }
		public string Token { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
	}
}
