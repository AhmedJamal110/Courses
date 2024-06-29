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
    public class RequirmentConfig : IEntityTypeConfiguration<Requerment>
    {
        public void Configure(EntityTypeBuilder<Requerment> builder)
        {
            builder.Property(R => R.Name);

        }
    }
}
