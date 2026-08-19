using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Data;

public class DataFactoryServiceProvider : IDataFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DataFactoryServiceProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ApplicationDbContext CreateDbContext()
    {
        return _serviceProvider.GetRequiredService<ApplicationDbContext>();
    }
}