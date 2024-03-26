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

        public string Name
        {
            get { return _name; }
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
                if (value != null)
                {
                    _date = DateTime.Now;
                }
                else
                {
                    _date = (DateTime)value;
                }
            }
        }




        // public string GetName() { return _name; }
        // public int GetQuantity() { return _quantity; }
        // public DateTime GetDate() { return _date; }

        // public override string ToString()
        // {
        //     return _name;
        // }
    }

}