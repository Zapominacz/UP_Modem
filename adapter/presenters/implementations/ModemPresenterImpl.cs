using System;
using ModemConnect.service;
using ModemConnect.service.implementations;
using ModemConnect.view;

namespace ModemConnect.adapter.presenters.implementations {
    class ModemPresenterImpl : ModemPresenter {

        private MainWindowView mainWindowView;
        private ModemService modemService;
        private ServerService serverService;

        public ModemPresenterImpl(MainWindowView mainWindowView) {
            this.mainWindowView = mainWindowView;
            modemService = new ModemServiceImpl(this);
            serverService = new ServerControlServiceImpl(this);
        }

        public void onCommandTyped(string text) {
            mainWindowView.showInCommandHistory(text);
        }

        public void onPortSelected(string port) {
            modemService.tryToConnectModem(port);
        }

        public void onViewCreated() {

        }

        public void onDestroyView() {
            modemService.disconnect();
            serverService.stop();
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
            if (serverService.isRunning()) {
                serverService.stop();
                mainWindowView.setServerButtonText("Serwer wyłączony");
            } else {
                serverService.start();
                mainWindowView.setServerButtonText("Serwer włączony");
            }
        }
    }
}
