using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class AboutMe
    {
        public string Id { get; set; }

        public string AboutTextEnglish { get; set; }
        public string AboutTextFrench { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}