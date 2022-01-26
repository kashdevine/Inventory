﻿using Inventory.Contracts;
using Inventory.Data;
using Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Services
{
    public class VendorRepository : IVendorRepository
    {
        private readonly InventoryContext _ctx;
        public VendorRepository(InventoryContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Vendor> CreateVendor(Vendor Vendor)
        {
            if (Vendor == null)
            {
                throw new ArgumentNullException(nameof(Vendor));
            }

            if (await VendorDoesExist(Vendor.Id))
            {
                return await GetVendorById(Vendor.Id);
            }

            _ctx.Vendors!.Add(Vendor);
            await Save();

            return Vendor;
        }

        public async Task<bool> DeleteVendor(Guid Id)
        {
            if (! await VendorDoesExist(Id))
            {
                return await Save();
            }
            var vendorToDelete = await GetVendorById(Id);

            _ctx.Vendors!.Remove(vendorToDelete);

            return await Save();
        }

        public async Task<Vendor> GetVendorById(Guid Id)
        {
            return await _ctx.Vendors.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Vendor> GetVendorByName(string Name)
        {
            return await _ctx.Vendors.FirstOrDefaultAsync(v=> v.Name == Name);
        }

        public async Task<IEnumerable<Vendor>> GetVendors()
        {
            return  _ctx.Vendors.Where(v => v.Name != null);
        }

        public async Task<bool> Save()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<Vendor> UpdateVendor(Vendor Vendor)
        {
            if (Vendor == null)
            {
                throw new ArgumentNullException(nameof(Vendor));
            }

            if (!await VendorDoesExist(Vendor.Id))
            {
                return null;
            }

            _ctx.Vendors!.Update(Vendor);

            if (await Save())
            {
                return Vendor;
            }


            return null;

        }

        public async Task<bool> VendorDoesExist(string VendorName)
        {
            var existing = await _ctx.Vendors.FirstOrDefaultAsync(v => v.Name == VendorName);
            return existing != null;
        }

        public async Task<bool> VendorDoesExist(Guid VendorId)
        {
            var existing = await _ctx.Vendors.FirstOrDefaultAsync(v => v.Id == VendorId);
            return existing != null;
        }
    }
}