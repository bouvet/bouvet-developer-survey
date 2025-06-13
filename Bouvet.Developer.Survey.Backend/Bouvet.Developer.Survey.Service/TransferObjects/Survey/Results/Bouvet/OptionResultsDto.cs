using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Bouvet
{
    public class OptionResultDto
    {
        public string Id { get; set; } = "";
        public string Label { get; set; } = "";
        public int Count { get; set; }
        public int AdmiredPercentage { get; set; }
        public int DesiredPercentage { get; set; }
    }

}
