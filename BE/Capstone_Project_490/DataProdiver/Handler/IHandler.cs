using BusinessObject.Models;
using DataProvider.Requests;
using DataProvider.Responses;

namespace DataProvider.Handler
{
    public interface IHandler<TRequest, TResponse>
        where TRequest : Request
        where TResponse : Response
    {
        TResponse handler(TRequest request);
    }
}
