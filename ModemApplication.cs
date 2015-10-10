using System;
using System.Windows.Forms;

namespace ModemConnect.View {
    static class ModemApplication {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindowView());
        }
    }
}
