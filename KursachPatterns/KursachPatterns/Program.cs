using KursachPatterns.Entities;
using KursachPatterns.Factories;
using KursachPatterns.Interfaces;

ICarFactory factory = new SedanFactory();
var sedan = factory.CreateCar("Chevrolet Lacetti");
factory = new SportCarFactory();
var sportCar = factory.CreateCar("Ford Mustang");
factory = new TruckFactory();
var truck = factory.CreateCar("Hyundai Mighty");

sedan.StartMoving();
sportCar.StartMoving();
truck.StartMoving();

sedan.StopMoving();
var carWithCart = new CarWithCart(sedan);
Console.WriteLine("The sedan got a cart");
carWithCart.StartMoving();

truck.StopMoving();
var snowblowerTruck = new SnowBlowerCar(truck);
Console.WriteLine("The truck got a snow blower");
snowblowerTruck.StartMoving();

