using System.Linq;
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
        public List<SupplyRequest> GetRequestsByStatus(RequestStatus status)
        {
            return _supplyRequestRepository
                .GetAll()
                .Where(request => request.Status == status)
                .ToList();
        }
        public Dictionary<string, int> GetTopRequestedMaterials()
        {
            return _supplyRequestRepository
                .GetAll()
                .SelectMany(request => request.Lines)
                .GroupBy(line => line.MaterialCode)
                .OrderByDescending(group => group.Sum(line => line.Quantity))
                .ToDictionary(
                    group => group.Key,
                    group => group.Sum(line => line.Quantity)
                );
        }
        public double GetAverageFulfillmentTime()
        {
            var fulfilledRequests = _supplyRequestRepository
                .GetAll()
                .Where(request => request.FulfilledAt.HasValue);

            if (!fulfilledRequests.Any())
            {
                return 0;
            }

            return fulfilledRequests
                .Average(request => (request.FulfilledAt.Value - request.CreatedAt).TotalHours);
        }
    }
}
