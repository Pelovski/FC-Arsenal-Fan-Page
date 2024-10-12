namespace FCArsenalFanPage.Web.ViewModels.Administration
{
    using System.ComponentModel;

    public class NewsDashboardViewModel
    {
        public int NewsId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [DisplayName("Created By User")]
        public string UserName { get; set; }

        [DisplayName("Category")]
        public string CategoryName { get; set; }

        [DisplayName("Created On")]
        public string CreatedOn { get; set; }

        [DisplayName("Modified On")]
        public string ModifiedOn { get; set; }

    }
}
