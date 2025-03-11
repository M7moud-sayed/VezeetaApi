using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace testV.Models;

public partial class Specialty
{
    [Key]
    [Column("SID")]
    public int Sid { get; set; }

    [Column("STitle")]
    [StringLength(100)]
    [Unicode(false)]
    public string Stitle { get; set; }

    [Column("SImg")]
    [StringLength(100)]
    [Unicode(false)]
    public string Simg { get; set; }

    [InverseProperty("SidNavigation")]
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    [InverseProperty("SidNavigation")]
    public virtual ICollection<SubSpecialty> SubSpecialties { get; set; } = new List<SubSpecialty>();
}
