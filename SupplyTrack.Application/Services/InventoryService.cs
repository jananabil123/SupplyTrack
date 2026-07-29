using SupplyTrack.Core;
using SupplyTrack.Core.Interfaces;

namespace SupplyTrack.Application.Services
{
    public class InventoryService
    {
        private readonly IMaterialRepository _materialRepository;

        public InventoryService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }
        public List<Material> GetAllMaterials()
        {
            return _materialRepository.GetAll();
        }
    }
}