using App.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Data.Mapping {
  public class UserMapping : IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder) {
      builder.ToTable("TB_USER").HasKey(x => x.Id);
      builder.Property(x => x.Login).IsRequired().HasMaxLength(50);
      builder.Property(x => x.Password).IsRequired().HasMaxLength(80);

      builder.Ignore(x => x.Authenticated);
    }
  }
}
