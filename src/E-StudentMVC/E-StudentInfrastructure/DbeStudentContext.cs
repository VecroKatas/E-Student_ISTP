using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using E_StudentDomain.Model;

namespace E_StudentInfrastructure;

public partial class DbeStudentContext : DbContext
{
    public DbeStudentContext()
    {
    }

    public DbeStudentContext(DbContextOptions<DbeStudentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dorm> Dorms { get; set; }

    public virtual DbSet<DormAccount> DormAccounts { get; set; }

    public virtual DbSet<DormAccountTransaction> DormAccountTransactions { get; set; }

    public virtual DbSet<DormInspection> DormInspections { get; set; }

    public virtual DbSet<DormPass> DormPasses { get; set; }

    public virtual DbSet<DormResident> DormResidents { get; set; }

    public virtual DbSet<DormRoom> DormRooms { get; set; }

    public virtual DbSet<ExamSchedule> ExamSchedules { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<LessonSchedule> LessonSchedules { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-PLGKHNA\\SQLEXPRESS; Database=DBE-Student; Trusted_Connection=True; TrustServerCertificate=True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dorm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Dorms_1");

            entity.Property(e => e.Address).HasMaxLength(50);
        });

        modelBuilder.Entity<DormAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DormAccounts_1");
        });

        modelBuilder.Entity<DormAccountTransaction>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.SecondParty).HasMaxLength(50);

            entity.HasOne(d => d.Account).WithMany(p => p.DormAccountTransactions)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DormAccountTransactions_DormAccounts");
        });

        modelBuilder.Entity<DormInspection>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Dorm).WithMany(p => p.DormInspections)
                .HasForeignKey(d => d.DormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DormInspections_Dorms");
        });

        modelBuilder.Entity<DormPass>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Dorm).WithMany(p => p.DormPasses)
                .HasForeignKey(d => d.DormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DormPasses_Dorms");

            entity.HasOne(d => d.Room).WithMany(p => p.DormPasses)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DormPasses_DormRooms");
        });

        modelBuilder.Entity<DormResident>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DormResidents_1");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Account).WithMany(p => p.DormResidents)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DormResidents_DormAccounts");

            entity.HasOne(d => d.Pass).WithMany(p => p.DormResidents)
                .HasForeignKey(d => d.PassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DormResidents_DormPasses");
        });

        modelBuilder.Entity<DormRoom>(entity =>
        {
            entity.HasOne(d => d.Dorm).WithMany(p => p.DormRooms)
                .HasForeignKey(d => d.DormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DormRooms_Dorms");
        });

        modelBuilder.Entity<ExamSchedule>(entity =>
        {
            entity.Property(e => e.File).HasColumnType("xml");

            entity.HasOne(d => d.Group).WithMany(p => p.ExamSchedules)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamSchedules_Groups");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(10);
        });

        modelBuilder.Entity<LessonSchedule>(entity =>
        {
            entity.Property(e => e.File).HasColumnType("xml");

            entity.HasOne(d => d.Group).WithMany(p => p.LessonSchedules)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LessonSchedules_Groups");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.Faculty).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.StudentNumber)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.Group).WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_Groups");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);

            entity.HasOne(d => d.DormResident).WithMany(p => p.Users)
                .HasForeignKey(d => d.DormResidentId)
                .HasConstraintName("FK_Users_DormResidents");

            entity.HasOne(d => d.Student).WithMany(p => p.Users)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Students");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
