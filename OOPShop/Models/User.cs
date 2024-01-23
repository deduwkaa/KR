using System.ComponentModel.DataAnnotations;

namespace OOPShop.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public double Balance { get; set; }
    }
}
