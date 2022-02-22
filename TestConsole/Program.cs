using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Dog dd = new Dog();
            Cat cc = new Cat();

            Child<Dog, Cat> c = new Child<Dog, Cat>(dd, cc);
            Child2<Dog> d = new Child2<Dog>(dd);
            c.print2();
            d.print1();
        }
    }


    abstract public class Base<T2> : Base2<T1>
                            where T2 : Animal, new()
                            where T1 : Animal, new()
    {
        public Base(T1 a, T2 b) : base(a)
        {

        }

        public void print2()
        {
            Console.Write("base2");
        }
    }

    abstract public class Base2<T1> where T1 : Animal, new()
    {
        public Base2(T1 a)
        {

        }
        public void print1()
        {
            Console.Write("base1");
        }
    }
    public class Child<T1, T2> : Base<T1, T2>
                            where T2 : Animal
                            where T1 : Animal
                             , new()
    {
        public Child(T1 cat, T2 dog) : base(cat, dog)
        {

        }
    }

    public class Child2<T1> : Base2<T1> where T1 : Animal
                            , new()
    {

        public Child2(T1 cat) : base(cat)
        {

        }

    }

    public class Animal
    {
        public Animal()
        {

        }
    }
    public class Cat : Animal
    {
        public Cat()
        {

        }
    }
    public class Dog : Animal
    {
        public Dog()
        {

        }
    }
}

