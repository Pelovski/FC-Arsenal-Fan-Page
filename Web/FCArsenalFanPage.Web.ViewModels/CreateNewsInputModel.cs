namespace FCArsenalFanPage.Web.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class CreateNewsInputModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string CreatedByUserId { get; set; }

        public string ImageId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
