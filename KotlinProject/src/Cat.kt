class Cat: Animal
{
    constructor(someFood: String, someLocation: String): super(someFood, someLocation){}

    var whiskersLength: Int = 5

    override fun makeNoise()
    {
        println("Cat makes noise")
    }

    override fun eat()
    {
        println("Cat eats")
    }

    override fun sleep()
    {
        println("Cat sleeps")
    }
}



