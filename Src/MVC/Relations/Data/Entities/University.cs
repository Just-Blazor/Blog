using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relations.Data.Entities
{
    public class University
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Code { get; set; }

        public string Location { get; set; }

        public ICollection<Course> Groups { get; set; }

    }
}
