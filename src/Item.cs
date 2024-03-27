using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.src
{
    public class Item
    {

        private string? _name;
        private int _quantity;
        private DateTime _date;

        public string Name
        {
            get
            {
                if (_name is not null)
                {
                    return _name;
                }
                else
                {
                    return "Default";
                }
            }
            set { _name = value; }
        }
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value > 0)
                {
                    _quantity = value;
                }
                else
                {
                    throw new Exception("Quantity can only be positive");
                }
            }
        }
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = (DateTime)value;
            }
        }

    }

}