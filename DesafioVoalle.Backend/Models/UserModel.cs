using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using DesafioVoalle.Backend.Classes;

namespace DesafioVoalle.Backend.Models
{
	public class UserModel
	{
		[Key]
		public int Id  { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		//public string PWD { get; set; }
		public EIdiom Idiom { get; set; }
	}

	public class UserValidator : AbstractValidator<UserModel>
	{
		public UserValidator()
		{
			RuleFor(x => x.Name).NotEmpty();
			RuleFor(x => x.Email).EmailAddress().NotEmpty();
			//RuleFor(x => x.PWD).NotEmpty();
		}
	}
}