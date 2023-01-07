using Application.DTO.User;
using Application.Email.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Email.Contract
{
    public interface IMailActions
    {
        bool ForgetPasswordMail(UserDto user, MailSettings mailSettings);
        bool VerificationMail(UserDto user, MailSettings mailSettings);
        bool SendEmail(MailRequest mailRequest, MailSettings mailSettings);
    }
}
