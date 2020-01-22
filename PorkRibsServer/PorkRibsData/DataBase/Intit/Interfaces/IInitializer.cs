using System.Threading.Tasks;

namespace PorkRibsData.DataBase.Intit.Interfaces
{
    public interface IInitializer
    {
        Task SeedDataAsync();
    }
}
