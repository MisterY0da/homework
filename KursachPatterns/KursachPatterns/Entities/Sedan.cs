using KursachPatterns.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursachPatterns.Entities
{
    public class Sedan : ICar
    {
        public string Model { get; }

        public string EngineType { get; }

        public int Horsepower { get; }

        public int DoorsCount { get; }

        public string FuelType { get; }

        public Sedan(string model)
        {
            Model = model;
            EngineType = "V4";
            Horsepower = 100;
            DoorsCount = 4;
            FuelType = "petrol";
        }

        public void StartMoving()
        {
            Console.WriteLine("Sedan " + Model + " started moving");
        }

        public void StopMoving()
        {
            Console.WriteLine("Sedan " + Model + " stopped moving");
        }
    }
}
