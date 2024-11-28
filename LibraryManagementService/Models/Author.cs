using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementService.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Biography { get; set; }

        public List<string> Books { get; set; } = new List<string>();
    }
    public class AuthorCreateModel
    {
        public string Biography { get; set; }
        public string Birthday { get; set; }
        public string Name { get; set; }
    }

    public class AuthorUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string Biography { get; set; }
    }
}
