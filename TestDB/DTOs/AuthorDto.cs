using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDB.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
