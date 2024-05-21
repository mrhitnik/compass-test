using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compass.Shared.Models
{
    public class Alumni
    {
        public int Id { get; set; }
        public string Major { get; set; }
        public string Name { get; set; }
        public string Graduation { get; set; }
        public string Employment { get; set; }
        public string PhotoUrl { get; set; }
        public string ProfileUrl { get; set; }
        public string DateCollected { get; set; }
    }
}
