using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2_App.controller;

namespace Workshop2_App
{
    class Program
    {
        static void Main(string[] args)
        {

            //Creating the controller
            Controller controller = new Controller();

            //Getting the view
            controller.showView();
        }

       
    }
}
