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
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {

            builder.HasMany(C => C.Learnings)
                    .WithOne( l => l.Course)
                    .HasForeignKey(L => L.CourseId);

            builder.HasMany(C => C.Requerments)
                    .WithOne( R => R.Course)
                    .HasForeignKey(R => R.CourseId);

            builder.HasOne(C => C.Category)
                    .WithMany( Cat => Cat.Courses)
                    .HasForeignKey(C => C.CategoryId);


            builder.Property(C => C.Id).IsRequired();
            builder.Property(C => C.Title).IsRequired();
            builder.Property(C => C.Instructor).IsRequired();
            builder.Property(C => C.Rating).HasColumnType("decimal(18,2)");
            builder.Property(C => C.Description).IsRequired();
            builder.Property(C => C.SubTitle).IsRequired();
            builder.Property(C => C.Image).IsRequired();
            builder.Property(C => C.Language).IsRequired();


        }
    }
}
