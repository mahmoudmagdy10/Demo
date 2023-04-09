using Demo.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Helper
{
    public static class MailSender
    {
        public static string SendMail(MailVM model)
        {
            try
            {
                var client = new SmtpClient("sandbox.smtp.mailtrap.io", 587)
                {
                    Credentials = new NetworkCredential("8dab12ee23a0a2", "ccbd14ff02a1c5"),
                    EnableSsl = true
                };
                client.Send("mahmoudmagdykamel333@gmail.com", model.Mail, model.Title, model.Content);
                var result = "Mail Sent Successfully";
                return result;
            }
            catch(Exception ex)
            {
                var result = "Failed To Send Mail";
                return result;

            }
        } 
    }
}
