using System.Collections.Generic;


namespace MiAmor.Core
{
    /// <summary>
    /// Represents a Campaign attribute mapping
    /// </summary>
    public partial class CampaignAttributeMapping : Entity<int>
    {
        private ICollection<CampaignAttributeValue> _CampaignAttributeValues;

        /// <summary>
        /// Gets or sets the Campaign identifier
        /// </summary>
        public int CampaignId { get; set; }

        /// <summary>
        /// Gets or sets the Campaign attribute identifier
        /// </summary>
        public int CampaignAttributeId { get; set; }

        /// <summary>
        /// Gets or sets a value a text prompt
        /// </summary>
        public string TextPrompt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is required
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the attribute control type identifier
        /// </summary>
        public int AttributeControlTypeId { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        //validation fields

        /// <summary>
        /// Gets or sets the validation rule for minimum length (for textbox and multiline textbox)
        /// </summary>
        public int? ValidationMinLength { get; set; }

        /// <summary>
        /// Gets or sets the validation rule for maximum length (for textbox and multiline textbox)
        /// </summary>
        public int? ValidationMaxLength { get; set; }

        /// <summary>
        /// Gets or sets the validation rule for file allowed extensions (for file upload)
        /// </summary>
        public string ValidationFileAllowedExtensions { get; set; }

        /// <summary>
        /// Gets or sets the validation rule for file maximum size in kilobytes (for file upload)
        /// </summary>
        public int? ValidationFileMaximumSize { get; set; }

        /// <summary>
        /// Gets or sets the default value (for textbox and multiline textbox)
        /// </summary>
        public string DefaultValue { get; set; }




     

        /// <summary>
        /// Gets the Campaign attribute
        /// </summary>
        public virtual CampaignAttribute CampaignAttribute { get; set; }

        /// <summary>
        /// Gets the Campaign
        /// </summary>
        public virtual Campaign Campaign { get; set; }
        
        /// <summary>
        /// Gets the Campaign attribute values
        /// </summary>
        public virtual ICollection<CampaignAttributeValue> CampaignAttributeValues
        {
            get { return _CampaignAttributeValues ?? (_CampaignAttributeValues = new List<CampaignAttributeValue>()); }
            protected set { _CampaignAttributeValues = value; }
        }

    }

}
