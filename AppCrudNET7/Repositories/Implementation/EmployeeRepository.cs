using AppCrudNET7.Models;
using AppCrudNET7.Repositories.Contracts;
using System.Data;
using System.Data.SqlClient;

namespace AppCrudNET7.Repositories.Implementation
{
    public class EmployeeRepository : IGenericRepository<Employee>
    {
        private readonly string _stringSQL = "";

        // DepartmentRepository constructor.
        // Receives an application configuration to retrieve the database connection string.
        public EmployeeRepository(IConfiguration configuration)
        {
            _stringSQL = configuration.GetConnectionString("stringSQL"); // Retrieve the database connection string
        }

        public async Task<List<Employee>> List()
        {
            List<Employee> _list = new List<Employee>();

            // Use a database connection using the specified connection string.
            using (var connection = new SqlConnection(_stringSQL))
            {
                connection.Open(); // Open the database connection
                SqlCommand cmd = new SqlCommand("sp_ListEmployees", connection); // Create a command for a specific stored procedure
                cmd.CommandType = CommandType.StoredProcedure; // Specify the command type as a stored procedure

                // Execute the command and get the results
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        // Add the results to the list of departments
                        _list.Add(new Employee()
                        {
                            idEmployee = Convert.ToInt32(dr["idEmployee"]),
                            fullName = dr["fullName"].ToString(),
                            refDepartment = new Department()
                            {
                                idDepartment = Convert.ToInt32(dr["idDepartment"]),
                                nameDepartment = dr["nameDepartment"].ToString()
                            },
                            salary = Convert.ToInt32(dr["salary"]),
                            dateContract = dr["dateContract"].ToString(),

                        });
                    }
                }
            }

            return _list; // Return the list of departments
        }

        public async Task<bool> Save(Employee model)
        {
            // Create a connection to the database using the provided connection string
            using (var connection = new SqlConnection(_stringSQL))
            {
                connection.Open(); // Open the database connection

                // Create a SqlCommand for a specific stored procedure named "sp_SaveEmployee"
                SqlCommand cmd = new SqlCommand("sp_SaveEmployee", connection);

                // Set parameter values for the stored procedure
                cmd.Parameters.AddWithValue("fullName", model.fullName);
                cmd.Parameters.AddWithValue("idDepartment", model.refDepartment.idDepartment);
                cmd.Parameters.AddWithValue("salart", model.salary);
                cmd.Parameters.AddWithValue("dateContract", model.dateContract);

                cmd.CommandType = CommandType.StoredProcedure; // Specify the command type as a stored procedure

                // Execute the stored procedure asynchronously and get the number of affected rows
                int rows_afected = await cmd.ExecuteNonQueryAsync();

                // Check if any rows were affected and return true if so, otherwise return false
                if (rows_afected > 0)
                    return true;
                else
                    return false;
            }
        }


        public async Task<bool> Edit(Employee model)
        {
            using (var connection = new SqlConnection(_stringSQL))
            {
                connection.Open(); // Open the database connection
                SqlCommand cmd = new SqlCommand("sp_Edit_SaveEmployee", connection); // Create a command for a specific stored procedure
                cmd.Parameters.AddWithValue("idEmployee", model.idEmployee);
                cmd.Parameters.AddWithValue("fullName", model.fullName);
                cmd.Parameters.AddWithValue("idDepartment", model.refDepartment.idDepartment);
                cmd.Parameters.AddWithValue("salart", model.salary);
                cmd.Parameters.AddWithValue("dateContract", model.dateContract);
                cmd.CommandType = CommandType.StoredProcedure; // Specify the command type as a stored procedure'

                int rows_afected = await cmd.ExecuteNonQueryAsync();

                if (rows_afected > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> Delete(int idEmployee)
        {
            using (var connection = new SqlConnection(_stringSQL))
            {
                connection.Open(); // Open the database connection
                SqlCommand cmd = new SqlCommand("sp_DeleteEmployee", connection); // Create a command for a specific stored procedure
                cmd.Parameters.AddWithValue("idEmployee", idEmployee);

                cmd.CommandType = CommandType.StoredProcedure; // Specify the command type as a stored procedure

                int rows_afected = await cmd.ExecuteNonQueryAsync();

                if (rows_afected > 0)
                    return true;
                else
                    return false;
            }
        }

    }
}
