using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCMS.Models
{
    public class PetitionViewModel
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Orginization is Required")]
        public string Orginization { get; set; }

        public int SigCount { get; set; }

        public int UniqueOrginizations { get; set; }

        public ICollection<Petition> Signatures { get; set; }
    }
}
