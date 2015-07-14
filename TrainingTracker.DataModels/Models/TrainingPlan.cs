using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTracker.DataModels.Models
{
    [Table("TrainingPlan")]
    public class TrainingPlan : IModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }

        [ForeignKey("Trainer")]
        public int TrainerID { get; set; }
        public virtual Trainer Trainer { get; set; }

        public virtual ICollection<TrainingSession> TrainingSessions { get; set; }
    }
}
