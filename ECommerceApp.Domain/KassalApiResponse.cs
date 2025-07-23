using Newtonsoft.Json;

namespace ECommerceApp.Domain;
public class KassalApiResponse
{
    public List<KassalProduct> Data { get; set; }
}
public class KassalProduct
{
    public string Name { get; set; }
    public string Brand { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public string Ingredients { get; set; }
    [JsonProperty("current_price")]
    public decimal CurrentPrice { get; set; }
    public Nutrition[] Nutrition { get; set; }
    public Store Store { get; set; } 
}
public class Nutrition
{
    [JsonProperty("display_name")]
    public string DisplayName { get; set; }
    public decimal Amount { get; set; }
    public string Unit { get; set; }
}

public class Store
{
    public string Name { get; set; }
}