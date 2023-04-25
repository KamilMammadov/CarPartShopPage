using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.Models
{
    public class LeaveUsReply
    {
        public int Id { get; set; }

        [StringLength(400)]
        public string Name { get; set; }

        [StringLength(400)]
        public string Surname { get; set; }

        [StringLength(400)]
        public string Subject { get; set; }

        [StringLength(2400)]
        public string Comment { get; set; }
    }
}
