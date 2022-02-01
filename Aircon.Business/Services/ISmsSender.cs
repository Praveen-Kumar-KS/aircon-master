using System.Threading.Tasks;

namespace Aircon.Business.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
