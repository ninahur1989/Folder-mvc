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
                            Id = 2,
                            Name = "Creating Digital images",
                            FolderId = 1,
                            Path = ""
                        },
                        new Folder()
                        {
                            Id = 3,
                            Name = "Resources",
                            FolderId = 2,
                            Path = "Creating Digital images"
                        },
                        new Folder()
                        {
                            Id = 4,
                            Name = "Evidence",
                            FolderId = 2,
                            Path = "Creating Digital images"
                        },
                        new Folder()
                        {
                            Id = 5,
                            Name = "Graphic Product",
                            FolderId = 2,
                            Path = "Creating Digital images"
                        },
                        new Folder()
                        {
                            Id = 6,
                            Name = "Primary Sources",
                            FolderId = 3,
                            Path = "Creating Digital images%Resources"
                        },
                        new Folder()
                        {
                            Id = 7,
                            Name = "Secondary Sources",
                            FolderId = 3,
                            Path = "Creating Digital images%Resources"
                        },
                        new Folder()
                        {
                            Id = 8,
                            Name = "Process",
                            FolderId = 5,
                            Path = "Creating Digital images%Graphic Product"
                        },
                        new Folder()
                        {
                            Id = 9,
                            Name = "Final Product",
                            FolderId = 5,
                            Path = "Creating Digital images%Graphic Product"
                        }
                    });

                }
                context.SaveChanges();
            }
        }
    }
}
