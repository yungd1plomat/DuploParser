using System.ComponentModel.DataAnnotations;

namespace DuploParser.Models
{
    public class Filter
    {
        public int Id { get; set; }

        public string? Season { get; set; }

        public bool? Pins { get; set; }

        public double? Width { get; set; }

        public double? Profile { get; set; }

        public double? Radius { get; set; }
        
        public bool? RunFlat { get; set; }

        public string? Code { get; set; }
    }
}
