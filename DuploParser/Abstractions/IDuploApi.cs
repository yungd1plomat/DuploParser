using DuploParser.Models.Api;

namespace DuploParser.Abstractions
{
    public interface IDuploApi
    {
        /// <summary>
        /// Получает шины с определенной страницы сервиса
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<IList<Tyre>?> GetTyresAsync(int page = 0);
    }
}
