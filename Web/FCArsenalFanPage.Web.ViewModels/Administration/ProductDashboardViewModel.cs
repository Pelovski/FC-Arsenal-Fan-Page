namespace FCArsenalFanPage.Web.ViewModels.Administration
{
    using System.ComponentModel;

    public class ProductDashboardViewModel
    {
        public string ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        [DisplayName("ProductCategory")]
        public string ProductCategory { get; set; }

        [DisplayName("Created By User")]
        public string CreatedByUser { get; set; }

        [DisplayName("Created On")]
        public string CreatedOn { get; set; }

        [DisplayName("Modified On")]
        public string ModifiedOn { get; set; }
    }
}
