using System.Threading.Tasks;

namespace HRMS.Data.Interfaces
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}