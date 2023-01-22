
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApi.Models
{
    public class Genre
    {
        // If id int : by default > identity
        // To be generated from Db  > DatabaseGeneratedOption.Identity
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // Id || GenereId >> Will considered as a primary key
        public byte Id { get; set; } // As no need for int (capacity)


        // Already Required as every properities are requied > nullable = false
        [MaxLength(100)]
        public string Name { get; set; }
    }
}