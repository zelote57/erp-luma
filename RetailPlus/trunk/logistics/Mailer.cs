using System;
using System.Net.Mail;
using System.Security.Permissions;

namespace AceSoft
{
	
	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Mailer
	{
		/// <summary>
		/// Send the email.
		/// </summary>
		/// <param name="EmailFrom"></param>
		/// <param name="EmailTo"></param>
		/// <param name="Subject"></param>
		/// <param name="Body"></param>
		/// <param name="Attachments"></param>
		/// <param name="CC"></param>
		/// <param name="BCC"></param>
		/// <param name="SMTPServer"></param>
		/// <param name="Mailformat"></param>
		/// <returns></returns>
        public bool SendMail(MailAddress EmailFrom, MailAddressCollection EmailTo, string Subject, string Body, AttachmentCollection AttachmentFiles, MailAddressCollection CC, MailAddressCollection BCC, SmtpClient SMTPServer, bool IsBodyHtml)
		{
			try
			{
				MailMessage Newmail = new MailMessage();
                Newmail.IsBodyHtml = IsBodyHtml;
                Newmail.Subject = Subject;
                Newmail.Body = Body;
                Newmail.From = EmailFrom;
                Newmail.Sender = EmailFrom;
                foreach (MailAddress recepient in EmailTo)
                    Newmail.To.Add(recepient);

                foreach (MailAddress ccrecepient in CC)
                    Newmail.CC.Add(ccrecepient);

                foreach (MailAddress bccrecepient in BCC)
                    Newmail.CC.Add(bccrecepient);

                foreach (Attachment attachment in AttachmentFiles)
                    Newmail.Attachments.Add(attachment);

				if (SMTPServer.Host != string.Empty)
				{
					SMTPServer.Host = "localhost";
				}

                SMTPServer.Send(Newmail);
				
				return true;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
	}
}
