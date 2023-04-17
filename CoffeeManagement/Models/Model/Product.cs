using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeManagement.Models.Model
{
    public class Product
    {
        private int id;
        private string name;
        private double cost;

        public Product(int id, string name, double cost)
        {
            this.id = id;
            this.name = name;
            this.cost = cost;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double Cost { get => cost; set => cost = value; }
    }
}