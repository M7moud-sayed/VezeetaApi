using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace testV.Models;

[Keyless]
public partial class DoctorFullInfo
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string DoctorName { get; set; }

    [StringLength(1)]
    public string Gender { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Degree { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Fees { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Image { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string MainSpecialty { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SubSpecialty { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string ClinicName { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string City { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Governorate { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Street { get; set; }
}
