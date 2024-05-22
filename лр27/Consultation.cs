using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace лр27.Models
{
    public class Consultation
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string TimeSlot { get; set; }
        public bool IsBooked { get; set; }
    }

}
