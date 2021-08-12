namespace ESourcing.Products.Settings.ProductDatabase
{
    public class ProductDatabaseSettings : IProductDatabaseSettings, ISettings
    {
        public string ConnectionStrings { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
