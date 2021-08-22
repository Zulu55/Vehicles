using Vehicles.Common.Models;

namespace Vehicles.API.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string to, string subject, string body);
    }
}
