using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS_POS.Models
{
    public class Quantity
    {
        [Key]
        public int q_id { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int p_id { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public int box_quantity { get; set; }
    }
}
