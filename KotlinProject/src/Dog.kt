class Dog: Animal
{
    constructor(someFood: String, someLocation: String): super(someFood, someLocation){}

    var isGuardDog: Boolean = false

    override fun makeNoise()
    {
        println("Dog makes noise")
    }

    override fun eat()
    {
        println("Dog eats")
    }

    override fun sleep()
    {
        println("Dog sleeps")
    }
}