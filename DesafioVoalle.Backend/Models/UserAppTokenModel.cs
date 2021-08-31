using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioVoalle.Backend.Models
{
	public class UserAppTokenModel
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public DateTime DtExpires { get; set; }
		public virtual UserModel User { get; set; }
	}
}
