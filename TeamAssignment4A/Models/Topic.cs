﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models.JointTables;


namespace TeamAssignment4A.Models
{
    public class Topic 
    {
        [Key]
        [Required]
        [Display(Name ="Topic Id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Number Of Possible Marks")]
        public int NumberOfPossibleMarks { get; set; }

        
        // Navigation Properties

        public virtual Certificate Certificate { get; set; }        
        public virtual IEnumerable<Stem>? Stems { get; set; }        
    }
}
