using SupplyTrack.Core;
using SupplyTrack.Core.Interfaces;

namespace SupplyTrack.Application.Services
{
    public class SupplyRequestService
    {
        private readonly ISupplyRequestRepository _supplyRequestRepository;

        public SupplyRequestService(ISupplyRequestRepository supplyRequestRepository)
        {
            _supplyRequestRepository = supplyRequestRepository;
        }
        public void AddRequest(SupplyRequest request)
        {
            _supplyRequestRepository.Add(request);
        }
        public List<SupplyRequest> GetAllRequests()
        {
            return _supplyRequestRepository.GetAll();
        }
        public SupplyRequest? FindRequestById(int id)
        {
            foreach (SupplyRequest request in _supplyRequestRepository.GetAll())
            {
                if (request.Id == id)
                {
                    return request;
                }
            }

            return null;
        }
    }
}
