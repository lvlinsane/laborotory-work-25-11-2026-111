using System;

namespace OrderManagement
{
    class Store
    {
        private Product[] products;
        private Order[] orders;
        private int orderCount;
        private int nextOrderId;

        public Store(Product[] products, int maxOrders)
        {
            this.products = products;
            orders = new Order[maxOrders];
            orderCount = 0;
            nextOrderId = 1;
        }

        public int FindProductIndex(int productId)
        {
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].Id == productId)
                {
                    return i;
                }
            }

            return -1;
        }

        public int FindOrderIndex(int orderId)
        {
            for (int i = 0; i < orderCount; i++)
            {
                if (orders[i].OrderId == orderId)
                {
                    return i;
                }
            }

            return -1;
        }

        public bool AddOrder(string customerName, int productId, int quantity)
        {
            if (orderCount == orders.Length)
            {
                Console.WriteLine("Ошибка: массив заказов заполнен");
                return false;
            }

            int productIndex = FindProductIndex(productId);

            if (productIndex == -1)
            {
                Console.WriteLine($"Ошибка: товар с Id {productId} не найден");
                return false;
            }

            if (quantity <= 0)
            {
                Console.WriteLine("Ошибка: количество должно быть больше нуля");
                return false;
            }

            if (quantity > products[productIndex].Quantity)
            {
                Console.WriteLine($"Ошибка: на складе только {products[productIndex].Quantity} шт. товара \"{products[productIndex].Name}\", а заказано {quantity}");
                return false;
            }

            decimal totalAmount = products[productIndex].Price * quantity;

            orders[orderCount] = new Order(nextOrderId, customerName, productId, quantity, totalAmount);
            products[productIndex].DecreaseQuantity(quantity);

            Console.WriteLine($"Заказ №{nextOrderId} добавлен: {products[productIndex].Name}, {quantity} шт., сумма {totalAmount}");

            orderCount++;
            nextOrderId++;
            return true;
        }

        public void ChangeOrderStatus(int orderId, OrderStatus newStatus)
        {
            int index = FindOrderIndex(orderId);

            if (index == -1)
            {
                Console.WriteLine($"Ошибка: заказ №{orderId} не найден");
                return;
            }

            orders[index].ChangeStatus(newStatus);
        }

        public void ShowOrder(int orderId)
        {
            int index = FindOrderIndex(orderId);

            if (index == -1)
            {
                Console.WriteLine($"Заказ №{orderId} не найден");
                return;
            }

            orders[index].DisplayInfo();
        }

        public void ShowOrdersByStatus(OrderStatus status)
        {
            Console.WriteLine($"Заказы со статусом {status}:");

            int found = 0;

            for (int i = 0; i < orderCount; i++)
            {
                if (orders[i].Status == status)
                {
                    orders[i].DisplayInfo();
                    Console.WriteLine();
                    found++;
                }
            }

            if (found == 0)
            {
                Console.WriteLine("Заказов с таким статусом нет");
            }
        }

        public decimal GetTotalOrdersAmount()
        {
            decimal total = 0;

            for (int i = 0; i < orderCount; i++)
            {
                total += orders[i].TotalAmount;
            }

            return total;
        }

        public void ShowStatusCounts()
        {
            Console.WriteLine("Количество заказов по статусам:");

            foreach (OrderStatus status in Enum.GetValues(typeof(OrderStatus)))
            {
                int count = 0;

                for (int i = 0; i < orderCount; i++)
                {
                    if (orders[i].Status == status)
                    {
                        count++;
                    }
                }

                Console.WriteLine($"{status}: {count}");
            }
        }

        public void ShowProducts()
        {
            Console.WriteLine("Список товаров:");

            foreach (Product product in products)
            {
                Console.WriteLine(product);
            }
        }

        public void ShowAllOrders()
        {
            Console.WriteLine("Список всех заказов:");

            for (int i = 0; i < orderCount; i++)
            {
                orders[i].DisplayInfo();
                Console.WriteLine();
            }
        }
    }
}
