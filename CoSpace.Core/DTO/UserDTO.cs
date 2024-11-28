using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Core.DTO
{
    public class UserDTO
    {
        public required string Username {  get; set; }
        public required string FirstName {  get; set; }
        public string LastName { get; set; } = null!;
        public required string Email {  get; set; }
    }
}
