using System.ComponentModel.DataAnnotations;

namespace BBS_POS.Models
{
    public class User
    {
        [Key]
        public int u_id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string u_name { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50)]
        public string u_usr { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255)]
        public string u_pwd { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [StringLength(50)]
        public string u_role { get; set; }
    }
}