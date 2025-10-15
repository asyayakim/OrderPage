using ECommerceApp.ApplicationLayer.Interfaces;


using ECommerceApp.Domain.Interfaces;

namespace ECommerceApp.ApplicationLayer.Services;

public class FavoriteManager : IFavoriteManager
{
    private readonly IUserDataFavBasket _userDataFavBasket;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productService;

    public FavoriteManager(IUserDataFavBasket userDataFavBasket, ICustomerRepository customerRepository,IProductRepository productService)
    {
        _userDataFavBasket = userDataFavBasket;
        _customerRepository = customerRepository;
        _productService = productService;
    }

    public async Task<List<object?>> GetAllFavorites()
    {
       var favorites = await _userDataFavBasket.GetAllFavoritesFromDb();
       return [favorites];
    }

    public async Task<object?> AddFavorite(string userId, Guid productId)
    {
        var customer = await _customerRepository.GetByIdAsync(Guid.Parse(userId));
        if (customer == null)
            return new { Message = "Customer not found" };
        var product = await _productService.GetProductById(productId);
        if (product == null)
            return new { Message = "Product not found" };
        var favorite = await _userDataFavBasket.AddFavorite(
            customer.Id, product.ProductId, product.Store.StoreId);

        if (favorite == null)
            return new { Message = "Already favorite" };
        return favorite;
    }

    public async Task<object?> DeleteFavorite(string userId, Guid productId)
    {
        return await _userDataFavBasket.DeleteFavorite(userId, productId);
    }

    public async Task<List<object?>> GetProductsFromBasketAsync()
    {
        var products= await _userDataFavBasket.GetAllProductsFromBasketsAllCustomersFromDb();
        return [products];
    }
}