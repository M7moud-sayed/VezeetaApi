using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace testV.Models;

public partial class Offer
{
    [Key]
    [Column("OID")]
    public int Oid { get; set; }

    [Column("OTitle")]
    [StringLength(50)]
    [Unicode(false)]
    public string Otitle { get; set; }

    [Column("ODescription")]
    [StringLength(255)]
    [Unicode(false)]
    public string Odescription { get; set; }

    [Column("ODiscount", TypeName = "decimal(5, 2)")]
    public decimal? Odiscount { get; set; }

    [Column("OStartDate", TypeName = "date")]
    public DateTime? OstartDate { get; set; }

    [Column("OEndDate", TypeName = "date")]
    public DateTime? OendDate { get; set; }

    [Column("OImg")]
    [StringLength(50)]
    [Unicode(false)]
    public string Oimg { get; set; }

    [Column("CID")]
    public int? Cid { get; set; }

    [Column("OPrice", TypeName = "decimal(10, 2)")]
    public decimal? Oprice { get; set; }

    [Column("OFinalPrice", TypeName = "decimal(10, 2)")]
    public decimal? OfinalPrice { get; set; }

    [ForeignKey("Cid")]
    [InverseProperty("Offers")]
    public virtual Clinic CidNavigation { get; set; }
}
