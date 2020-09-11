using System.Threading.Tasks;

namespace DeveloperTi.WebScraping.Data
{
    public interface IUnityOfWork
    {
        Task<int> Commit();
        void Rollback();
    }
}