using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeamAssignment4A.Models.JointTables;


namespace TeamAssignment4A.Models
{
    public class Certificate {
        //Basic Info for Certificate

        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Certificate Id")]
        public int Id { get; set; }

        
        [StringLength(20)]
        [Display(Name = "Title Of Certificate")]
        public string TitleOfCertificate { get; set; }

        //public (int, string) CompositeKey => (Id, TitleOfCertificate);
        [Required]        
        [Display(Name = "Passing Grade")]
        public int PassingGrade { get; set; }

        [Required]
        [Display(Name = "Maximum Score")]
        public int MaximumScore { get; set; }


        // Navigation Properties
        public virtual ICollection<Topic>? Topics { get; set; }
        public virtual ICollection<Exam>? Exams { get; set; }
    }
}
