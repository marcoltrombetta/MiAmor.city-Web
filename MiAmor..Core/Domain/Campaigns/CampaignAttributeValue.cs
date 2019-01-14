namespace MiAmor.Core
{
    /// <summary>
    /// Represents a Campaign attribute value
    /// </summary>
    public partial class CampaignAttributeValue : Entity<int>
    {
        /// <summary>
        /// Gets or sets the Campaign attribute mapping identifier
        /// </summary>
        public int CampaignAttributeMappingId { get; set; }

        /// <summary>
        /// Gets or sets the attribute value type identifier
        /// </summary>
        public int AttributeValueTypeId { get; set; }

        /// <summary>
        /// Gets or sets the associated Campaign identifier (used only with AttributeValueType.AssociatedToCampaign)
        /// </summary>
        public int AssociatedCampaignId { get; set; }

        /// <summary>
        /// Gets or sets the Campaign attribute name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the color RGB value (used with "Color squares" attribute type)
        /// </summary>
        public string ColorSquaresRgb { get; set; }

        /// <summary>
        /// Gets or sets the price adjustment (used only with AttributeValueType.Simple)
        /// </summary>
        public decimal PriceAdjustment { get; set; }

        /// <summary>
        /// Gets or sets the weight adjustment (used only with AttributeValueType.Simple)
        /// </summary>
        public decimal WeightAdjustment { get; set; }

        /// <summary>
        /// Gets or sets the attibute value cost (used only with AttributeValueType.Simple)
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Gets or sets the quantity of associated Campaign (used only with AttributeValueType.AssociatedToCampaign)
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the value is pre-selected
        /// </summary>
        public bool IsPreSelected { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the picture (identifier) associated with this value
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// Gets the Campaign attribute mapping
        /// </summary>
        public virtual CampaignAttributeMapping CampaignAttributeMapping { get; set; }
             
    }
}
