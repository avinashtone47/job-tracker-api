using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Domain.Entities
{
    public class Reminder : BaseEntity
    {
        public DateTime ReminderDate { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsSent { get; set; } = false;

        // Foreign Key
        public int JobApplicationId { get; set; }

        // Navigation Property
        public JobApplication JobApplication { get; set; } = null!;
    }
}
