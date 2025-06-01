namespace ECommerceApp.Domain;

public class PricingService
{
    public decimal CalculatePrice(ECommerceApp.Domain.Product product)
    {
        //some price logic
        if (product == null)
        {
            return 0;
        }
        return 0;
    }
}