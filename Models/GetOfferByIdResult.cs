﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testV.Models
{
    public partial class GetOfferByIdResult
    {
        public int ID { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [Column("DiscountPercentage", TypeName = "decimal(5,2)")]
        public decimal? DiscountPercentage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [StringLength(50)]
        public string? OfferImage { get; set; }
        [StringLength(50)]
        public string ClinicName { get; set; }
        [Column("OriginalPrice", TypeName = "decimal(10,2)")]
        public decimal? OriginalPrice { get; set; }
        [Column("FinalPrice", TypeName = "decimal(10,2)")]
        public decimal? FinalPrice { get; set; }
    }
}
