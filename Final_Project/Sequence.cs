using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    internal class Sequence
    {
        int peopleAmount;
        Person[] people;

        public Sequence(int peopleAmount)
        {
            this.peopleAmount = peopleAmount;
            people = new Person[peopleAmount];
        }
    }
}
