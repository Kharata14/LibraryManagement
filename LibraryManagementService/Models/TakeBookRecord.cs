using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementService.Models
{
    public class TakeBookRecord
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        public DateTime TakeDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
