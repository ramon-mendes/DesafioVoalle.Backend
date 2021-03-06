using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DesafioVoalle.Backend.Classes
{
	public class Mailing
	{
		public static void SendTheMasterMail(string subject, string msg)
		{
			SendMail("rfmendesbrazil@gmail.com", "rfmendesbrazil@gmail.com", subject, msg);
		}

		public static void SendMail(string to, string from, string subject, string msg)
		{
			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(from);
			mail.To.Add(to); // para
			mail.Subject = subject; // assunto
			mail.Body = msg;

			using(var smtp = new SmtpClient("smtp.gmail.com"))
			{
				smtp.EnableSsl = true;
				smtp.Port = 587;
				smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtp.UseDefaultCredentials = false;
				smtp.Credentials = new NetworkCredential("", "");
				smtp.Send(mail);
			}
		}
	}
}
