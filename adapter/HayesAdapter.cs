using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModemConnect.Repository.Ports;

namespace ModemConnect.adapter {
    class HayesAdapter {

        private ComPort port;
        private readonly ModemServiceImpl service;
        private ServerControlService serverService;
        private bool dataMode;

        public HayesAdapter(ModemServiceImpl service) {
            this.service = service;
            port = new ComPort();
        }

        public void setServerService(ServerControlService service) {
            this.serverService = service;
        }

        public void dialWith(String number) {
            port.sendMessage("ATDT" + number);
        }

        public void PortDataReceivedListener(object sender, PortEvent e) {
            String message = e.getEventMessage();
            if (dataMode) {
                if (serverService != null && message.StartsWith("SC")) {
                    parseCommand(message.Substring(2));
                } else {
                    service.dataReceived(message);
                }
            } else {
                service.setLastOperationResult(message);
            }
        }

        private void parseCommand(String cmd) {
            if (cmd.StartsWith("FF")) {
                String result = serverService.readFileToString(cmd.Substring(2));
                port.sendMessage(result);
            }
        }
    }
}
