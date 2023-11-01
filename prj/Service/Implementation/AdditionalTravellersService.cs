using prj.Model;
using prj.Repository.Interface;
using prj.Service.Interface;

namespace prj.Service.Implementation
{
    public class AdditionalTravellersService : IAdditinalTravellersService
    {
        private readonly IAdditionalTravellers repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionalTravellersService"/> class.
        /// </summary>
        /// <param name="_repo">The profile repository.</param>
        public AdditionalTravellersService(IAdditionalTravellers _repo)
        {
            repo = _repo;
        }

        #region GetTraveller

        /// <summary>
        /// Get a list of additional travellers.
        /// </summary>
        /// <returns>A list of additional travellers.</returns>
        public Task<IEnumerable<AdditionalTraveller>> GetAdditionalTraveller()
        {
            return repo.GetAdditionalTraveller();
        }

        #endregion

        #region GetTravellerById

        /// <summary>
        /// Get an additional traveller by ID.
        /// </summary>
        /// <param name="id">The ID of the additional traveller to retrieve.</param>
        /// <returns>The additional traveller.</returns>
        public Task<AdditionalTraveller> GetAdditionalTravellerById(int id)
        {
            return repo.GetAdditionalTravellerById(id);
        }

        #endregion

        #region GetTravellerByCusId

        /// <summary>
        /// Get a list of additional travellers by customer ID.
        /// </summary>
        /// <param name="id">The ID of the customer whose additional travellers to retrieve.</param>
        /// <returns>A list of additional travellers belonging to the customer.</returns>
        public async Task<List<AdditionalTraveller>> GetAdditionalTravellerByCustomerId(int id)
        {
            return await repo.GetAdditionalTravellerByCustomerId(id);
        }

        #endregion

        #region PostTraveller

        /// <summary>
        /// Add an additional traveller.
        /// </summary>
        /// <param name="additional_Traveller">The additional traveller to add.</param>
        /// <returns>The added additional traveller.</returns>
        public Task<AdditionalTraveller> AddAdditionalTraveller(AdditionalTraveller additional_Traveller)
        {
            return repo.AddAdditionalTraveller(additional_Traveller);
        }

        #endregion

        #region PutTraveller

        /// <summary>
        /// Update an existing additional traveller.
        /// </summary>
        /// <param name="id">The ID of the additional traveller to update.</param>
        /// <param name="additional_Traveller">The updated additional traveller data.</param>
        /// <returns>The updated additional traveller.</returns>
        public Task<AdditionalTraveller> UpdateAdditionalTraveller(int id, AdditionalTraveller additional_Traveller)
        {
            return repo.UpdateAdditionalTraveller(id, additional_Traveller);
        }

        #endregion

        #region DeleteTraveller

        /// <summary>
        /// Delete an additional traveller by ID.
        /// </summary>
        /// <param name="id">The ID of the additional traveller to delete.</param>
        /// <returns>The deleted additional traveller.</returns>
        public Task<AdditionalTraveller> DeleteAdditionalTraveller(int id)
        {
            return repo.DeleteAdditionalTraveller(id);
        }

        #endregion
    }
}
