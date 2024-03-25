using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_management.src
{
    public class Store
    {
        private List<Item> _items = [];
        private int _capacity = 1000;

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

        public string GetItems()
        {
            List<string> itemsString = [];
            _items.ForEach(item => { itemsString.Add(item.GetName()); });
            return String.Join(" - ", itemsString);
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

    }
}