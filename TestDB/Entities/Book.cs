using System;
using System.Collections.Generic;
using TestDB.Entities;

#nullable disable

namespace TestDB.Context
{
    public partial class Book : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string Excerpt { get; set; }
        public DateTime? PublicDate { get; set; }
    }
}
