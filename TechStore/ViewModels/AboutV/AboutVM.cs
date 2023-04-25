using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.Models;

namespace TechStore.ViewModels.AboutV
{
    public class AboutVM
    {
        public About About { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Partner> Partners { get; set; }
        public IEnumerable<Testimonial> Testimonials { get; set; }
        public IEnumerable<Team> Teams { get; set; }


    }
}
