using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.src
{
    public class Store()
    {
        private List<Item> _items = [];
        private int _capacity = 1000;

        private string _name = "Default";

        public void AddItem(Item newItem)
        {
            _items.ForEach(item =>
            {
                if (item.GetName() == newItem.GetName())
                {
                    throw new Exception("Item already exists");
                }
                else if (this.GetCurrentVolume() + newItem.GetQuantity() > this.GetCapacity())
                {
                    throw new Exception("Store reached maximum capacity");
                }
            });
            _items.Add(newItem);
            Console.WriteLine($"New item added successfully");
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
                totalAmount += item.GetQuantity();
            });

            return totalAmount;
        }

        public Item FindItemByName(string name)
        {
            Item? itemToFind = _items.Find(item => item.GetName().ToLower() == name.ToLower());

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
            return _items.OrderBy(item => item.GetName()).ToList();
        }

        public List<Item> collectionSortedByDate(string order)
        {
            if (order == "asc")
            {
                return _items.OrderByDescending(d => d.GetDate()).ToList();
            }
            else if (order == "desc")
            {
                return _items.OrderBy(d => d.GetDate()).ToList();
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
                if (item.GetDate().AddMonths(3) > DateTime.Now)
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

    }
}