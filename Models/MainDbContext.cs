using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace TASK9.Models;

public partial class MainDbContext : DbContext
{
    public MainDbContext()
    {
    }

    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Doctor> Doctors { get; set; }
    public virtual DbSet<Medicament> Medicament { get; set; }
    public virtual DbSet<Patient> Patient { get; set; }
    public virtual DbSet<Prescription> Prescription { get; set; }
    public virtual DbSet<PrescriptionMedicament> PrescriptionMedicament { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        base.OnConfiguring(optionsBuilder);
        optionsBuilder
   .LogTo(Console.WriteLine)
   .UseSqlServer("Data Source=Alessio\\SQLSERVER,1433;Initial Catalog=TESTAPBD;User id=sa;Password=Root123!;Integrated Security=False;TrustServerCertificate=True; Trusted_Connection=False;MultipleActiveResultSets=true");


    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Console.WriteLine("Creating database");
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.IdDoctor);
            entity.Property(e => e.IdDoctor).ValueGeneratedOnAdd();

            entity.ToTable("Doctor");
            entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
             entity.Property(e => e.Email).HasMaxLength(100).IsRequired();

        });

        modelBuilder.Entity<Patient>(entity =>
          {
            entity.ToTable("Patient");
              entity.HasKey(e => e.IdPatient).HasName("Patient_pk");
              entity.Property(e => e.IdPatient).ValueGeneratedOnAdd();
              

              entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
              entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
              entity.Property(e => e.Birthdate).HasColumnType("datetime").IsRequired();
          });

        modelBuilder.Entity<Medicament>(entity =>
     {
         entity.HasKey(e => e.IdMedicament);


         entity.ToTable("Medicament");
         entity.Property(e => e.IdMedicament).ValueGeneratedOnAdd();
         entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
         entity.Property(e => e.Description).HasMaxLength(100).IsRequired();
         entity.Property(e => e.Type).HasMaxLength(100).IsRequired();
         

     });

        modelBuilder.Entity<Prescription>(entity =>
        {entity.ToTable("Prescription");

            entity.HasKey(e => e.IdPrescription).HasName("Prescription_pk");
            entity.Property(e => e.IdPrescription).ValueGeneratedOnAdd();
            
            entity.Property(e => e.Date).HasColumnType("datetime").IsRequired();
            entity.Property(e => e.DueDate).HasColumnType("datetime").IsRequired();

            entity.HasOne(d => d.Doctor).WithMany(p => p.Prescriptions)
            .HasForeignKey(d => d.IdDoctor)
            .OnDelete(DeleteBehavior.ClientCascade); // TO DO: Check if connected objects are deleted on DELETE

            entity.HasOne(d => d.Patient).WithMany(p => p.ClientPrescriptions)
            .HasForeignKey(d => d.IdPatient)
            .OnDelete(DeleteBehavior.ClientCascade);

        });

        modelBuilder.Entity<PrescriptionMedicament>(entity =>
        { entity.ToTable("PrescriptionMedicament");

            entity.HasKey(e => new { e.IdPrescription, e.IdMedicament });

           entity.Property(e=> e.Dose).HasColumnType("SMALLINT");

            entity.Property(e=> e.Details).HasMaxLength(100);


            entity.HasOne(d => d.Medicament).WithMany(p => p.PrescriptionMedicament)
                .HasForeignKey(d => d.IdMedicament)
                .OnDelete(DeleteBehavior.ClientCascade);

            entity.HasOne(d => d.Prescription).WithMany(p => p.PrescriptionMedicaments)
                .HasForeignKey(d => d.IdPrescription)
                .OnDelete(DeleteBehavior.ClientCascade);
        });


    }

}