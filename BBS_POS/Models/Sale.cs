using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS_POS.Models
{
    public class Sale
    {
        [Key]
        public int s_id { get; set; }  // Primary Key

        [Required]
        [StringLength(100)]
        public string p_name { get; set; }

        public int? qty_box { get; set; }
        public int? qty_pieces { get; set; }

        [Required]
        public decimal price { get; set; }

        [Required]
        public decimal total { get; set; }

        public DateTime s_date { get; set; } = DateTime.Now;

        [ForeignKey("Product")]
        public int p_id { get; set; }

        public virtual Product Product { get; set; }
    }
}