
using System.ComponentModel.DataAnnotations.Schema;

namespace APIAspNetCore5.Model
{
    [Table("persons")]
    public class Person 
    {
        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("Gender")]
        public string Gender { get; set; }

        [Column("Id")]
        public long Id { get; internal set; }
    }
}