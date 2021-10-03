using App.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Data.Mapping {
  public class CarMapping : IEntityTypeConfiguration<Car> {
    public void Configure(EntityTypeBuilder<Car> builder) {
      builder.ToTable("TB_CAR").HasKey(x => x.Id);
      builder.Property(x => x.MaxKm).IsRequired();
      builder.Property(x => x.ModelReleaseYear).IsRequired();
      builder.Property(x => x.Name).HasMaxLength(60).IsRequired();
      builder.Property(x => x.Model).HasMaxLength(60).IsRequired();
      builder.Property(x => x.Color).HasMaxLength(60).IsRequired();
      builder.Property(x => x.Manufacturer).HasMaxLength(60).IsRequired();
      builder.HasMany(x => x.Photos).WithOne();
    }
  }
}
