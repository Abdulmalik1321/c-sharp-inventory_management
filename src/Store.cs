using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.VisualBasic;
using System.Reflection.Metadata;
namespace InventoryManagement.src
{

    public class Store
    {

        private List<Item> _items = [];
        private int _capacity = 1000;
        private string _name = "Default";
        private List<User> _users = [];

        public Store()
        {
            string fileName = "store.json";
            string jsonString = File.ReadAllText(fileName);
            StoreReadDTO storeData = JsonSerializer.Deserialize<StoreReadDTO>(jsonString)!;
            Console.WriteLine(storeData.Name);

            if (storeData is not null)
            {
                _items = storeData.Items;
                _capacity = storeData.Capacity;
                _name = storeData.Name;
                _users = storeData.Users;
            }
        }

        public bool CheckUser(string username)
        {
            return _users.Exists(user => user.Name.ToLower() == username.ToLower());
        }

        public UserRole GetUserRole(string username)
        {
            User? user = _users.Find(user => user.Name.ToLower() == username.ToLower());
            if (user is not null)
            {
                Console.WriteLine($"{user.ConvertToRead().Name}");

                return user.ConvertToRead().Role;
            }
            else
            {
                throw new Exception("something went wrong");
            }
        }

        public bool CheckUserPassword(string username, string password)
        {
            User? user = _users.Find(user => user.Name.ToLower() == username.ToLower());
            if (user is not null && user.Password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<UserReadDTO> GetUsers()
        {
            List<UserReadDTO> usersList = new List<UserReadDTO>();

            _users.ForEach(user =>
            {
                usersList.Add(user.ConvertToRead());
            });

            return usersList;
        }
        public void AddUser(User newUser)
        {
            if (this.CheckUser(newUser.Name))
            {
                throw new Exception("User already exists");
            }

            _users.Add(newUser);
            Console.WriteLine($"New user added successfully");
            this.SaveData();
        }

        public void DeleteUser(string username)
        {
            if (this.CheckUser(username))
            {
                User? user = _users.Find(user => user.Name.ToLower() == username.ToLower());
                if (user is not null)
                {
                    _users.Remove(user);
                    Console.WriteLine($"User deleted successfully");
                    this.SaveData();
                }
            }
            else
            {
                throw new Exception("User does not exists");
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
            this.SaveData();

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

        public List<Item> SortByNameAsc(SortOrder order)
        {
            if (order == SortOrder.ASC)
            {
                return _items.OrderBy(item => item.Name).ToList();
            }
            else if (order == SortOrder.DESC)
            {
                return _items.OrderByDescending(item => item.Name).ToList();
            }
            throw new Exception("please specify sorting order (asc or desc)");
        }

        public List<Item> collectionSortedByDate(SortOrder order)
        {
            if (order == SortOrder.ASC)
            {
                return _items.OrderBy(d => d.Date).ToList();
            }
            else if (order == SortOrder.DESC)
            {
                return _items.OrderByDescending(d => d.Date).ToList();
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
                Name = _name,
                Users = _users

            };
        }
    }
    public class StoreReadDTO
    {
        public required List<Item> Items { get; set; }
        public required int Capacity { get; set; }
        public required string Name { get; set; }
        public required List<User> Users { get; set; }

    }
}

