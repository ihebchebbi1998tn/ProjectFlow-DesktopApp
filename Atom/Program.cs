using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atom
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
          // Application.Run(new home());
          Application.Run(new Form1());
            //Application.Run(new projectchefadd());
       // Application.Run(new Equipe());
       //     Application.Run(new Clients());
     //  Application.Run(new membretableau());
            //   Application.Run(new creecompte());
            //  Application.Run(new motdepassoublie());
         

            // Application.Run(new ressources());

        }
    }
}
