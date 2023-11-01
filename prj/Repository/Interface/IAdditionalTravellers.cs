using prj.Model;


namespace prj.Repository.Interface

{
    public interface IAdditionalTravellers
    {
        public Task<IEnumerable<AdditionalTraveller>> GetAdditionalTraveller();
        public Task<AdditionalTraveller> GetAdditionalTravellerById(int AdditionalId);
        public Task<List<AdditionalTraveller>> GetAdditionalTravellerByCustomerId(int customerId);
        public Task<AdditionalTraveller> AddAdditionalTraveller(AdditionalTraveller additionalTraveller);
        public Task<AdditionalTraveller> UpdateAdditionalTraveller(int customerId, AdditionalTraveller additionalTraveller);
        public Task<AdditionalTraveller> DeleteAdditionalTraveller(int AdditionalId);
    }
}
