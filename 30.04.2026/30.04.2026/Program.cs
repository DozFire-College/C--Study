using System;
using System.Collections.Generic;

namespace Shop
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"ID: {Id} | {Name} | Цена: {Price} | В наличии: {Quantity} шт.";
        }
    }

   
    public class Products
    {
        private List<Product> productList;

        public Products()
        {
            productList = new List<Product>();
            InitializeProducts();
        }

        private void InitializeProducts()
        {
            productList.Add(new Product { Id = 1, Name = "Хлеб", Price = 45.50m, Quantity = 50 });
            productList.Add(new Product { Id = 2, Name = "Молоко", Price = 89.90m, Quantity = 30 });
            productList.Add(new Product { Id = 3, Name = "Яйца (10 шт.)", Price = 120.00m, Quantity = 40 });
            productList.Add(new Product { Id = 4, Name = "Сыр", Price = 250.00m, Quantity = 20 });
            productList.Add(new Product { Id = 5, Name = "Колбаса", Price = 350.00m, Quantity = 15 });
            productList.Add(new Product { Id = 6, Name = "Макароны", Price = 75.00m, Quantity = 100 });
            productList.Add(new Product { Id = 7, Name = "Рис", Price = 95.00m, Quantity = 80 });
            productList.Add(new Product { Id = 8, Name = "Курица", Price = 280.00m, Quantity = 25 });
            productList.Add(new Product { Id = 9, Name = "Масло сливочное", Price = 180.00m, Quantity = 35 });
            productList.Add(new Product { Id = 10, Name = "Чай", Price = 150.00m, Quantity = 60 });
        }

        
        public List<Product> GetAllProducts()
        {
            return productList;
        }

      
        public Product GetProductById(int id)
        {
            for (int i = 0; i < productList.Count; i++)
            {
                if (productList[i].Id == id)
                {
                    return productList[i];
                }
            }
            return null;
        }
        public void DisplayProducts()
        {
            Console.WriteLine("ДОСТУПНЫЕ ТОВАРЫ");
            for (int i = 0; i < productList.Count; i++)
            {
                Console.WriteLine(productList[i].ToString());
            }
        }
    }

    public class Balance
    {
        public decimal Amount { get; private set; }

        public Balance(decimal initialAmount)
        {
            Amount = initialAmount;
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Amount += amount;
                Console.WriteLine($"Баланс пополнен на {amount}. Текущий баланс: {Amount}");
            }
            else
            {
                Console.WriteLine("Сумма пополнения должна быть положительной!");
            }
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= Amount)
            {
                Amount -= amount;
                return true;
            }
            return false;
        }

        public bool HasEnoughMoney(decimal amount)
        {
            return Amount >= amount;
        }

        public void DisplayBalance()
        {
            Console.WriteLine($"Текущий баланс: {Amount}");
        }
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal GetTotalPrice()
        {
            return Product.Price * Quantity;
        }

        public override string ToString()
        {
            return $"{Product.Name} x {Quantity} шт. = {GetTotalPrice()}";
        }
    }
    public class Cart
    {
        private List<CartItem> items;

        public Cart()
        {
            items = new List<CartItem>();
        }
        public bool AddProduct(Product product, int quantity)
        {
            if (product == null)
            {
                Console.WriteLine("Товар не найден!");
                return false;
            }

            if (quantity <= 0)
            {
                Console.WriteLine("Количество должно быть больше нуля!");
                return false;
            }

            if (quantity > product.Quantity)
            {
                Console.WriteLine($"Недостаточно товара! В наличии: {product.Quantity} шт.");
                return false;
            }
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Product.Id == product.Id)
                {
                    int newQuantity = items[i].Quantity + quantity;
                    if (newQuantity > product.Quantity)
                    {
                        Console.WriteLine($"Недостаточно товара! Максимально можно добавить: {product.Quantity - items[i].Quantity} шт.");
                        return false;
                    }
                    items[i].Quantity = newQuantity;
                    Console.WriteLine($"Обновлено количество товара '{product.Name}'. Теперь: {items[i].Quantity} шт.");
                    return true;
                }
            }
            items.Add(new CartItem { Product = product, Quantity = quantity });
            Console.WriteLine($"Товар '{product.Name}' добавлен в корзину ({quantity} шт.)");
            return true;
        }
        public bool RemoveProduct(int productId)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Product.Id == productId)
                {
                    Console.WriteLine($"Товар '{items[i].Product.Name}' удален из корзины");
                    items.RemoveAt(i);
                    return true;
                }
            }
            Console.WriteLine("Товар не найден в корзине!");
            return false;
        }
        public void Clear()
        {
            items.Clear();
            Console.WriteLine("Корзина очищена");
        }
        public decimal GetTotalAmount()
        {
            decimal total = 0;
            for (int i = 0; i < items.Count; i++)
            {
                total += items[i].GetTotalPrice();
            }
            return total;
        }
        public void DisplayCart()
        {
            if (items.Count == 0)
            {
                Console.WriteLine(" КОРЗИНА ПУСТА");
                return;
            }

            Console.WriteLine("\nСОДЕРЖИМОЕ КОРЗИНЫ");
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i].ToString()}");
            }
            Console.WriteLine($"\nОбщая сумма: {GetTotalAmount()}");
        }
        public int GetItemCount()
        {
            return items.Count;
        }
        public List<CartItem> GetItems()
        {
            return items;
        }
    }
    public class Shop
    {
        private Products products;
        private Cart cart;
        private Balance balance;

        public Shop()
        {
            products = new Products();
            cart = new Cart();
        }

        public void Start()
        {
            Console.Write("Введите ваш начальный баланс: ");
            decimal initialBalance;
            while (!decimal.TryParse(Console.ReadLine(), out initialBalance) || initialBalance < 0)
            {
                Console.Write("Введите корректную сумму (положительное число): ");
            }
            balance = new Balance(initialBalance);

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
                Console.WriteLine("1. Показать все товары");
                Console.WriteLine("2. Добавить товар в корзину");
                Console.WriteLine("3. Показать корзину");
                Console.WriteLine("4. Удалить товар из корзины");
                Console.WriteLine("5. Очистить корзину");
                Console.WriteLine("6. Пополнить баланс");
                Console.WriteLine("7. Показать баланс");
                Console.WriteLine("8. Оформить заказ");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        products.DisplayProducts();
                        break;
                    case "2":
                        AddProductToCart();
                        break;
                    case "3":
                        cart.DisplayCart();
                        break;
                    case "4":
                        RemoveProductFromCart();
                        break;
                    case "5":
                        cart.Clear();
                        break;
                    case "6":
                        DepositBalance();
                        break;
                    case "7":
                        balance.DisplayBalance();
                        break;
                    case "8":
                        Checkout();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Спасибо за посещение магазина! До свидания!");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        private void AddProductToCart()
        {
            products.DisplayProducts();
            Console.Write("Введите ID товара для добавления в корзину: ");
            int productId;
            if (int.TryParse(Console.ReadLine(), out productId))
            {
                Product product = products.GetProductById(productId);
                if (product != null)
                {
                    Console.Write("Введите количество: ");
                    int quantity;
                    if (int.TryParse(Console.ReadLine(), out quantity))
                    {
                        cart.AddProduct(product, quantity);
                    }
                    else
                    {
                        Console.WriteLine("Неверное количество!");
                    }
                }
                else
                {
                    Console.WriteLine("Товар с таким ID не найден!");
                }
            }
            else
            {
                Console.WriteLine("Неверный ID товара!");
            }
        }

        private void RemoveProductFromCart()
        {
            if (cart.GetItemCount() == 0)
            {
                Console.WriteLine("Корзина пуста!");
                return;
            }

            cart.DisplayCart();
            Console.Write("Введите ID товара для удаления из корзины: ");
            int productId;
            if (int.TryParse(Console.ReadLine(), out productId))
            {
                cart.RemoveProduct(productId);
            }
            else
            {
                Console.WriteLine("Неверный ID товара!");
            }
        }

        private void DepositBalance()
        {
            Console.Write("Введите сумму для пополнения: ");
            decimal amount;
            if (decimal.TryParse(Console.ReadLine(), out amount))
            {
                balance.Deposit(amount);
            }
            else
            {
                Console.WriteLine("Неверная сумма!");
            }
        }

        private void Checkout()
        {
            if (cart.GetItemCount() == 0)
            {
                Console.WriteLine("Корзина пуста! Добавьте товары для оформления заказа.");
                return;
            }

            decimal totalAmount = cart.GetTotalAmount();
            
            Console.WriteLine("\n=== ОФОРМЛЕНИЕ ЗАКАЗА ===");
            cart.DisplayCart();
            balance.DisplayBalance();

            if (!balance.HasEnoughMoney(totalAmount))
            {
                Console.WriteLine($"Недостаточно средств! Не хватает: {totalAmount - balance.Amount:C}");
                return;
            }

            Console.Write("Подтвердить покупку? (y/n): ");
            string confirmation = Console.ReadLine().ToLower();

            if (confirmation == "y" || confirmation == "yes")
            {
                List<CartItem> cartItems = cart.GetItems();
                for (int i = 0; i < cartItems.Count; i++)
                {
                    Product currentProduct = products.GetProductById(cartItems[i].Product.Id);
                    if (currentProduct == null || currentProduct.Quantity < cartItems[i].Quantity)
                    {
                        Console.WriteLine($"Товар '{cartItems[i].Product.Name}' больше недоступен в нужном количестве!");
                        return;
                    }
                }
                if (balance.Withdraw(totalAmount))
                {
                    for (int i = 0; i < cartItems.Count; i++)
                    {
                        Product currentProduct = products.GetProductById(cartItems[i].Product.Id);
                        currentProduct.Quantity -= cartItems[i].Quantity;
                    }

                    Console.WriteLine("Заказ успешно оформлен!");
                    Console.WriteLine($"С вашего баланса списано: {totalAmount}");
                    balance.DisplayBalance();
                    cart.Clear();
                }
                else
                {
                    Console.WriteLine("Ошибка при списании средств!");
                }
            }
            else
            {
                Console.WriteLine("Оформление заказа отменено.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();
            shop.Start();
        }
    }
}