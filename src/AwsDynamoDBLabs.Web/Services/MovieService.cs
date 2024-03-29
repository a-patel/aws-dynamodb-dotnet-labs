﻿#region Imports
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using AwsDynamoDBLabs.Web.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using AwsDynamoDBLabs.Web.Data;

#endregion

namespace AwsDynamoDBLabs.Web.Services
{
    public class MovieService : IMovieService
    {
        #region Members

        private readonly IMovieRepository _movieRepository;

        #endregion

        #region Ctor

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        #endregion

        #region Methods

        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _movieRepository.GetAll();
        }

        public async Task<Movie> Get(int id, string name)
        {
            return await _movieRepository.Get(id, name);
        }

        public async Task Add(Movie movie)
        {
            await _movieRepository.Add(movie);
        }

        public async Task Update(Movie movie)
        {
            await _movieRepository.Update(movie);
        }

        public async Task Delete(Movie movie)
        {
            await _movieRepository.Delete(movie);
        }

        public async Task<IEnumerable<Movie>> GetMovieRank(string movieName)
        {
            var config = new DynamoDBOperationConfig
            {
                IndexName = "MovieName-index"
            };

            return await _movieRepository.QueryAsync<Movie>(movieName, config).GetRemainingAsync();
        }

        public async Task<IEnumerable<Movie>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var config = new DynamoDBOperationConfig
            {
                QueryFilter = new List<ScanCondition>
                {
                    new ScanCondition("MovieName", ScanOperator.BeginsWith, movieName)
                }
            };

            return await _movieRepository.QueryAsync<Movie>(userId, config).GetRemainingAsync();
        }

        #endregion
    }
}
