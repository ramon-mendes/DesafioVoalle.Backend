using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioVoalle.Backend.Models
{
	public class ProductModel
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public string Name { get; set; }
		public string Category { get; set; }
		public double Price { get; set; }

		public virtual UserModel User { get; set; }
		public virtual ICollection<ProductImageModel> Images { get; set; }
	}
}
