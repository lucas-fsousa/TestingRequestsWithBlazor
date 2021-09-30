using Microsoft.EntityFrameworkCore;
using Server.Infrastructure.Data.Mapping;

namespace Server.Infrastructure.Data {
  /* The context class is the bridge between the application and the database.
   * 
   * It defines the initialization method that can be used to apply predefined entity mapping settings
   */
  public class Context: DbContext {
    public Context(DbContextOptions<Context> options): base(options) { }

    protected override void OnModelCreating(ModelBuilder builder) {
      builder.ApplyConfiguration(new CarMapping());

      base.OnModelCreating(builder);
    }
  }
}
