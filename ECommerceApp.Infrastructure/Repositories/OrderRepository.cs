﻿using Microsoft.EntityFrameworkCore; 
using ECommerceApp.ApplicationLayer.DTO;
using ECommerceApp.ApplicationLayer.Interfaces;
using ECommerceApp.Domain;
using Order.Infrastructure.Persistence;


namespace Order.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ProductOrder productOrder)
    {
        await _context.Orders.AddAsync(productOrder);
        await _context.SaveChangesAsync();
    }
    public async Task<List<OrderDto>> GetOrdersByCustomerIdAsync(Guid id)
    {
        var orders = await _context.Orders
            .Include(o => o.Items)
            .Where(o => o.CustomerId == id)
            .ToListAsync();

        return orders.Select(order => new OrderDto
        {
            CustomerId = order.CustomerId,
            OrderDate = order.OrderDate,
            TotalPriceWithDiscount = order.TotalPriceWithDiscount,
            TotalPriceWithoutDiscount = order.TotalPriceWithoutDiscount,
            Items = order.Items.Select(item => new OrderItemDto
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.Price,
                Category = item.Category,
                ImageUrl = item.ImageUrl,
                Discount = item.Discount,
                Description = item.Description,
                ProductName = item.ProductName,
                UnitPriceWithDiscount = item.UnitPriceWithDiscount,
            }).ToList()
        }).ToList();
    }

    public async Task<List<OrderDto>> GetAllAsync()
    {
        var products = await _context.Orders.Include(o => o.Items)
            .ToListAsync();
        return products.Select(product => new OrderDto
        {
            Id = product.Id,
            CustomerId = product.CustomerId,
            OrderDate = product.OrderDate,
            TotalPriceWithDiscount = product.TotalPriceWithDiscount,
            TotalPriceWithoutDiscount = product.TotalPriceWithoutDiscount,
            
            Items = product.Items.Select(item => new OrderItemDto
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.Price,
                Category = item.Category,
                ImageUrl = item.ImageUrl,
                Discount = item.Discount,
                UnitPriceWithDiscount = item.UnitPriceWithDiscount,
                ProductName = item.ProductName
            }).ToList()
        }).ToList();
    }

    public async Task<ProductOrder?> RemoveOrderAsync(Guid id)
    {
       var order = await _context.Orders.FindAsync(id);
       _context.Orders.Remove(order?? throw new Exception("Order not found"));
       await _context.SaveChangesAsync();
       return order;
    }

    public async Task<object?> GetOrderByProductOrderIdAsync(Guid productOrderId)
    {
        var product = await _context.Orders.Include(i => i.Items)
            .Where(p => p.Id == productOrderId)
            .ToListAsync();
        return product.FirstOrDefault();

    }
}