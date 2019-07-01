using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ModelProject
{
    public class EmployeeDBContext:DbContext
    {
        public DbSet<EmployeeModel> Employees { get; set; }
    }
}
