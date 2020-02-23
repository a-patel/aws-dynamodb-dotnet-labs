#region Imports
using System.Collections.Generic;
#endregion

namespace AwsDynamoDBLabs.Web.Models
{
    public class MovieModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Director { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public decimal Rating { get; set; }

        public List<string> Actors { get; set; } = new List<string>();
    }
}
