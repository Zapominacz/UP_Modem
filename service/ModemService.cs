using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModemConnect.service {
    interface ModemService {
        void dataReceived(string message);
        void setLastOperationResult(string message);
        void tryToConnectModem(string port);
        void disconnect();
        void dialWith(string number);
        bool isServerRunning();
        void stopServer();
        void startServer();
        string[] getPorts();
        void executeCommand(string text);
    }
}
