﻿using ASM_CSharp4_Linhtnph20247.ContextEF;
using ASM_CSharp4_Linhtnph20247.Models;
using ASM_CSharp4_Linhtnph20247.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace ASM_CSharp4_Linhtnph20247.Services
{
    public class CartService : ICartService
    {
        ShopDbContext _shopDbContext;

        public CartService()
        {
            _shopDbContext = new ShopDbContext();
        }
        public List<Cart> GetAllCart()
        {
            return _shopDbContext.Carts.ToList();
        }

        public Cart GetCartById(Guid id)
        {
            return _shopDbContext.Carts.FirstOrDefault(p => p.UserId == id);
        }

        public bool CreateCart(Cart cart)
        {
            try
            {
                cart.CreatedAt = DateTime.Now;
                _shopDbContext.Carts.Add(cart);
                _shopDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateCart(Cart cart)
        {
            return false;
        }

        public bool DeleteCart(Guid id)
        {
            try
            {
                var cart = _shopDbContext.Carts.FirstOrDefault(p => p.UserId == id);
                _shopDbContext.Carts.Remove(cart);
                _shopDbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
