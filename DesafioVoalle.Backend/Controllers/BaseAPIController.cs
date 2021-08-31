using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioVoalle.Backend.DAL;
using DesafioVoalle.Backend.Models;

namespace DesafioVoalle.Backend.Controllers
{
	public class BaseAPIController : Controller
	{
		const int EXPIRE_DAYS = 365;
		public MVCContext _db { get; set; }

		protected UserModel GetAuthorizedUser()
		{
			UserAppTokenModel auth;
			if((auth = IsBearerAuthorized()) == null)
				throw new Exception("Not authorized");

			return auth.User;
		}

		protected string AuthorizeUser(UserModel user)
		{
			var auth = new UserAppTokenModel()
			{
				UserId = user.Id,
				DtExpires = DateTime.Now.AddDays(EXPIRE_DAYS),
			};
			_db.UserAppTokens.Add(auth);
			_db.SaveChanges();
			
			return auth.Id.ToString();
		}

		protected UserAppTokenModel IsBearerAuthorized()
		{
			string token = Request.Headers["Authorization"].First().Substring(7);
			var guid = new Guid(token);
			var auth = _db.UserAppTokens.FirstOrDefault(u => u.Id == guid);
			if(auth == null)
				return null;
			if(DateTime.Now > auth.DtExpires)
				return null;
			return auth;
		}
	}
}