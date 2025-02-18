using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Foundation.Cms.Blocks
{
    [ContentType(DisplayName = "Twitter Feed Block",
        GUID = "8ed98895-c4a5-4d4d-8abf-43853bd46bc8",
        GroupName = CmsGroupNames.SocialMedia)]
    [ImageUrl("~/assets/icons/cms/blocks/twitter.png")]
    public class TwitterBlock : FoundationBlockData
    {
        [Display(Name = "Account name", GroupName = SystemTabNames.Content, Order = 10)]
        public virtual string AccountName { get; set; }

        [Range(3, 10)]
        [Display(Name = "Number of items", GroupName = SystemTabNames.Content, Order = 20)]
        public virtual int NumberOfItems { get; set; }

        public override void SetDefaultValues(ContentType pageType)
        {
            base.SetDefaultValues(pageType);

            NumberOfItems = 5;
        }
    }
}