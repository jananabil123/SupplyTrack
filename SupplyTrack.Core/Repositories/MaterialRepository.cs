using SupplyTrack.Core.Interfaces;
using System.Collections.Generic;

namespace SupplyTrack.Core.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly List<Material> materials = new();

        public void Add(Material material)
        {
            materials.Add(material);
        }

        public List<Material> GetAll()
        {
            return materials;
        }
    }
}