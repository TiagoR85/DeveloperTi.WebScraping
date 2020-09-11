using Microsoft.EntityFrameworkCore;

namespace DeveloperTi.WebScraping.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }
    }
}
