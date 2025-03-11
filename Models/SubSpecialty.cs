using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace testV.Models;

public partial class SubSpecialty
{
    [Key]
    [Column("SubSID")]
    public int SubSid { get; set; }

    [Column("SubSTitle")]
    [StringLength(50)]
    [Unicode(false)]
    public string SubStitle { get; set; }

    [Column("SubSImg")]
    [StringLength(100)]
    [Unicode(false)]
    public string SubSimg { get; set; }

    [Column("SID")]
    public int? Sid { get; set; }

    [InverseProperty("SubS")]
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    [ForeignKey("Sid")]
    [InverseProperty("SubSpecialties")]
    public virtual Specialty SidNavigation { get; set; }
}
