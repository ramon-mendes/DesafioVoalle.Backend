using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioVoalle.Backend.Models
{
	public class ProductImageModel
	{
		public Guid Id { get; set; }
		public Guid ProductId { get; set; }
		public string Url { get; set; }

		public virtual ProductModel Product { get; set; }
	}
}
