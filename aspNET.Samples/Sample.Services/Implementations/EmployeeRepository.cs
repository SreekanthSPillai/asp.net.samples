using System;
using System.Collections.Generic;
using System.Linq;
using Sample.Data;
using Sample.Services.Interfaces;

namespace Sample.Services.Implementations
{
	public class EmployeeRepository : GenericRepository<EmployeeModel>, IEmployeeRepository
    {
		public EmployeeRepository(EmployeeDBContext context) : base(context)
		{
		}

		public IQueryable<EmployeeModel> GetEmployees(IEnumerable<string> employeeIds)
		{
			return GetAll().OfType<EmployeeModel>()
					.Where(prt => prt.Name == "ABC");
		}

	}
}