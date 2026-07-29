using SupplyTrack.Core.Interfaces;
using System.Collections.Generic;

namespace SupplyTrack.Core.Repositories
{
    public class SupplyRequestRepository : ISupplyRequestRepository
    {
        private readonly List<SupplyRequest> requests = new();

        public void Add(SupplyRequest request)
        {
            requests.Add(request);
        }

        public List<SupplyRequest> GetAll()
        {
            return requests;
        }
    }
}