using Inventory.Contracts;
using Inventory.Models;

namespace Inventory.Services
{
    public class VendorRepository : IVendorRepository
    {
        public Task<Vendor> CreateVendor(Vendor Vendor)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteVendor(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Vendor> GetVendorById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Vendor> GetVendorByName(string Name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vendor>> GetVendors()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<Vendor> UpdateVendor(Vendor Vendor)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VendorDoesExist(string VendorName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VendorDoesExist(Guid VendorId)
        {
            throw new NotImplementedException();
        }
    }
}
