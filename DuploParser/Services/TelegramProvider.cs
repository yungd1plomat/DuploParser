using DuploParser.Abstractions;
using DuploParser.Models;
using DuploParser.Models.Api;
using System.Collections.Concurrent;
using System.Text;

namespace DuploParser.Services
{
    public class TelegramProvider : ITelegramProvider
    {
        private readonly string _channelId;

        private ConcurrentQueue<MessageParams> _messageQueue { get; set; }

        public TelegramProvider(string channelId)
        {
            _channelId = channelId;
            _messageQueue = new ConcurrentQueue<MessageParams>();
        }

        public void Enqueue(Tyre tyre)
        {
            var description = GenerateDescription(tyre);
            var photoUrl = tyre.Model.LogoUrl.Replace("//", null);
            var messageParams = new MessageParams()
            {
                ChatId = _channelId,
                Description = description,
                Photo = photoUrl,
            };
            _messageQueue.Enqueue(messageParams);
        }

        public string GenerateDescription(Tyre tyre)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("*");
            sb.Append(tyre.Brand.Name);
            sb.Append(tyre.Model.Name);
            sb.Append("*");
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("*Сезон: *");
            sb.AppendLine(tyre.Params.Season.Name);
            sb.Append("*Размер: *");
            sb.AppendLine(tyre.SizeText);
            sb.Append("*Шипы: *");
            sb.AppendLine(tyre.Params.Pins.ToString());
            sb.Append("*RunFlat: *");
            sb.AppendLine(tyre.Params.RunFlat.ToString());
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("*Цена: *");
            sb.AppendLine(tyre.Stock.Price.ToString());
            sb.Append("*Сток: *");
            sb.AppendLine(tyre.Stock.Amount.ToString());
            sb.AppendLine();
            sb.Append($"[Ссылка](https://duplo.shinservice.ru/tyre/{tyre.Brand.UrlPath}/{tyre.Model.UrlPath}/{tyre.Id})");
            return sb.ToString();
        }

        public MessageParams? GetQueueMsg()
        {
            if (!_messageQueue.TryDequeue(out var message))
                return null;
            return message;
        }
    }
}
