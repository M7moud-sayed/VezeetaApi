using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace testV.Models;

[Table("Doctor")]
public partial class Doctor
{
    [Key]
    [Column("DID")]
    public int Did { get; set; }

    [Required]
    [Column("DName")]
    [StringLength(50)]
    public string Dname { get; set; }

    [Column("DGender")]
    [StringLength(1)]
    public string Dgender { get; set; }

    [Column("DDegree")]
    [StringLength(50)]
    [Unicode(false)]
    public string Ddegree { get; set; }

    [Column("DProfileImage")]
    [StringLength(100)]
    [Unicode(false)]
    public string DprofileImage { get; set; }

    [Column("SID")]
    public int? Sid { get; set; }

    [Column("DFees", TypeName = "decimal(10, 2)")]
    public decimal? Dfees { get; set; }

    [Column("SubSID")]
    public int? SubSid { get; set; }

    [InverseProperty("DidNavigation")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [InverseProperty("DidNavigation")]
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    [InverseProperty("DidNavigation")]
    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    [ForeignKey("Sid")]
    [InverseProperty("Doctors")]
    public virtual Specialty SidNavigation { get; set; }

    [ForeignKey("SubSid")]
    [InverseProperty("Doctors")]
    public virtual SubSpecialty SubS { get; set; }

    [ForeignKey("Did")]
    [InverseProperty("Dids")]
    public virtual ICollection<Clinic> Cids { get; set; } = new List<Clinic>();
}
