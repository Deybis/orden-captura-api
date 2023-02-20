using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Entities.Shared.Abstractions
{
    public interface IStartupServices
    {
        void Initialize(IServiceCollection services, IConfiguration configuration);
    }
}
