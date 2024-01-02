using OnlineStore.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.DataAccess.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FristName { get; set; }

        public string LastName { get; set; }

        [StringLength(50)]
        public string Civilianld { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Roles Role { get; set; }

        public bool isDeleted { get; set; }
    }
}
