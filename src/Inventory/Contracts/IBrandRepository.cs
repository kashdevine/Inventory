using Inventory.Models;

namespace Inventory.Contracts
{
    /// <summary>
    /// This is responsible for handling the data connection
    /// for the brands
    /// </summary>
    public interface IBrandRepository
    {
        /// <summary>
        /// Get all the brands that are in the database.
        /// </summary>
        /// <returns>IEnumable of brands</returns>
        public Task<IEnumerable<Brand>> GetBrands();

        /// <summary>
        /// Get a brand by it's Id.
        /// </summary>
        /// <param name="Id">A Guid.</param>
        /// <returns>A Brand object.</returns>
        public Task<Brand> GetBrandById(Guid Id);

        /// <summary>
        /// Get a brand by it's name.
        /// </summary>
        /// <param name="Name">A String.</param>
        /// <returns>A Brand object.</returns>
        public Task<Brand> GetBrandByName(string Name);

        /// <summary>
        /// Adds a brand to the db and returns the newly
        /// created brand.
        /// </summary>
        /// <param name="brand">Brand Object.</param>
        /// <returns>Added Brand.</returns>
        public Task<Brand> CreateBrand(Brand brand);

        /// <summary>
        /// Check whether a brand exists in the database
        /// by name.
        /// </summary>
        /// <param name="brandName">A String.</param>
        /// <returns>A boolean.</returns>
        public Task<bool> BrandDoesExist(string brandName);

        /// <summary>
        /// Updates the brand provided.
        /// </summary>
        /// <param name="brand">Brand Object.</param>
        /// <returns>Updated Brand.</returns>
        public Task<Brand> UpdateBrand(Brand brand);

        /// <summary>
        /// Removes a brand from the database.
        /// </summary>
        /// <param name="Id">Brand Guid.</param>
        /// <returns>A boolean.</returns>
        public Task<bool> DeleteBrand(Guid Id);

        /// <summary>
        /// Saves changes to the database fi there was any.
        /// </summary>
        /// <returns>A boolean.</returns>
        public Task<bool> Save();

    }
}
