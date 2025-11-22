using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BBS_POS.Models
{
    public class Product
    {
        [Key]
        public int p_id { get; set; }

        [Required]
        [StringLength(100)]
        public string p_name { get; set; }

        [Required]
        public decimal p_sale_price { get; set; }

        [Required]
        public decimal p_buy_price { get; set; }

        [Required]
        public int p_box_size { get; set; }

        [Required]
        public int p_piece_size { get; set; }

        public DateTime p_created_at { get; set; } = DateTime.Now;
        public DateTime? p_updated_at { get; set; }

        // Navigation property for related sales
        public virtual ICollection<Sale> Sales { get; set; }

        // Navigation property for related quantities
        public virtual ICollection<Quantity> Quantities { get; set; }
    }
}