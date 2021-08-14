namespace ESourcing.Products.Settings.ProductDatabase
{
    public interface IProductDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }
    }
}
