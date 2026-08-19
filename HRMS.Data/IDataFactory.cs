using Microsoft.EntityFrameworkCore;

namespace HRMS.Data;

public interface IDataFactory
{
    ApplicationDbContext CreateDbContext();
}