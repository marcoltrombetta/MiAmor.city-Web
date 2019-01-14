using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Core
{
    public abstract class BaseEntity { 
    
    }

    public abstract class Entity<T> : BaseEntity, IEntity<T> 
    {
        [Required]
        [Key]
        public virtual T Id { get; set; }
    }

    public abstract class Person : BaseEntity
    {
        
        public virtual int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool? Gender { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string ImgPath { get; set; }

        public DateTime? MarriageDate { get; set; }

        public virtual List<PersonAddress> PersonAddress { get; set; }
    }
}
