using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DesafioVoalle.Backend.Controllers.API
{
	public class APIProdutoController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
