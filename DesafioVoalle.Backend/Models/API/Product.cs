using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioVoalle.Backend.Models.API
{
	public class Product
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Category { get; set; }

		public List<string> ImagesURL { get; set; }
	}
}
