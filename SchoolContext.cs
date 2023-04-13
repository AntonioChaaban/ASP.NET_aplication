using System;

public class SchoolContext: DbContext
{
	public SchoolContext(): base("SchoolContext")
	{

	}

    public DbSet<Student> Student { get; set }
	public DbSet<Enrollment> Enrollments { get; set }
	public DbSet<Course> Courses { get; set }

	protected override void OnModelCreating(DbModelBuilder modelBuilder)
	{
		modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
	}
}
