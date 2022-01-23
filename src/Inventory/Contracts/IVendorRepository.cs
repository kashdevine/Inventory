using Inventory.Models;

namespace Inventory.Contracts
{
    public interface IVendorRepository
    {
        public Task<IEnumerable<Vendor>> GetVendors();

        public Task<Vendor> GetVendorById(Guid Id);

        public Task<Vendor> GetVendorByName(string Name);

        public Task<Vendor> CreateVendor(Vendor Vendor);

        public Task<bool> VendorDoesExist(string VendorName);

        public Task<bool> VendorDoesExist(Guid VendorId);

        public Task<Vendor> UpdateVendor(Vendor Vendor);

        public Task<bool> DeleteVendor(Guid Id);

        public Task<bool> Save();
    }
}
