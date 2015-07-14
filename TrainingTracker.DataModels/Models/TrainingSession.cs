using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTracker.DataModels.Models
{
    [Table("TrainingSession")]
    public class TrainingSession : IModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime SessionDateTime { get; set; }
        public decimal? Result { get; set; }

        [ForeignKey("TrainingPlan")]
        public int TrainingPlanId { get; set; }
        public virtual TrainingPlan TrainingPlan { get; set; }
    }
}
