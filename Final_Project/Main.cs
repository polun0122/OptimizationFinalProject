using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    internal class Main
    {
        int peopleAmount;
        public Person[] sequence;

        //public Main(int peopleAmount, Person.Strategy strategy)
        //{
        //    this.peopleAmount = peopleAmount;
        //    sequence = new Person[peopleAmount];
        //    for (int i = 0; i < peopleAmount; i++)
        //    {
        //        sequence[i] = new Person(strategy);
        //    }
        //}

        //public void Evaluation(int row, int amountPerRow)
        //{
        //    Toilet toilet = new Toilet(row, amountPerRow);

        //}

        public void Test()
        {
            Toilet toilet = new Toilet(2, 6);
            //Console.WriteLine(toilet.ToiletAmount());
            Console.WriteLine(toilet.Distribution());
            //Console.WriteLine(toilet.DistanceToToilet(10));
            Console.WriteLine(toilet.OccupyToilet(0, 0));
            Console.WriteLine(toilet.OccupyToilet(0, 4));
            Console.WriteLine(toilet.OccupyToilet(1, 2));
            //Console.WriteLine(toilet.IsToiletOccupied(1));
            //Console.WriteLine(toilet.ReleaseToilet(1));
            //Console.WriteLine(toilet.IsToiletOccupied(1));
            var status = toilet.ToiletStatus();

            Person sam = new Person(Person.Strategy.CooperativeBehavior);
            int[] a = sam.ChosenToiletIndex(toilet);
            Console.WriteLine(a[0].ToString()+ ","+ a[1].ToString());
        }
    }
    
}
