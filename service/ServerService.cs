using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModemConnect.service {
    interface ServerService {
        string readFileToString(string v);
        bool isRunning();
        void start();

        void stop();
    }
}
