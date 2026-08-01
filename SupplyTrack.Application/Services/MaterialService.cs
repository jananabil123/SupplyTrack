using System.Linq;
using SupplyTrack.Core;
using SupplyTrack.Core.Interfaces;

namespace SupplyTrack.Application.Services
{
    public class MaterialService
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }
        public void AddMaterial(Material material)
        {
            _materialRepository.Add(material);
        }
        public List<Material> GetAllMaterials()
        {
            return _materialRepository.GetAll();
        }
        public Material? FindMaterialByCode(string code)
        {
            foreach (Material material in _materialRepository.GetAll())
            {
                if (material.Code == code)
                {
                    return material;
                }
            }

            return null;
        }
        public List<Material> GetLowStockMaterials(int threshold)
        {
            return _materialRepository
                .GetAll()
                .Where(material => material.Stock < threshold)
                .ToList();
        }
    }

}
