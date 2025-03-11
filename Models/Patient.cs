using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace testV.Models;

[Table("Patient")]
public partial class Patient
{
    [Key]
    [Column("PID")]
    public int Pid { get; set; }

    [Column("PName")]
    [StringLength(50)]
    [Unicode(false)]
    public string Pname { get; set; }

    [Column("PBirthDate", TypeName = "date")]
    public DateTime? PbirthDate { get; set; }

    [Column("PLocation")]
    [StringLength(50)]
    [Unicode(false)]
    public string Plocation { get; set; }

    [Column("PPhone")]
    [StringLength(20)]
    [Unicode(false)]
    public string Pphone { get; set; }

    [Column("PEmail")]
    [StringLength(50)]
    [Unicode(false)]
    public string Pemail { get; set; }

    [Column("PGender")]
    [StringLength(1)]
    public string Pgender { get; set; }

    [Column("DID")]
    public int? Did { get; set; }

    [InverseProperty("PidNavigation")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [ForeignKey("Did")]
    [InverseProperty("Patients")]
    public virtual Doctor DidNavigation { get; set; }

    [InverseProperty("PidNavigation")]
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}
