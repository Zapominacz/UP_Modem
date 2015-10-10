using System;
using ModemConnect.repository;
using ModemConnect.service;

namespace ModemConnect.adapter {
    class HayesAdapter {

        private const Char NEWLINE = '\r';
        private readonly ComPort port;
        private readonly ModemService service;
        private bool dataMode;

        public HayesAdapter(ModemService service) {
            this.service = service;
            port = new ComPort();
        }

        public void setPort(string port) {
            this.port.openPort(port);
        }

        public void dialWith(String number) {
            if (port.isConnected()) {
                port.sendMessage("ATDT" + number + NEWLINE);
                dataMode = true;
            } else {
                service.dataReceived("Port jest zamknięty!");
            }
        }

        public void changeDataMode(bool commandMode) {
            if (commandMode) {
                dataMode = false;
                port.sendMessage("+++" + NEWLINE);
            } else {
                dataMode = true;
                port.sendMessage("ATO" + NEWLINE);
            }
        }

        public void closePort() {
            port.closePort();
        }

        public void hangUp() {
            port.sendMessage("ATH" + NEWLINE);
        }

        public void PortDataReceivedListener(object sender, PortEvent e) {
            String message = e.getEventMessage();
            if (dataMode) {
                service.dataReceived(message);
            } else {
                service.setLastOperationResult(message);
            }
        }

        public string[] getPorts() {
            return port.getAvailablePorts();
        }

        public bool isConnected() {
            return port.isConnected();
        }

        public void write(string text) {
            port.sendMessage(text + NEWLINE);
        }
    }
}
