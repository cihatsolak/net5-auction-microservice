namespace ESourcing.Products.Settings.ProductDatabase
{
    public interface IProductDatabaseSettings
    {
        string ConnectionStrings { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }
    }
}
