using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HelloNet.DataAccesslayer
{
    public class NotesDbContextFactory : IDesignTimeDbContextFactory<NotesDBContext>
    {
        public NotesDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NotesDBContext>();
            optionsBuilder.UseMySql("Data Source=gordonbackend.mysql.database.azure.com;Initial Catalog=BackendGGL;User ID=Gordon;Password=gdindi1!; Encrypt=True", ServerVersion.AutoDetect("Data Source=gordonbackend.mysql.database.azure.com;Initial Catalog=BackendGGL;User ID=Gordon;Password=gdindi1!; Encrypt=True"));
            return new NotesDBContext(optionsBuilder.Options);
        }
    }
}
