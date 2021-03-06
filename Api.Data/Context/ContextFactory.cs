using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
  public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
  {
    public MyContext CreateDbContext(string[] args)
    {
      var cs = "Server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=mudar@123";
      var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
      optionsBuilder.UseMySql(cs);
      return new MyContext(optionsBuilder.Options);
    }
  }
}
