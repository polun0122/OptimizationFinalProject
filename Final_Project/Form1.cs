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
            Toilet toilet = new Toilet(3, 3);
            Console.WriteLine(toilet.ToiletAmount());
            Console.WriteLine(toilet.Distribution());
            Console.WriteLine(toilet.DistanceToToilet(10));
            Console.WriteLine(toilet.OccupyToilet(1));
            Console.WriteLine(toilet.IsToiletOccupied(1));
            Console.WriteLine(toilet.ReleaseToilet(1));
            Console.WriteLine(toilet.IsToiletOccupied(1));
        }
    }
}
