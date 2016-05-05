using System.Data.Entity;
using OnlineTestEntities;
using System.Web;
using System;

namespace OnlineTestRepository
{
    public class ExamDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<College> Colleges { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Hr> Hrs { get; set; }
        public DbSet<Marks> Markses { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Questionset> Questionsets { get; set; }
        public DbSet<Technicalpanel> Technicalpanels { get; set; }
       // public DbSet<LoginDetails> LoginCredintials { get; set; }
        //public DbSet<Roles> roles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<StudentQuestions> StudentQuestions { get; set; }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public ExamDbContext()
            : base("name=ExamDbContext")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<College>()
                .HasRequired(c => c.Location)
                .WithMany(c => c.Colleges)
                .HasForeignKey(c => c.LocationId)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Questionset>()
            //    .HasMany(c => c.QuantitativeQuestions)
            //    .WithOptional(c => c.QuantitativeQuestionSet)
            //    .HasForeignKey(c => c.QuantitativeQuestionSetID)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Questionset>()
            //   .HasMany(c => c.TechnicalQuestions)
            //   .WithOptional(c => c.TechnicalQuestionSet)
            //   .HasForeignKey(c => c.TechnicalQuestionSetID)
            //   .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r=>r.Users)
                .Map(m =>
                {
                    m.ToTable("UserRoles");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("RoleId");
                });

            modelBuilder.Entity<Technicalpanel>()
                .HasMany<Employee>(e => e.Employees)
                .WithMany(s => s.TechnicalPanels)
                .Map(cs =>
                {
                    cs.MapLeftKey("TechnicalpanelRefId");
                    cs.MapRightKey("EmployeeRefId");
                    cs.ToTable("TechnicalPanelEmployee");
                });

            base.OnModelCreating(modelBuilder);

        }
    }
}
