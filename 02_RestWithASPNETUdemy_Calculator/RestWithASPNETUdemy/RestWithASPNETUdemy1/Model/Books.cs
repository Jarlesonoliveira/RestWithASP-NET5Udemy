using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIAspNetCore5.Model
{
    [Table("books")]
    public class Books
    {
        [Column("Title")]
        public string Title { get; set; }

        [Column("Author")]
        public string Author { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }

        [Column("LaunchDate")]
        public DateTime LaunchDate { get; set; }
        public string Id { get; internal set; }
    }
}