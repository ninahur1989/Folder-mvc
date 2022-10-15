using Folders.Data;
using Folders.Data.DbModels;
using Folders.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Folders.Services
{
    public class HomeService : IHomeService
    {
        private readonly int count;
        private readonly AppDbContext _context;
        private List<Folder> _myFolders = new List<Folder>();
        private int _iterator;
        private string _currentPath;

        public HomeService(AppDbContext context)
        {
            _context = context;
            _iterator = _context.Folders.Count() + 2;
            count = _context.Folders.Count() + 1;
        }

        private bool AddNewFolder(Folder folder)
        {
            folder.Id = _context.Folders.Count() + 1;
            folder.Name = folder.Path;
            folder.FolderId = 1;
            if (_context.Folders.Add(folder) != null)
            {
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        private bool CheckValidityPath(string path)
        {
            try
            {
                Directory.GetDirectories(path);
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                return false;
            }
            return true;
        }

        public void PrintDirectoryTree(string directory, int lvl, string[] excludedFolders = null, string lvlSeperator = "")
        {
            excludedFolders = excludedFolders ?? new string[0];
            foreach (string d in Directory.GetDirectories(directory))
            {
                if (_iterator != _context.Folders.Count() + 1)
                {
                    int a = _iterator - 1;

                    _myFolders.Add(new Folder()
                    {
                        Id = _iterator,
                        Name = Path.GetFileName(d),
                        Path = Path.GetDirectoryName(d),
                        FolderId = (Path.GetDirectoryName(d) == "C:\\Users\\Admin\\Desktop\\fo") ? count
                        : _myFolders.FirstOrDefault(x => x.Path == Path.GetDirectoryName(d)) != null ? _myFolders.FirstOrDefault(x => x.Path == Path.GetDirectoryName(d)).FolderId : a
                    });
                    _iterator++;
                    _currentPath = Path.GetDirectoryName(d);
                }
                else
                {
                    _myFolders.Add(new Folder() { Id = _iterator, Name = Path.GetFileName(d), FolderId = count, Path = Path.GetDirectoryName(d) });
                    _iterator++;
                    _currentPath = Path.GetDirectoryName(d);
                }

                if (lvl > 0 && Array.IndexOf(excludedFolders, Path.GetFileName(d)) < 0)
                {
                    PrintDirectoryTree(d, lvl - 1, excludedFolders, lvlSeperator + "  ");
                }
            }
        }

        public bool ImportDataFromOS(Folder folder)
        {
            if (AddNewFolder(folder))
            {
                var dir = folder.Path;
                if (CheckValidityPath(dir))
                {
                    PrintDirectoryTree(dir, 2, new string[] { "folder3" });
                    _context.Folders.AddRange(_myFolders);
                    _context.SaveChanges();
                    return true;
                }
            }

            return false;
        }
    }
}
