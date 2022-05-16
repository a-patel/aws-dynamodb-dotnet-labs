#region Imports
using AwsDynamoDBLabs.Web.Models;
using AwsDynamoDBLabs.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

namespace AwsDynamoDBLabs.Web.Controllers
{
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieModel>> GetAllItemsFromDatabase()
        {
            var results = await _movieService.GetAll();

            return results;
        }

        [HttpGet]
        [Route("{userId}/{movieName}")]
        public async Task<MovieModel> GetMovie(int userId, string movieName)
        {
            var result = await _movieService.Get(userId, movieName);

            return result;
        }

        [HttpGet]
        [Route("user/{userId}/rankedMovies/{movieName}")]
        public async Task<IEnumerable<MovieModel>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var results = await _movieService.GetUsersRankedMoviesByMovieTitle(userId, movieName);

            return results;
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task<IActionResult> AddMovie(int userId, [FromBody] MovieRankRequest movieRankRequest)
        {
            await _movieService.Add(userId, movieRankRequest);

            return Ok();
        }

        [HttpPatch]
        [Route("{userId}")]
        public async Task<IActionResult> UpdateMovie(int userId, [FromBody] MovieUpdateRequest request)
        {
            await _movieService.Update(userId, request);

            return Ok();
        }

        [HttpGet]
        [Route("{movieName}/ranking")]
        public async Task<MovieRankResponse> GetMoviesRanking(string movieName)
        {
            var result = await _movieService.GetMovieRank(movieName);

            return result;
        }
    }
}
