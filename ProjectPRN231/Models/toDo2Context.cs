using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectPRN231.Models
{
    public partial class toDo2Context : DbContext
    {
        public toDo2Context()
        {
        }

        public toDo2Context(DbContextOptions<toDo2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Attachment> Attachments { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Milestone> Milestones { get; set; } = null!;
        public virtual DbSet<Plant> Plants { get; set; } = null!;
        public virtual DbSet<PlantTag> PlantTags { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<TaskTag> TaskTags { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserSetting> UserSettings { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=.;database=toDo2;uid=sasa;pwd=sasa;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.Property(e => e.UploadedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK__Attachmen__TaskI__4AB81AF0");

                entity.HasOne(d => d.UploadedByNavigation)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.UploadedBy)
                    .HasConstraintName("FK__Attachmen__Uploa__4BAC3F29");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK__Comments__TaskId__46E78A0C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Comments__UserId__47DBAE45");
            });

            modelBuilder.Entity<Milestone>(entity =>
            {
                entity.Property(e => e.ActualEndDate).HasColumnType("datetime");

                entity.Property(e => e.ActualStartDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Dflag).HasColumnName("DFlag");

                entity.Property(e => e.PlannedEndDate).HasColumnType("datetime");

                entity.Property(e => e.PlannedStartDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Plant>(entity =>
            {
                entity.Property(e => e.Dflag).HasColumnName("DFlag");

                entity.Property(e => e.LastUpdate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.Milestone)
                    .WithMany(p => p.Plants)
                    .HasForeignKey(d => d.MilestoneId)
                    .HasConstraintName("FK__Plants__Mileston__30F848ED");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Plants)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Plants__UserId__31EC6D26");
            });

            modelBuilder.Entity<PlantTag>(entity =>
            {
                entity.ToTable("Plant_Tag");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.PlantTags)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("FK__Plant_Tag__Plant__3F466844");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.PlantTags)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK__Plant_Tag__TagId__403A8C7D");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.Dflag).HasColumnName("DFlag");

                entity.Property(e => e.LastUpdate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.Milestone)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.MilestoneId)
                    .HasConstraintName("FK__Tasks__Milestone__35BCFE0A");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("FK__Tasks__PlantId__34C8D9D1");

                entity.HasOne(d => d.TaskParent)
                    .WithMany(p => p.InverseTaskParent)
                    .HasForeignKey(d => d.TaskParentId)
                    .HasConstraintName("FK__Tasks__TaskParen__36B12243");
            });

            modelBuilder.Entity<TaskTag>(entity =>
            {
                entity.ToTable("Task_Tag");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.TaskTags)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK__Task_Tag__TagId__440B1D61");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskTags)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK__Task_Tag__TaskId__4316F928");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Dflag).HasColumnName("DFlag");

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__RoleId__29572725");
            });

            modelBuilder.Entity<UserSetting>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSettings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserSetti__UserI__4E88ABD4");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
