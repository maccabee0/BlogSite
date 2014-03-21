using System.Web.Mvc;

namespace BlogSite.Domain.Entities
{
    public class Media
    {
        public int MediaId { get; set; }

        public byte[] MediaData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string MediaMimeType { get; set; }
    }
}
