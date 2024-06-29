using Entity.Identity;
using Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data.Context
{
    public class StoreContextSeeding
    {
        public static async Task SeedDatabaseAsync(StoreDbContext context, ILoggerFactory loggerFactory , UserManager<AppUser> userManager)
        {
            try
            {
                if (userManager.Users.Any())
                {
                    var Student = new AppUser
                    {
                        UserName = "student",
                        Email = "student@gmail.com"
                    };
                    await userManager.CreateAsync(Student, "Pa$$w0rd");
                    await userManager.AddToRoleAsync(Student, "Student");

                    var Instructor = new AppUser
                    {
                        UserName = "instructor",
                        Email = "instructor@gmail.com"
                    };
                    await userManager.CreateAsync(Instructor, "Pa$$w0rd");
                    await userManager.AddToRolesAsync(Instructor, new[] { "Instructor" , "Student"});



                }



                /// courses seeding data
                if (!context.Courses.Any())
                {
                    var courseData = File.ReadAllText("../Infrastructure/Data/SeedData/courses.json");
                    var courses = JsonSerializer.Deserialize<List<Course>>(courseData);

                    if (courses?.Count() > 0)
                    {
                        foreach (var course in courses)
                        {
                            await context.Courses.AddAsync(course);
                        }
                        await context.SaveChangesAsync();
                    }


                }


                ///learning seeding data
                if (!context.Learnings.Any())
                {
                    var learningData = File.ReadAllText("../Infrastructure/Data/SeedData/learnings.json");
                    var learinigs = JsonSerializer.Deserialize<List<Learning>>(learningData);

                    if (learinigs?.Count() > 0)
                    {
                        foreach (var learnig in learinigs)
                        {
                            await context.Learnings.AddAsync(learnig);
                        }
                        await context.SaveChangesAsync();
                    }


                }


                ///requirments seeding data
                if (!context.Requerments.Any())
                {
                    var requirmentsData = File.ReadAllText("../Infrastructure/Data/SeedData/requirements.json");
                    var requirments = JsonSerializer.Deserialize<List<Requerment>>(requirmentsData);

                    if (requirments?.Count() > 0)
                    {
                        foreach (var req in requirments)
                        {
                            await context.Requerments.AddAsync(req);
                        }
                        await context.SaveChangesAsync();
                    }


                }


                // category seeding data
                if (!context.Categories.Any())
                {
                    var categoryData = File.ReadAllText("../Infrastructure/Data/SeedData/categories.json");
                    var Categories = JsonSerializer.Deserialize<List<Category>>(categoryData);

                    if (Categories?.Count() > 0)
                    {
                        foreach (var category in Categories)
                        {
                            await context.Categories.AddAsync(category);
                        }
                        await context.SaveChangesAsync();
                    }


                }



                // section 
                if (!context.Sections.Any())
                {
                    var sectionData = File.ReadAllText("../Infrastructure/Data/SeedData/sections.json");
                    var sections = JsonSerializer.Deserialize<List<Section>>(sectionData);

                    if (sections?.Count() > 0)
                    {
                        foreach (var sect in sections)
                        {
                            await context.Sections.AddAsync(sect);
                        }
                        await context.SaveChangesAsync();
                    }


                }

                // lecture

                if (!context.Lectures.Any())
                {
                    var lectureData = File.ReadAllText("../Infrastructure/Data/SeedData/lectures.json");
                    var lectures = JsonSerializer.Deserialize<List<Lecture>>(lectureData);

                    if (lectures?.Count() > 0)
                    {

                        foreach (var lect in lectures)
                        {
                            var section = await context.Sections.FindAsync(lect.SectionId);

                            var lecture = new Lecture
                            {
                                Titl = lect.Titl,
                                Url = lect.Url,
                                Section = section
                            };

                            await context.Lectures.AddAsync(lecture);
                        }
                        await context.SaveChangesAsync();
                    }


                }




            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<StoreContextSeeding>();

                logger.LogError(ex.Message);
            }



        }

    }
}
