namespace ECommerceApp.Domain;

public class PricingService
{
    public decimal CalculatePrice(ProductOrder productOrder)
    {
        //some price logic
        if (productOrder == null)
        {
            return 0;
        }
        return 0;
    }
}