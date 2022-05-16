#region Imports
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using AwsDynamoDBLabs.Web.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

namespace AwsDynamoDBLabs.Web.Data
{
    public class MovieRepository : IMovieRepository
    {
        #region Members

        private readonly DynamoDBContext _context;

        #endregion

        #region Ctor

        public MovieRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _context = new DynamoDBContext(dynamoDbClient);
        }

        #endregion

        #region Methods

        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _context.ScanAsync<Movie>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task<Movie> Get(int id, string name)
        {
            return await _context.LoadAsync<Movie>(id, name);
        }

        public async Task Add(Movie movie)
        {
            await _context.SaveAsync(movie);
        }

        public async Task Update(Movie movie)
        {
            await _context.SaveAsync(movie);
        }

        public async Task Delete(Movie movie)
        {
            await _context.SaveAsync(movie);
        }

        public async Task<IEnumerable<Movie>> GetMovieRank(string movieName)
        {
            var config = new DynamoDBOperationConfig
            {
                IndexName = "IX-MovieName"
            };

            return await _context.QueryAsync<Movie>(movieName, config).GetRemainingAsync();
        }

        public async Task<IEnumerable<Movie>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var config = new DynamoDBOperationConfig
            {
                QueryFilter = new List<ScanCondition>
                {
                    new ScanCondition("Name", ScanOperator.BeginsWith, movieName)
                }
            };

            return await _context.QueryAsync<Movie>(userId, config).GetRemainingAsync();
        }

        #endregion
    }
}
