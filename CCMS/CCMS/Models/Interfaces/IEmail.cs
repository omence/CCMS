using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.Models.Interfaces
{
    /// <summary>
    /// sends user message to admin via email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="subject"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public interface IEmail
    {
        Task SendEmail(string email, string subject, string message);
    }
}
