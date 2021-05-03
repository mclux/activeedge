using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveEdge.Infrastructure.DTO
{
    public class EmployeeDto:BaseDto
    {
        //[JsonProperty("employee_id")]
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
