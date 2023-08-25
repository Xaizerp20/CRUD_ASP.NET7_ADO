using AppCrudNET7.Models;
using AppCrudNET7.Repositories.Contracts;
using System.Data;
using System.Data.SqlClient;

namespace AppCrudNET7.Repositories.Implementation
{
    // DepartmentRepository: Implementation of the IGenericRepository interface for the Department entity.
    public class DepartmentRepository : IGenericRepository<Department>
    {
        private readonly string _stringSQL = "";

        // DepartmentRepository constructor.
        // Receives an application configuration to retrieve the database connection string.
        public DepartmentRepository(IConfiguration configuration)
        {
            _stringSQL = configuration.GetConnectionString("stringSQL"); // Retrieve the database connection string
        }

        // Method to list all departments.
        public async Task<List<Department>> List()
        {
            List<Department> _list = new List<Department>();

            // Use a database connection using the specified connection string.
            using (var connection = new SqlConnection(_stringSQL))
            {
                connection.Open(); // Open the database connection
                SqlCommand cmd = new SqlCommand("sp_ListDeparments", connection); // Create a command for a specific stored procedure
                cmd.CommandType = CommandType.StoredProcedure; // Specify the command type as a stored procedure

                // Execute the command and get the results
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        // Add the results to the list of departments
                        _list.Add(new Department()
                        {
                            idDepartment = Convert.ToInt32(dr["idDepartment"]),
                            nameDepartment = dr["nameDepartment"].ToString()
                        });
                    }
                }
            }

            return _list; // Return the list of departments
        }

        //TODO: Method to save a department (not implemented).
        public Task<bool> Save(Department model)
        {
            throw new NotImplementedException();
        }

        //TODO: Method to edit a department (not implemented).
        public Task<bool> Edit(Department model)
        {
            throw new NotImplementedException();
        }

        //TODO: Method to delete a department (not implemented).
        public Task<bool> Delete(int idDepartment)
        {
            throw new NotImplementedException();
        }

    }
}
