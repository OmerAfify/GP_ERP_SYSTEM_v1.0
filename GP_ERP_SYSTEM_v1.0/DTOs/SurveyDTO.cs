using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddSurveyDTO
    {
        [Required]
        public string SurveyName { get; set; }
        [Required]
        public string SurveyDesc { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int QuestionId { get; set; }
    }
    public class SurveyDTO: AddSurveyDTO
    {
        public int SurveyId { get; set; }
    }
}
