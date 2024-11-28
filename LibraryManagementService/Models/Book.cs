using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementService.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime PublishTime { get; set; }
        public Author Author { get; set; }
        public int PageCount { get; set; }
        public string Description { get; set; }
        public int BookCount { get; set; }
        public Guid? TakenBook { get; set; }
    }

    public class BookRegisterModel
    {
        public string Name { get; set; }
        public string PublishTime { get; set; }
        public Author Author { get; set; }
        public int PageCount { get; set; }
        public string Description { get; set; }
        public int BookCount { get; set; }
    }

    public class BookUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PublishTime { get; set; }
        public Author Author { get; set; }
        public int PageCount { get; set; }
        public string Description { get; set; }
    }
}
