using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace testV.Models;

[PrimaryKey("Did", "Pid")]
[Table("Feedback")]
public partial class Feedback
{
    [Column(TypeName = "text")]
    public string Comment { get; set; }

    public int? Rate { get; set; }

    [Key]
    [Column("DID")]
    public int Did { get; set; }

    [Key]
    [Column("PID")]
    public int Pid { get; set; }

    [ForeignKey("Did")]
    [InverseProperty("Feedbacks")]
    public virtual Doctor DidNavigation { get; set; }

    [ForeignKey("Pid")]
    [InverseProperty("Feedbacks")]
    public virtual Patient PidNavigation { get; set; }
}
