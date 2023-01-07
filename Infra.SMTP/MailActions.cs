using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.User;
using Application.Helpers;
using Application.Email.Model;
using Application.Email.Contract;
using RazorEngine;
using Encoding = System.Text.Encoding;
using Application.Helper;

namespace Infra.SMTP
{
    public class MailActions : IMailActions
    {
        public bool ForgetPasswordMail(UserDto user, MailSettings mailSettings) 
        {
            MailRequest verify = new MailRequest();
            verify.Subject = $"بازیابی کلمه عبور - استریو 8";
            verify.ToEmail = user.Username;
            verify.Body = string.Format("http://localhost:8078/ChangePassword?Hash={0}", WebUtility.UrlEncode(Commons.EncryptString(user.Id.ToString())));
            return SendEmail(verify,mailSettings);
        }
        public bool VerificationMail(UserDto user, MailSettings mailSettings) 
        {
            MailRequest verify = new MailRequest();
            verify.Subject = $"فعال سازی حساب کاربری - استریو 8";
            verify.ToEmail = user.Username;
            verify.Body = Razor.Parse("@Model.address", new { address = string.Format("http://localhost:8078/VerifyUserAccount?Hash={0}", WebUtility.UrlEncode(Commons.EncryptString(user.Id.ToString())))  });
            return SendEmail(verify, mailSettings);
        }
        public bool SendEmail(MailRequest mailRequest, MailSettings mailSettings)
        {
            var email2 = new MailMessage(mailSettings.Mail, mailRequest.ToEmail);
            email2.Subject = mailRequest.Subject;
            email2.IsBodyHtml = true;
            email2.BodyEncoding = Encoding.UTF8;
            email2.Body = mailRequest.Body;
            using var smtp = new System.Net.Mail.SmtpClient();
            smtp.Port = mailSettings.Port;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(mailSettings.Mail, mailSettings.Password);
            smtp.Host = mailSettings.Host;
            smtp.Send(email2);
            return true;
        }
    }
}
