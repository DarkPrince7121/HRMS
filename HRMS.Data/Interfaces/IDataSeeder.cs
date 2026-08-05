using System.Threading.Tasks;

namespace HRMS.Data.Interfaces
{
    public interface IDataSeeder
    {
        Task SeedAsync();
    }
}