using Microsoft.EntityFrameworkCore;
using prj.Context;
using prj.Model;
using prj.Repository.Interface;

namespace prj.Repository.Implementation
{
    public class AdditionalTravellersRepository : IAdditionalTravellers
    {
        private readonly ProfileContext _profileContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionalTravellersRepository"/> class.
        /// </summary>
        /// <param name="context">The ProfileContext to use.</param>

        public AdditionalTravellersRepository(ProfileContext context)
        {
            _profileContext = context;
        }

        #region GetAdditionalTraveller

        /// <summary>
        /// Retrieve all additional travelers from the database.
        /// </summary>
        /// <returns>A collection of additional travelers.</returns>
        /// <exception cref="NullReferenceException">Thrown when no additional travelers are found.</exception>
        public async Task<IEnumerable<AdditionalTraveller>> GetAdditionalTraveller()
        {
            var traveller = await _profileContext.additional_Travellers.ToListAsync();
            if (traveller != null)
            {
                return traveller;
            }
            throw new NullReferenceException("There are no Additional Traveller");
        }

        #endregion

        #region GetAdditionalTravellerById

        /// <summary>
        /// Retrieve an additional traveler by ID from the database.
        /// </summary>
        /// <param name="id">The ID of the additional traveler to retrieve.</param>
        /// <returns>The retrieved additional traveler.</returns>
        /// <exception cref="NullReferenceException">Thrown when the additional traveler is null.</exception>
        public async Task<AdditionalTraveller> GetAdditionalTravellerById(int AdditionalId)
        {
            var traveller = await _profileContext.additional_Travellers.FirstOrDefaultAsync(x => x.AdditionalId == AdditionalId);

            if (traveller != null)
            {
                return traveller;
            }

            throw new NullReferenceException($"Additional Traveller is Null in this id : {AdditionalId}");
        }

        #endregion

        #region GetTravellerByCusId

        /// <summary>
        /// Retrieve additional travelers belonging to a customer from the database.
        /// </summary>
        /// <param name="id">The ID of the customer.</param>
        /// <returns>A list of additional travelers belonging to the customer.</returns>
        /// <exception cref="NullReferenceException">Thrown when no additional travelers are found for the customer.</exception>
        public async Task<List<AdditionalTraveller>> GetAdditionalTravellerByCustomerId(int customerId)
        {
            var adittionaltraveller = await _profileContext.additional_Travellers.Where(x => x.CustomerId == customerId).ToListAsync();

            if (adittionaltraveller != null)
            {
                return adittionaltraveller;
            }
            throw new NullReferenceException($"There are no Customer in this id: {customerId}");
        }

        #endregion

        #region PostTraveller

        /// <summary>
        /// Add a new additional traveler to the database.
        /// </summary>
        /// <param name="additionalTraveller">The additional traveler to add.</param>
        /// <returns>The added additional traveler.</returns>
        public async Task<AdditionalTraveller> AddAdditionalTraveller(AdditionalTraveller additionalTraveller)
        {
            _profileContext.additional_Travellers.Add(additionalTraveller);
            await _profileContext.SaveChangesAsync();
            return additionalTraveller;
        }

        #endregion

        #region PutTraveller

        /// <summary>
        /// Update an existing additional traveler in the database.
        /// </summary>
        /// <param name="id">The ID of the additional traveler to update.</param>
        /// <param name="additionalTraveller">The updated additional traveler data.</param>
        /// <returns>The updated additional traveler.</returns>
        public async Task<AdditionalTraveller> UpdateAdditionalTraveller(int customerId, AdditionalTraveller additionalTraveller)
        {
            _profileContext.Entry(additionalTraveller).State = EntityState.Modified;
            await _profileContext.SaveChangesAsync();
            return additionalTraveller;
        }

        #endregion

        #region DeleteTraveller

        /// <summary>
        /// Delete an additional traveler by ID from the database.
        /// </summary>
        /// <param name="id">The ID of the additional traveler to delete.</param>
        /// <returns>The deleted additional traveler.</returns>
        /// <exception cref="NullReferenceException">Thrown when the additional traveller to delete is null.</exception>

        public async Task<AdditionalTraveller> DeleteAdditionalTraveller(int AdditionalId)
        {
            AdditionalTraveller? traveler = await _profileContext.additional_Travellers.FirstOrDefaultAsync(x => x.AdditionalId == AdditionalId);
            if (traveler != null)
            {
                _profileContext.additional_Travellers.Remove(traveler);
                await _profileContext.SaveChangesAsync();
                return traveler;
            }
            throw new NullReferenceException("Additional traveller is null");

        }

        #endregion
    }
}
