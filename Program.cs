using System;

namespace OrderManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Product[] products = new Product[5];

            products[0] = new Product(1, "Ноутбук", 450000, 5, ProductCategory.Electronics);
            products[1] = new Product(2, "Футболка", 7000, 40, ProductCategory.Clothing);
            products[2] = new Product(3, "Хлеб", 150, 100, ProductCategory.Food);
            products[3] = new Product(4, "Учебник C#", 12000, 15, ProductCategory.Books);
            products[4] = new Product(5, "Рюкзак", 18000, 12, ProductCategory.Other);

            Store store = new Store(products, 20);

            store.ShowProducts();
            Console.WriteLine();

            Console.WriteLine("Добавление заказов:");
            store.AddOrder("Иванов Иван Иванович", 1, 2);
            store.AddOrder("Петрова Анна Сергеевна", 2, 3);
            store.AddOrder("Сидоров Пётр Олегович", 4, 2);
            store.AddOrder("Ким Алия Маратовна", 3, 10);
            store.AddOrder("Омаров Даулет Ержанович", 5, 1);
            Console.WriteLine();

            Console.WriteLine("Проверка некорректных заказов:");
            store.AddOrder("Тест Тест Тестович", 99, 1);
            store.AddOrder("Тест Тест Тестович", 1, 100);
            store.AddOrder("Тест Тест Тестович", 2, 0);
            Console.WriteLine();

            Console.WriteLine("Изменение статусов:");
            store.ChangeOrderStatus(1, OrderStatus.Confirmed);
            store.ChangeOrderStatus(1, OrderStatus.Processing);
            store.ChangeOrderStatus(2, OrderStatus.Confirmed);
            store.ChangeOrderStatus(3, OrderStatus.Cancelled);
            store.ChangeOrderStatus(4, OrderStatus.Shipped);
            store.ChangeOrderStatus(50, OrderStatus.Confirmed);
            Console.WriteLine();

            Console.WriteLine("Поиск заказа по номеру:");
            store.ShowOrder(2);
            Console.WriteLine();
            store.ShowOrder(50);
            Console.WriteLine();

            store.ShowOrdersByStatus(OrderStatus.Confirmed);
            Console.WriteLine();
            store.ShowOrdersByStatus(OrderStatus.Delivered);
            Console.WriteLine();

            Console.WriteLine($"Общая стоимость всех заказов: {store.GetTotalOrdersAmount()}");
            Console.WriteLine();

            store.ShowStatusCounts();
            Console.WriteLine();

            store.ShowAllOrders();

            store.ShowProducts();

            Console.ReadKey();
        }
    }
}
