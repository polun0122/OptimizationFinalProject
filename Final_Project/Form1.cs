using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            FintnessFunction main = new FintnessFunction(5, 5, Person.Strategy.CooperativeBehavior);
            Console.WriteLine("=====================");
            Console.WriteLine("Cost: " + main.Evaluation(0, 3).ToString());
            Console.WriteLine("=====================");
            Console.WriteLine("Cost: " + main.Evaluation(3, 3).ToString());
            Console.WriteLine("=====================");
        }
    }
}
