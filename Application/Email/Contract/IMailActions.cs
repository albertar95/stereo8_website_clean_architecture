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
        bool ForgetPasswordMail(UserDto user);
        bool VerificationMail(Guid id, string username);
        bool SendEmail(MailRequest mailRequest);
    }
}
