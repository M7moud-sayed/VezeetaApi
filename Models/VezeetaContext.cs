using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace testV.Models;

public partial class VezeetaContext : DbContext
{
    public VezeetaContext()
    {
    }

    public VezeetaContext(DbContextOptions<VezeetaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Clinic> Clinics { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorFullInfo> DoctorFullInfos { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Offer> Offers { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Specialty> Specialties { get; set; }

    public virtual DbSet<SubSpecialty> SubSpecialties { get; set; }
    public virtual DbSet<FindDoctorBySearchResult> FindDoctorBySearchResults { get; set; }
    public virtual DbSet<FindDoctorByNameResult> FindDoctorByNameResults { get; set; }
    public DbSet<DoctorFullInfo> DoctorFullInfo { get; set; } 
    public DbSet<GetOfferByIdResult> GetOfferByIdResults { get; set; } = null!;
    public DbSet<GetAllOffersResult> GetAllOffersResults { get; set; } = null!;







    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=.\\SQLexpress;Initial Catalog=Test;Integrated Security=True;Trust Server Certificate=True;Command Timeout=300");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasOne(d => d.CidNavigation).WithMany(p => p.Addresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Address_Clinic");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasOne(d => d.CidNavigation).WithMany(p => p.Appointments).HasConstraintName("FK_Appointment_Clinic");

            entity.HasOne(d => d.DidNavigation).WithMany(p => p.Appointments).HasConstraintName("FK_Appointment_Doctor");

            entity.HasOne(d => d.PidNavigation).WithMany(p => p.Appointments).HasConstraintName("FK_Appointment_Patient");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.Property(e => e.Dgender).IsFixedLength();

            entity.HasOne(d => d.SidNavigation).WithMany(p => p.Doctors).HasConstraintName("FK_Doctor_Specialties");

            entity.HasOne(d => d.SubS).WithMany(p => p.Doctors).HasConstraintName("FK_Doctor_SubSpecialties");

            entity.HasMany(d => d.Cids).WithMany(p => p.Dids)
                .UsingEntity<Dictionary<string, object>>(
                    "WorksAt",
                    r => r.HasOne<Clinic>().WithMany()
                        .HasForeignKey("Cid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Works_At_Clinic"),
                    l => l.HasOne<Doctor>().WithMany()
                        .HasForeignKey("Did")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Works_At_Doctor"),
                    j =>
                    {
                        j.HasKey("Did", "Cid");
                        j.ToTable("Works_At");
                        j.IndexerProperty<int>("Did").HasColumnName("DID");
                        j.IndexerProperty<int>("Cid").HasColumnName("CID");
                    });
        });

        modelBuilder.Entity<DoctorFullInfo>(entity =>
        {
            entity.ToView("DoctorFullInfo");

            entity.Property(e => e.Gender).IsFixedLength();
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasOne(d => d.DidNavigation).WithMany(p => p.Feedbacks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Feedback_Doctor");

            entity.HasOne(d => d.PidNavigation).WithMany(p => p.Feedbacks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Feedback_Patient");
        });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.ToTable(tb =>
            {
                tb.HasTrigger("Calculate_Final_Price");
                tb.HasTrigger("Update_Discounted_Fees");
            });

            entity.HasOne(d => d.CidNavigation).WithMany(p => p.Offers).HasConstraintName("FK_Offers_Clinic");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.Property(e => e.Pgender).IsFixedLength();

            entity.HasOne(d => d.DidNavigation).WithMany(p => p.Patients).HasConstraintName("FK_Patient_Doctor");
        });

        modelBuilder.Entity<SubSpecialty>(entity =>
        {
            entity.HasOne(d => d.SidNavigation).WithMany(p => p.SubSpecialties).HasConstraintName("FK_SubSpecialties_Specialties");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public async Task<List<FindDoctorBySearchResult>> FindDoctorsBySearchAsync(string? speciality, string? governorate, string? city)
    {
        return await this.Set<FindDoctorBySearchResult>()
            .FromSqlRaw("EXEC FindDoctorBySearch @Speciality = {0}, @Governorate = {1}, @City = {2}",
                string.IsNullOrWhiteSpace(speciality) ? (object)DBNull.Value : speciality,
                string.IsNullOrWhiteSpace(governorate) ? (object)DBNull.Value : governorate,
                string.IsNullOrWhiteSpace(city) ? (object)DBNull.Value : city)
            .AsNoTracking()
            .ToListAsync();
    }


    public async Task<List<DoctorFullInfo>> GetAllDoctorsFullInfoAsync()
    {
        return await this.DoctorFullInfo.AsNoTracking().ToListAsync();
    }


    public async Task<FindDoctorByIDResult?> FindDoctorByIDAsync(int doctorId)
    {
        var result = this.Database
            .SqlQueryRaw<FindDoctorByIDResult>("EXEC FindDoctorByID @DID = {0}", doctorId)
            .AsEnumerable()
            .FirstOrDefault();

        return await Task.FromResult(result);
    }


    public async Task<List<FindDoctorByNameResult>> FindDoctorByNameAsync(string doctorName)
    {
        return await this.Set<FindDoctorByNameResult>()
            .FromSqlRaw("EXEC FindDoctorByName @DoctorName = {0}", doctorName)
            .ToListAsync();
    }

    public async Task<GetOfferByIdResult?> GetOfferByIdAsync(int offerId)
    {
         var result = this.Database
        .SqlQueryRaw<GetOfferByIdResult>("EXEC GetOfferById @OfferId = {0}", offerId)
        .AsEnumerable()
        .FirstOrDefault();

        return await Task.FromResult(result);
    }

    public async Task<List<GetAllOffersResult>> GetAllOffersAsync()
    {
        return await this.GetAllOffersResults
            .FromSqlRaw("EXEC GetAllOffers") 
            .AsNoTracking()
            .ToListAsync();
    }




}