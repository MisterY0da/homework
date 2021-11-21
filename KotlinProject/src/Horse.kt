class Horse: Animal
{
    constructor(someFood: String, someLocation: String): super(someFood, someLocation){}

    var color: String = "white"

    override fun makeNoise()
    {
        println("Horse makes noise")
    }

    override fun eat()
    {
        println("Horse eats")
    }

    override fun sleep()
    {
        println("Horse sleeps")
    }
}