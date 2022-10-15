using Folders.Data.DbModels;

namespace Folders.Interfaces
{
    public interface IHomeService
    {
        public void PrintDirectoryTree(string directory, int lvl, string[] excludedFolders = null, string lvlSeperator = "");
        public bool ImportDataFromOS(Folder folder);
    }
}
