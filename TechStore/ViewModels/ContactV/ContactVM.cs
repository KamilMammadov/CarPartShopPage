using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.Models;

namespace TechStore.ViewModels.ContactV
{
    public class ContactVM
    {
        public IEnumerable<Contact> Contacts { get; set; }
        public IEnumerable<LeaveUsReply> LeaveUsReplies { get; set; }
    }
}
