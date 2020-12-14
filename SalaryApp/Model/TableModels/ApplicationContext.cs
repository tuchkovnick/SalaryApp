using System.Data.Entity;

namespace SalaryApp.Model.TableModels
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {            
        }
        public DbSet<CompanyEmployee> CompanyEmployees { get; set; }
        public DbSet<CompanyRelation> CompanyRelations { get; set; }

    }
}
