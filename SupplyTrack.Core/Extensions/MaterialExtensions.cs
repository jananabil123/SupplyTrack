namespace SupplyTrack.Core.Extensions
{
    public static class MaterialExtensions
    {
        public static bool IsLowStock(this Material material, int threshold)
        {
            return material.Stock<= threshold;
        }
    }
}