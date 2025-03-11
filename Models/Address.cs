using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace testV.Models;

[PrimaryKey("Cid", "Street", "City", "Governorate")]
[Table("Address")]
public partial class Address
{
    [Key]
    [Column("CID")]
    public int Cid { get; set; }

    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string Street { get; set; }

    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string City { get; set; }

    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string Governorate { get; set; }

    [ForeignKey("Cid")]
    [InverseProperty("Addresses")]
    public virtual Clinic CidNavigation { get; set; }
}
