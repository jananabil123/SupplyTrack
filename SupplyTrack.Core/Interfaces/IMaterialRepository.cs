using System.Collections.Generic;

namespace SupplyTrack.Core.Interfaces
{
    public interface IMaterialRepository
    {
        void Add(Material material);
        List<Material> GetAll();
    }
}