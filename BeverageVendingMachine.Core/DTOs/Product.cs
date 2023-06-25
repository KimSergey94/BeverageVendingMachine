namespace BeverageVendingMachine.Web.Models
{
    public class Product
    {
        public Product(int id, string name, decimal cost, string imageUrl, int storageQuantity, bool isSelected)
        {
            Id = id;
            Name = name;
            Cost = cost;
            ImageUrl = imageUrl;
            StorageQuantity = storageQuantity;
            IsSelected = isSelected;
        }

        /// <summary>
        /// Id of the product
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cost of the product
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Url path to image of the product
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// The quantity of the product inside the vending machine
        /// </summary>
        public int StorageQuantity { get; set; }

        /// <summary>
        /// Whether the product is selected for puchase
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
