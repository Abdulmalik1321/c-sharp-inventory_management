using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.VisualBasic;
namespace InventoryManagement.src
{

    public class Store
    {

        private List<Item> _items = [];
        private int _capacity = 1000;

        private string _name = "Default";

        public List<Item> Items { get { return _items; } set { _items = value; } }
        public int Capacity { get { return _capacity; } set { _capacity = value; } }
        public string Name { get { return _name; } set { _name = value; } }

        public Store()
        {
            // using FileStream openStream = File.OpenRead("store.json");
            string fileName = "store.json";
            string jsonString = File.ReadAllText(fileName);
            StoreReadDTO storeData = JsonSerializer.Deserialize<StoreReadDTO>(jsonString)!;
            Console.WriteLine(storeData.Name);

            if (storeData is not null)
            {
                _items = storeData.Items;
                _capacity = storeData.Capacity;
                _name = storeData.Name;
            }
        }




        public void AddItem(Item newItem)
        {
            _items.ForEach(item =>
            {
                if (item.Name == newItem.Name)
                {
                    throw new Exception("Item already exists");
                }
                else if (this.GetCurrentVolume() + newItem.Quantity > this.GetCapacity())
                {
                    throw new Exception("Store reached maximum capacity");
                }
            });
            _items.Add(newItem);
            Console.WriteLine($"New item added successfully");
            this.SaveData();
        }

        private void SaveData()
        {
            string jsonString = JsonSerializer.Serialize(this.ConvertToRead());
            File.WriteAllText("store.json", jsonString);
        }

        public void DeleteItem(Item itemToDelete)
        {
            _items.Remove(itemToDelete);
            Console.WriteLine($"Item deleted successfully");

        }

        public string GetName()
        {
            return _name;
        }
        public void ChangeName(string name)
        {
            _name = name;
            this.SaveData();
        }
        public List<Item> GetItems()
        {
            List<Item> itemsString = [];
            _items.ForEach(item => { itemsString.Add(item); });
            return itemsString;
        }

        public int GetCapacity()
        {
            return _capacity;
        }

        public void ChangeCapacity(int newCapacity)
        {
            if (newCapacity > 0)
            {
                _capacity = newCapacity;
                Console.WriteLine($"Store maximum capacity updated successfully");
                this.SaveData();
            }
            else
            {
                Console.WriteLine($"Store capacity can only be positive");
            }
        }

        public int GetCurrentVolume()
        {
            int totalAmount = 0;

            _items.ForEach(item =>
            {
                totalAmount += item.Quantity;
            });

            return totalAmount;
        }

        public Item FindItemByName(string name)
        {
            Item? itemToFind = _items.Find(item => item.Name.ToLower() == name.ToLower());

            if (itemToFind is null)
            {
                throw new Exception($"Item {name} does not exists");
            }
            else
            {
                return itemToFind;
            }
        }

        public List<Item> SortByNameAsc()
        {
            return _items.OrderBy(item => item.Name).ToList();
        }

        public List<Item> collectionSortedByDate(string order)
        {
            if (order == "asc")
            {
                return _items.OrderByDescending(d => d.Date).ToList();
            }
            else if (order == "desc")
            {
                return _items.OrderBy(d => d.Date).ToList();
            }
            else
            {
                throw new Exception("please specify sorting order (asc or desc)");
            }
        }

        public Dictionary<string, List<Item>> GroupByDate()
        {
            Dictionary<string, List<Item>> groupedItems = new Dictionary<string, List<Item>>();
            // {
            //     {"New", []},
            //     }; // New Old

            _items.ForEach(item =>
            {
                if (item.Date.AddMonths(3) > DateTime.Now)
                {
                    if (groupedItems.ContainsKey("New"))
                    {
                        groupedItems["New"].Add(item);
                    }
                    else
                    {
                        groupedItems.Add("New", [item]);
                    }
                }
                else
                {
                    if (groupedItems.ContainsKey("Old"))
                    {
                        groupedItems["Old"].Add(item);
                    }
                    else
                    {
                        groupedItems.Add("Old", [item]);
                    }
                }
            });

            return groupedItems;
        }

        public StoreReadDTO ConvertToRead()
        {
            return new StoreReadDTO
            {
                Items = _items,
                Capacity = _capacity,
                Name = _name

            };
        }
    }
    public class StoreReadDTO
    {
        public required List<Item> Items { get; set; }
        public required int Capacity { get; set; }
        public required string Name { get; set; }
    }
}

