using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop2_App.controller;
using Workshop2_App.view;

namespace Workshop2_App
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creating the view
            View view = new View();

            //Creating the controller
            MasterController controller = new MasterController(view);

            //Getting the view
            controller.showView();

            //controller.testDataController();
        }

    }
}
