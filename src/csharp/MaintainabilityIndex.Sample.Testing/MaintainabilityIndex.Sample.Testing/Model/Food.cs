namespace MaintainabilityIndex.Sample.Testing.Model
{
    /// <summary>
    /// Type representing food that can be classified.
    /// </summary>
    public class Food
    {
        /// <summary>
        /// Gets or sets the external supplier code for the food item. This is used when
        /// no <see cref="InternalCode"/> is available.
        /// </summary>
        public string ExternalSupplierCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the internal code for the food item. This is used as
        /// the primary way to classify an item.
        /// </summary>
        public string InternalCode { get; set; } = string.Empty;
    }
}
