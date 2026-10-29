using FoodMenuConsole.Helper;
using FoodMenuConsole.Models;
using FoodMenuConsole.Services;

namespace FoodMenuConsole.Menus
{
    public class FoodMenu
    {
        private readonly FoodService _service;

        public FoodMenu(FoodService service)
        {
            _service = service;
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("===== FOOD MENU MANAGEMENT =====");
                Console.WriteLine("1. Add Food Item");
                Console.WriteLine("2. View All Food Items");
                Console.WriteLine("3. Search Food by ID");
                Console.WriteLine("4. Search Food by Category");
                Console.WriteLine("5. Update Food Item");
                Console.WriteLine("6. Delete Food Item");
                Console.WriteLine("7. View Available Food Items");
                Console.WriteLine("0. Exit");

                int choice = ConsoleHelper.ReadInt("\nEnter your choice: ");

                try
                {
                    switch (choice)
                    {
                        case 1:
                            AddFood();
                            break;

                        case 2:
                            ShowFoods(_service.GetAll());
                            break;

                        case 3:
                            SearchFood();
                            break;

                        case 4:
                            SearchByCategory();
                            break;

                        case 5:
                            UpdateFood();
                            break;

                        case 6:
                            DeleteFood();
                            break;

                        case 7:
                            ShowFoods(_service.GetAvailable());
                            break;

                        case 0:
                            running = false;
                            Console.WriteLine("Goodbye!");
                            continue;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong: " + ex.Message);
                }

                ConsoleHelper.Pause();
            }
        }

        private void AddFood()
        {
            Console.WriteLine("\nAdd Food Item");

            FoodItem item = new FoodItem
            {
                Name = ConsoleHelper.ReadRequired("Food name: "),
                Category = CategoryMenu.ChooseCategory(),
                Description = ConsoleHelper.ReadOptional("Description: "),
                Price = ConsoleHelper.ReadDecimal("Price: "),
                IsAvailable = ConsoleHelper.ReadYesNo("Is it available? (y/n): ")
            };

            if (string.IsNullOrWhiteSpace(item.Description))
                item.Description = null;

            if (_service.Add(item, out string message))
                Console.WriteLine(message);
            else
                Console.WriteLine(message);
        }

        private void SearchFood()
        {
            int id = ConsoleHelper.ReadInt("Enter food ID: ");

            FoodItem? item = _service.GetById(id);

            if (item == null)
            {
                Console.WriteLine("Food item not found.");
                return;
            }

            ShowFoods(new List<FoodItem> { item });
        }

        private void SearchByCategory()
        {
            string category = CategoryMenu.ChooseCategory();

            ShowFoods(_service.GetByCategory(category));
        }

        private void UpdateFood()
        {
            int id = ConsoleHelper.ReadInt("Enter food ID to update: ");

            FoodItem? existing = _service.GetById(id);

            if (existing == null)
            {
                Console.WriteLine("Food item not found.");
                return;
            }

            Console.WriteLine("\nCurrent food item:");
            ShowFoods(new List<FoodItem> { existing });

            FoodItem updated = new FoodItem
            {
                Id = existing.Id,
                Name = ConsoleHelper.ReadRequired("New food name: "),
                Category = CategoryMenu.ChooseCategory(),
                Description = ConsoleHelper.ReadOptional("New description: "),
                Price = ConsoleHelper.ReadDecimal("New price: "),
                IsAvailable = ConsoleHelper.ReadYesNo("Is it available? (y/n): ")
            };

            if (string.IsNullOrWhiteSpace(updated.Description))
                updated.Description = null;

            _service.Update(updated, out string message);
            Console.WriteLine(message);
        }

        private void DeleteFood()
        {
            int id = ConsoleHelper.ReadInt("Enter food ID to delete: ");

            FoodItem? item = _service.GetById(id);

            if (item == null)
            {
                Console.WriteLine("Food item not found.");
                return;
            }

            ShowFoods(new List<FoodItem> { item });

            bool confirm = ConsoleHelper.ReadYesNo(
                "Are you sure you want to delete this item? (y/n): ");

            if (!confirm)
            {
                Console.WriteLine("Deletion cancelled.");
                return;
            }

            _service.Delete(id, out string message);
            Console.WriteLine(message);
        }

        private void ShowFoods(List<FoodItem> foods)
        {
            Console.WriteLine("\nFood Items");

            if (foods.Count == 0)
            {
                Console.WriteLine("No food items found.");
                return;
            }

            foreach (FoodItem food in foods)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"ID: {food.Id}");
                Console.WriteLine($"Name: {food.Name}");
                Console.WriteLine($"Category: {food.Category}");
                Console.WriteLine($"Description: {food.Description ?? "None"}");
                Console.WriteLine($"Price: ₹{food.Price:F2}");
                Console.WriteLine(
                    $"Availability: {(food.IsAvailable ? "Available" : "Not available")}");
            }

            Console.WriteLine("--------------------------------");
        }
    }
}
