using Vehicles.Common.Models;

namespace Vehicles.API.Helpers
{
    public class MailHelper2 : IMailHelper
    {
        public Response SendMail(string to, string subject, string body)
        {
            return new Response { IsSuccess = true };
        }
    }
}
