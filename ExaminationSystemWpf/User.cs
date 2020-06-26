using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.DynamicData;

namespace ExaminationSystemWpf
{
    [TableName("User")]
    public class User
    {
        public User() 
        {
            IsDeleted = false;
        }

        public int ID { get; set; }
        [MaxLength(30), Index(IsUnique = true)]

        [Required]
        public string Name { get; set; }


        [MaxLength(30)]
        [Required]
        public string Password { get; set; }

        
        
        public Nullable<bool> IsDeleted { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}

