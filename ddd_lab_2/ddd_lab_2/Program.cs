using ddd_lab_2.Factories;
using ddd_lab_2.Interfaces;
using ddd_lab_2.Repositories;
using System;
using System.Collections.Generic;

namespace ddd_lab_2
{
    public class Program
    {
        static void Main(string[] args)
        {
            IBuyerFactory buyerFactory = new BuyerFactory();
            IOrderFactory orderFactory = new OrderFactory();
            IBuyerRepository buyerRepository = new BuyerRepository();
            IOrderRepository orderRepository = new OrderRepository();

            IOrder usualOrder = orderFactory.CreateUsualOrder(1, 1000, "created", new List<string>() { "good1", "good2" }, "avenue1", "48h");
            IOrder expressOrder = orderFactory.CreateExpressOrder(2, 1000, "created", new List<string>() { "good3", "good4" }, "avenue2");

            IBuyer foreignBuyer = buyerFactory.CreateForeignBuyer("name1");
            IBuyer goodBuyer = buyerFactory.CreateRussianBuyer("Vova");

            buyerRepository.Add(foreignBuyer);
            buyerRepository.Add(goodBuyer);
            orderRepository.Add(usualOrder);
            orderRepository.Add(expressOrder);

            Console.WriteLine("usual order price: " + orderRepository.GetOrderById(1).Price);
            Console.WriteLine("express order price: " + orderRepository.GetOrderById(2).Price);
            Console.WriteLine("foreign buyer card type: " + buyerRepository.GetBuyerByName("name1").CardType);
            Console.WriteLine("russian buyer card type: " + buyerRepository.GetBuyerByName("Vova").CardType);
        }
    }
}
