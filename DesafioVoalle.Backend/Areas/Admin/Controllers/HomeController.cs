using Microsoft.AspNetCore.Mvc;
using DesafioVoalle.Backend.Controllers;
using DesafioVoalle.Backend.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioVoalle.Backend.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class HomeController : BaseController
	{
		public HomeController(MVCContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
