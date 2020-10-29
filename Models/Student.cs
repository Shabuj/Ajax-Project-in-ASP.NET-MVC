using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompleteApp.Models
{
    public class Student
    {
      
        [Key]
        public int StudentId { get; set; }
        [Required(ErrorMessage ="Please insert student name .")]
        [StringLength(30)]
        [Column(TypeName= "Varchar")]
        [Display(Name = "Student Name ")]
        [RegularExpression(@"[a-zA-Z\s]+$", ErrorMessage ="Name Can't be a number.. ")]
        public string StudentName { get; set; }

        [Required(ErrorMessage ="Student Address must be Insert.. ")]
        [Display(Name = "Student Address")]
        [Column(TypeName ="Varchar")]
        [StringLength(30)]
        [DataType(DataType.MultilineText)]
        public string StudentAddress { get; set; }

        [Required(ErrorMessage ="Roll must be Insert please.")]
        public string  StudentRoll { get; set; }
    }
}