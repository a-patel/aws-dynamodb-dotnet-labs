#region Imports
using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
#endregion

namespace AwsDynamoDBLabs.Web.Entities
{
    [DynamoDBTable("Movie")]
    public class Movie
    {
        [DynamoDBHashKey]
        public int Id { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey]
        public string Name { get; set; }

        public string Director { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public decimal Rating { get; set; }

        public List<string> Actors { get; set; } = new List<string>();
    }
}
