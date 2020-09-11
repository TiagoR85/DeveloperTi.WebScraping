using System;
using System.Threading.Tasks;

namespace DeveloperTi.WebScraping.Data
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly DataContext _context;

        public UnityOfWork(DataContext context)
        {
            _context = context;
        }

        public Task<int> Commit()
        {
            return _context.SaveChangesAsync(new System.Threading.CancellationToken());
        }

        public void Rollback()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
