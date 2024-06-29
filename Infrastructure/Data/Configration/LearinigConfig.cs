using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configration
{
    public class LearinigConfig : IEntityTypeConfiguration<Learning>
    {
        public void Configure(EntityTypeBuilder<Learning> builder)
        {
            builder.Property(L => L.Name).IsRequired();

        }
    }
}
