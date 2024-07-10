using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashkart.Domain.Entities
{
    public class Books
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string YearOfPublication { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
