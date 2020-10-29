using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CompleteApp.Models
{
    public class StudentContext:DbContext
    {
        public StudentContext():base()
        {
                
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}