#region Imports
using AwsDynamoDBLabs.Web.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

namespace AwsDynamoDBLabs.Web.Data
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAll();

        Task<Movie> Get(int id, string name);

        Task Add(Movie movie);

        Task Update(Movie movie);

        Task Delete(Movie movie);

        Task<IEnumerable<Movie>> GetMovieRank(string movieName);

        Task<IEnumerable<Movie>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName);
    }
}
