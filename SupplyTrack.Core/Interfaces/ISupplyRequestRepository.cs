using System.Collections.Generic;

namespace SupplyTrack.Core.Interfaces
{
    public interface ISupplyRequestRepository
    {
        void Add(SupplyRequest request);
        List<SupplyRequest> GetAll();
    }
}