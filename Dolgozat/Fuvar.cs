using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolgozat
{
    internal class Fuvar
    {
        private int id;
        private DateTime startDate;
        private int travelTime;
        private double distance;
        private double price;
        private double tip;
        private string payMethod;

        public Fuvar(int id, DateTime startDate, int travelTime, double distance, double price, double tip, string payMethod) {
            this.id = id;
            this.startDate = startDate;
            this.travelTime = travelTime;
            this.distance = distance;
            this.price = price;
            this.tip = tip;
            this.payMethod = payMethod;
        }

        public int Id { get { return id; } }
        public DateTime StartDate { get { return startDate; } }
        public int TravelTime { get {  return travelTime; } }
        public double Distance { get { return distance; } }
        public double Price { get { return price; } }
        public double Tip { get { return tip; } }
        public string PayMethod { get { return payMethod; } }
    }
}
