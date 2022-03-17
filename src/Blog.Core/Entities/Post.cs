using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Entities
{
    public class Post : Entity<int>
    {
        public string Title { get; set; }

        public string Html { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
