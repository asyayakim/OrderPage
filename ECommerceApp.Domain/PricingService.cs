namespace ECommerceApp.Domain;

public class PricingService
{
    public decimal CalculatePrice(ECommerceApp.Domain.Order order)
    {
        //some price logic
        if (order == null)
        {
            return 0;
        }
        return 0;
    }
}