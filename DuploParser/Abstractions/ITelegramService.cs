using DuploParser.Models.Api;

namespace DuploParser.Abstractions
{
    public interface ITelegramService
    {
        string GenerateDescription(Tyre tyre);

        Task SendTyre(Tyre tyre);
    }
}
