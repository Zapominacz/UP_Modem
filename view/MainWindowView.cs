using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModemConnect.view {
    interface MainWindowView {

        void showInCommandHistory(String information);
        void listAvailablePorts(String[] ports);
        void setConnectButtonText(String text);
        void setServerButtonText(String text);

    }
}
