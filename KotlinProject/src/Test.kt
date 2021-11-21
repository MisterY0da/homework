fun main()
{
    val animals: Array<Animal> = arrayOf(Dog("meat", "Kennel"),
        Cat("fish", "Street"), Horse("hay", "Barnyard"))

    val veterinarian = Veterinarian()

    var i = 0
    while(i < animals.size)
    {
        veterinarian.treatAnimal(animals[i])
        i++
    }
}

