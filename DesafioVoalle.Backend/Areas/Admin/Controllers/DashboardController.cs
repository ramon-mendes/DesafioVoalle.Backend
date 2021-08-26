using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DesafioVoalle.Backend.Controllers;
using DesafioVoalle.Backend.DAL;

namespace DesafioVoalle.Backend.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class DashboardController : BaseController
	{
		public DashboardController(MVCContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
