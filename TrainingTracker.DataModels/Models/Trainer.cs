using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTracker.DataModels.Models
{
    [Table("Trainer")]
    public class Trainer : IModel
    {
        [Key]
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }

        public virtual ICollection<TrainingPlan> TrainingPlans { get; set; }
    }
}
