using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeManagement.Models.Model
{
    public class Orders
    {
        private int id;
        private int tableNumber;
        private DateTime time;
        private byte state;
        private List<Product> listProduct;
        private List<int> quantity;


        public Orders(int id, int tableNumber, DateTime time, byte state)
        {
            this.id = id;
            this.tableNumber = tableNumber;
            this.time = time;
            this.state = state;
        }

        public Orders(List<Product> listProduct, List<int> quantity)
        {
            this.listProduct = listProduct;
            this.quantity = quantity;
        }

        public int Id { get => id; set => id = value; }
        public int TableNumber { get => tableNumber; set => tableNumber = value; }
        public DateTime Time { get => time; set => time = value; }
        public byte State { get => state; set => state = value; }
        public List<Product> ListProduct { get => listProduct; set => listProduct = value; }
        public List<int> Quantity { get => quantity; set => quantity = value; }
    }
}