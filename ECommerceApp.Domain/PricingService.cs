namespace ECommerceApp.Domain;

public class PricingService
{
    public decimal CalculatePrice(ProductOrder productOrder)
    {
        var category = productOrder.Items.Select(item => item.Category).Distinct().Single();
        var amount = productOrder.Items.Select(item => item.Quantity).Sum();
        var price = productOrder.Items.Select(item => item.Price).Sum();
        if (category == "fruit" && amount >= 3)
        {
            price *= 0.8m;
        }
        return price;
    }
}