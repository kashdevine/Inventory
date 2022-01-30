using Inventory.Models;

namespace Inventory.Contracts
{
    /// <summary>
    /// This is responsible for handling the data conenction
    /// for the vendors.
    /// </summary>
    public interface IVendorRepository
    {
        /// <summary>
        /// Gets a list of all the vendors in the database.
        /// </summary>
        /// <returns>IEnumerable of vendors.</returns>
        public Task<IEnumerable<Vendor>> GetVendors();

        /// <summary>
        /// Gets a vendor by it's id.
        /// </summary>
        /// <param name="Id">A Guid.</param>
        /// <returns>A Vendor object.</returns>
        public Task<Vendor> GetVendorById(Guid Id);

        /// <summary>
        /// Gets a vendor by it's name.
        /// </summary>
        /// <param name="Name">A string.</param>
        /// <returns>A Vendor object.</returns>
        public Task<Vendor> GetVendorByName(string Name);

        /// <summary>
        /// Adds a vendor to the database and returns it for use.
        /// </summary>
        /// <param name="Vendor">Vendor object.</param>
        /// <returns>Newly created Vendor.</returns>
        public Task<Vendor> CreateVendor(Vendor Vendor);

        /// <summary>
        /// Checks whether a vendor exists by the name.
        /// </summary>
        /// <param name="VendorName">A string.</param>
        /// <returns>A boolean.</returns>
        public Task<bool> VendorDoesExist(string VendorName);

        /// <summary>
        /// Checks whether a vendor exists by the Id.
        /// </summary>
        /// <param name="VendorId">A Guid.</param>
        /// <returns>A boolean.</returns>
        public Task<bool> VendorDoesExist(Guid VendorId);

        /// <summary>
        /// Updates a vendor and returns it for use.
        /// </summary>
        /// <param name="Vendor">Venor object.</param>
        /// <returns>Updated Vendor object.</returns>
        public Task<Vendor> UpdateVendor(Vendor Vendor);

        /// <summary>
        /// Removes a vendor from the database.
        /// </summary>
        /// <param name="Id">A Guid.</param>
        /// <returns>A boolean.</returns>
        public Task<bool> DeleteVendor(Guid Id);

        /// <summary>
        /// Saves changes to the database if there were any.
        /// </summary>
        /// <returns>A boolean.</returns>
        public Task<bool> Save();
    }
}
