using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementService.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PersonalNumber { get; set; }
        public string Email { get; set; }
        public List<TakeBookRecord> TakenBookHistory { get; set; } = new List<TakeBookRecord>();

    }
}
