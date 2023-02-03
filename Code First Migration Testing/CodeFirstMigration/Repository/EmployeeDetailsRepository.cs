using Dapper;
using System.Data;
using CodeFirstMigration.Models;
using CodeFirstMigration.Data;

namespace CodeFirstMigration.Repository
{
    public class EmployeeDetailsRepository : IEmployeeDetailsRepository
    {
        private readonly DapperContext _context;
        public EmployeeDetailsRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EmployeeDetails>> GetEmployeeDetails()
        {
            try 
            {
                var procedureName = "EmployeeDetails";
                using (var connection = _context.CreateConnection())
                {
                    var reader = await connection.QueryMultipleAsync
                        (sql: procedureName, commandType: CommandType.StoredProcedure);

                    return  reader.Read<EmployeeDetails>().ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }
        public async Task UpdateDepartment(Department department)
        {
            try
            {
                var query = "UpdateDepartment";

                var parameters = new DynamicParameters();
                parameters.Add("DepartmentID", department.DepartmentId, DbType.Int32);
                parameters.Add("DepartmentName", department.DepartmentName, DbType.String);
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
