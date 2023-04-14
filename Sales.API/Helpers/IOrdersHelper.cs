using Sales.Shared.Entities;

namespace Sales.API.Helpers
{
    public interface IOrdersHelper
    {
        Task<Response> ProcessOrderAsync(string email, string remarks);
    }
}

