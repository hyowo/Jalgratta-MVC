using Jalgratta_Eksam.Models;
using Microsoft.EntityFrameworkCore;

namespace Jalgratta_Eksam.Data
{
    public class EksamidDBContext : DbContext
    {
        public EksamidDBContext(DbContextOptions<EksamidDBContext> options)
            : base(options)
            { }
        
        public DbSet<Eksam> Eksamid { get; set; }
    }
}
