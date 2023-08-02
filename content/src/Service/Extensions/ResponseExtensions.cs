namespace Linn.Template.Service.Extensions
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Net.Http.Headers;

    public static class ResponseExtensions
    {
        public static Task Negotiate<T>(
            this HttpResponse response,
            T model,
            CancellationToken cancellationToken = default)
        {
            var negotiators = response.HttpContext.RequestServices.GetServices<IResponseNegotiator>().ToList();
            IResponseNegotiator negotiator = null;

            MediaTypeHeaderValue.TryParseList(response.HttpContext.Request.Headers["Accept"], out var accept);
            if (accept != null)
            {
                var ordered = accept.OrderByDescending(x => x.Quality ?? 1);

                foreach (var acceptHeader in ordered)
                {
                    negotiator = negotiators.FirstOrDefault(x => x.CanHandle(acceptHeader));
                    if (negotiator != null)
                    {
                        break;
                    }
                }
            }

            if (negotiator == null)
            {
                negotiator = negotiators.First(
                    x => x.CanHandle(new MediaTypeHeaderValue("application/json")));
            }

            return negotiator.Handle(
                response.HttpContext.Request, response, model, cancellationToken);
        }
    }
}
