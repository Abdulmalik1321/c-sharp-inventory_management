using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.src
{
    public class Item
    {

        private string _name;
        private int _quantity;
        private DateTime _date;


        public Item(string name, int quantity, DateTime? date = null)
        {
            _name = name;
            if (quantity > 0)
            {
                _quantity = quantity;
            }
            else
            {
                throw new Exception("Quantity can only be positive");
            }
            if (date is null)
            {
                _date = DateTime.Now;
            }
            else
            {
                _date = (DateTime)date;
            }

        }

        public string GetName() { return _name; }
        public int GetQuantity() { return _quantity; }
        public DateTime GetDate() { return _date; }
    }

}