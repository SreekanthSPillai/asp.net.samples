using Sample.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Services.Interfaces
{
	public interface IEmployeeRepository : IGenericRepository<EmployeeModel>
	{
		IQueryable<EmployeeModel> GetPendingRequirements(Guid crmCaseId, IEnumerable<string> barcodeNumber);
		void ChangeStatus(Guid taskId, EmployeeModel status);
	}
}