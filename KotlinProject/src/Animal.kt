open class Animal
{
    var food: String
    var location: String

    constructor(someFood: String, someLocation: String)
    {
        this.food = someFood
        this.location = someLocation
    }

    open fun makeNoise()
    {
        println("Animal makes noise")
    }

    open fun eat()
    {
        println("Animal eats")
    }

    open fun sleep()
    {
        println("Animal sleeps")
    }
}