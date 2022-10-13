using Folders.Data;
using Folders.Data.DbModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Folders.Services
{
    public class HomeService
    {
        public readonly int count;
        private readonly AppDbContext _context;
        public HomeService(AppDbContext context)
        {
            _context = context;
            iterator = _context.Folders.Count() +1;
            count = _context.Folders.Count();
        }

        public  List<Folder> myFolders = new List<Folder>();
        public  int iterator  ;
        public  string currentPath;

        public  void PrintDirectoryTree(string directory, int lvl, string[] excludedFolders = null, string lvlSeperator = "")
        {
            excludedFolders = excludedFolders ?? new string[0];
            foreach (string d in Directory.GetDirectories(directory))
            {
                Console.WriteLine(lvlSeperator + "-" + Path.GetFileName(d));
                Console.WriteLine(Path.GetDirectoryName(d));
                if (iterator != _context.Folders.Count() + 1)
                {
                    int a = iterator - 1;

                    myFolders.Add(new Folder()
                    {
                        Id = iterator,
                        Name = Path.GetFileName(d),
                        Path = Path.GetDirectoryName(d),
                        FolderId = (Path.GetDirectoryName(d) == "C:\\Users\\Admin\\Desktop\\fo") ? count
                        : myFolders.FirstOrDefault(x => x.Path == Path.GetDirectoryName(d)) != null ? myFolders.FirstOrDefault(x => x.Path == Path.GetDirectoryName(d)).FolderId : a
                    });
                    iterator++;
                    currentPath = Path.GetDirectoryName(d);
                }
                else
                {
                    myFolders.Add(new Folder() { Id = iterator, Name = Path.GetFileName(d), FolderId = count, Path = Path.GetDirectoryName(d) });
                    iterator++;
                    currentPath = Path.GetDirectoryName(d);
                }


                if (lvl > 0 && Array.IndexOf(excludedFolders, Path.GetFileName(d)) < 0)
                {
                    PrintDirectoryTree(d, lvl - 1, excludedFolders, lvlSeperator + "  ");
                }
            }
        }
    }
}
