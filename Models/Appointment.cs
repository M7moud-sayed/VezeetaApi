using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace testV.Models;

[Table("Appointment")]
public partial class Appointment
{
    [Key]
    [Column("AID")]
    public int Aid { get; set; }

    [Column("ADate", TypeName = "date")]
    public DateTime? Adate { get; set; }

    [Column("ATime")]
    public TimeSpan? Atime { get; set; }

    [Column("PID")]
    public int? Pid { get; set; }

    [Column("CID")]
    public int? Cid { get; set; }

    [Column("DID")]
    public int? Did { get; set; }

    [ForeignKey("Cid")]
    [InverseProperty("Appointments")]
    public virtual Clinic CidNavigation { get; set; }

    [ForeignKey("Did")]
    [InverseProperty("Appointments")]
    public virtual Doctor DidNavigation { get; set; }

    [ForeignKey("Pid")]
    [InverseProperty("Appointments")]
    public virtual Patient PidNavigation { get; set; }
}
