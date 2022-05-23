using System;
using System.Collections.Generic;
using TestDB.Entities;

#nullable disable

namespace TestDB.Context
{
    public partial class Author : BaseEntity
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
