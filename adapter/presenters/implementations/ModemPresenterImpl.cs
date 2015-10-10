using System;
using ModemConnect.service;
using ModemConnect.service.implementations;
using ModemConnect.view;

namespace ModemConnect.adapter.presenters.implementations {
    class ModemPresenterImpl : ModemPresenter {

        private MainWindowView mainWindowView;
        private ModemService modemService;

        public ModemPresenterImpl(MainWindowView mainWindowView) {
            this.mainWindowView = mainWindowView;
            modemService = new ModemServiceImpl(this);
        }

        public void onCommandTyped(string text) {
            mainWindowView.showInCommandHistory(text);
            modemService.executeCommand(text);
        }

        public void onPortSelected(string port) {
            if (port != null && port.Contains("COM")) {
                modemService.tryToConnectModem(port);
            } else {
                mainWindowView.showInCommandHistory("ZŁY PORT!");
            }
        }

        public void onViewCreated() {
            mainWindowView.listAvailablePorts(modemService.getPorts());
        }

        public void onDestroyView() {
            modemService.disconnect();
        }

        public void onDataReceived(string data) {
            mainWindowView.showInCommandHistory(getTimestamp() + data);
        }

        public void onError(string error) {
            mainWindowView.showInCommandHistory(getTimestamp() + "ERROR: " + error);
        }

        private String getTimestamp() {
            return DateTime.Now.ToString("hh:mm:ss:  ");
        }


        public void onServerStatusChanged() {
            if (modemService.isServerRunning()) {
                modemService.stopServer();
                mainWindowView.setServerButtonText("Serwer wyłączony");
            } else {
                modemService.startServer();
                mainWindowView.setServerButtonText("Serwer włączony");
            }
        }

        public void onDial(string number) {
            modemService.dialWith(number);
        }
    }
}
