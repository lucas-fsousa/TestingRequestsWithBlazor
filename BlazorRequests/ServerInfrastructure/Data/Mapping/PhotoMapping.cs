using App.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Data.Mapping {
  public class PhotoMapping : IEntityTypeConfiguration<Photo> {
    public void Configure(EntityTypeBuilder<Photo> builder) {
      builder.ToTable("TB_PHOTO").HasKey(x => x.Id);
      builder.Property(x => x.Url).IsRequired();
    }
  }
}
