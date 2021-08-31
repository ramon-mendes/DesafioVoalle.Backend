using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioVoalle.Backend.DAL;
using DesafioVoalle.Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Reinvindique.Backend.Models.API;

namespace DesafioVoalle.Backend.Controllers.API
{
	[Route("api/user")]
	[ApiController]
	public class APIUserController : BaseAPIController
	{
		public APIUserController(MVCContext db)
		{
			_db = db;
		}

		// api/user/register
		[Route("register")]
		public IActionResult Register(string name, string email, string pwd)
		{
			name = name.Trim();
			email = email.Trim().ToLower();
			pwd = pwd.Trim();
			var found = _db.Users.SingleOrDefault(u => u.Email.ToLower() == email);
			if(found != null)
				return StatusCode(201);

			_db.Users.Add(new UserModel()
			{
				Email = email,
				Name = name,
				PWD = pwd,
				Type = EAuthType.NORMAL,
			});
			_db.SaveChanges();

			return Ok();
		}

		// api/user/login
		[Route("login")]
		public User Login(string email, string pwd)
		{
			var model = _db.Users.SingleOrDefault(u => u.Email == email && u.PWD == pwd);
			if(model == null)
				return null;

			var token = AuthorizeUser(model);
			return new User()
			{
				Type = model.Type,
				Token = token,
				Name = model.Name,
				Email = model.Email,
			};
		}

		// api/user/googlelogin
		[Route("googlelogin")]
		public User GoogleLogin(string idtoken, string name, string email)
		{
			var model = _db.Users.SingleOrDefault(u => u.Type == Models.EAuthType.GOOGLE && u.GoogleIdToken == idtoken);
			if(model == null)
			{
				_db.Users.Add(model = new UserModel()
				{
					Email = email,
					Name = name,
					GoogleIdToken = idtoken,
					Type = EAuthType.GOOGLE,
				});
				_db.SaveChanges();
			}

			var token = AuthorizeUser(model);// creates a new token
			return new User()
			{
				Name = model.Name,
				Type = model.Type,
				Email = model.Email,
				Token = token,
			};
		}
	}
}
