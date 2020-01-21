using System.Threading.Tasks;

namespace PorkRibsAPI.DataBase.Intit
{
    public interface IInitializer
    {
        Task SeedDataAsync();
    }
}
