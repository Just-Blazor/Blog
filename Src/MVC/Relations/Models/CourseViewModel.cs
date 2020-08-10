using Relations.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relations.Models
{
    public class CourseViewModel : Course
    {
        public int UniversityId { get; set; }
    }
}
