using Folders.Data.DbModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Folders.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                if (!context.Folders.Any())
                {
                    context.Folders.AddRange(new List<Folder>()
                    {
                        new Folder()
                        {
                            Id = 1,
                            Name = "Creating Digital images",
                            FolderId = 0,
                        },
                        new Folder()
                        {
                            Id = 2,
                            Name = "Resources",
                            FolderId = 1,
                        },
                        new Folder()
                        {
                            Id = 3,
                            Name = "Evidence",
                            FolderId = 1,
                        },
                        new Folder()
                        {
                            Id = 4,
                            Name = "Graphic Product",
                            FolderId = 1,
                        },
                        new Folder()
                        {
                            Id = 5,
                            Name = "Primary Sources",
                            FolderId = 2,
                        },
                         new Folder()
                        {
                            Id = 6,
                            Name = "Secondary Sources",
                            FolderId = 2,
                        },
                        new Folder()
                        {
                            Id = 7,
                            Name = "Primary Sources",
                            FolderId = 4,
                        },
                         new Folder()
                        {
                            Id = 8,
                            Name = "Secondary Sources",
                            FolderId = 4,
                        }
                    });

                }
                context.SaveChanges();
            }

        }
    }
}
