using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace testV.Models;

[Table("Clinic")]
public partial class Clinic
{
    [Key]
    [Column("CID")]
    public int Cid { get; set; }

    [Required]
    [Column("CName")]
    [StringLength(50)]
    [Unicode(false)]
    public string Cname { get; set; }

    [Column("CPhone")]
    [StringLength(20)]
    [Unicode(false)]
    public string Cphone { get; set; }

    [Column("CWorkHours")]
    public int? CworkHours { get; set; }

    [InverseProperty("CidNavigation")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [InverseProperty("CidNavigation")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [InverseProperty("CidNavigation")]
    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();

    [ForeignKey("Cid")]
    [InverseProperty("Cids")]
    public virtual ICollection<Doctor> Dids { get; set; } = new List<Doctor>();
}
