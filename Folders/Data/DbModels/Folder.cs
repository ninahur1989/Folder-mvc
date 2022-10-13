using System.ComponentModel.DataAnnotations;

namespace Folders.Data.DbModels
{
    public class Folder
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int FolderId { get; set; }

        public string? Path { get; set; }
    }
}
