using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Runtime.Serialization.DataContracts;

namespace AppCrudNET7.Models
{
    public class Employee
    {
        public int idEmployee { get; set; }
        public string fullName { get; set; }
        public Department refDepartment { get; set; }
        public int salary { get; set; }
        public string dateContract { get; set; }
    }
}

