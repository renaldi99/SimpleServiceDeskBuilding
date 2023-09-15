using SimpleServiceDeskBuilding.Exceptions;
using SimpleServiceDeskBuilding.Helpers;
using SimpleServiceDeskBuilding.Services;
using System.Net.Http.Headers;

namespace SimpleServiceDeskBuilding.Middleware
{
    public class AuthenticationMiddleware : IMiddleware
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationMiddleware(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Next Implementation
            await next(context);
        }
    }
}
