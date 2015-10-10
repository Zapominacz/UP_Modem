using System;
using ModemConnect.adapter;
using ModemConnect.adapter.presenters;
using ModemConnect.adapter.presenters.implementations;

namespace ModemConnect.service.implementations {
    class ModemServiceImpl : ModemService {

        private readonly ModemPresenter modemPresenter;
        private readonly ServerService serverService;
        private readonly HayesAdapter hayesAdapter;

        public ModemServiceImpl(ModemPresenter modemPresenterImpl) {
            this.modemPresenter = modemPresenterImpl;
            hayesAdapter = new HayesAdapter(this);
            serverService = new ServerControlServiceImpl(this);
        }

        public void dataReceived(string message) {
            modemPresenter.onDataReceived(message);
        }

        public void disconnect() {
            hayesAdapter.hangUp();
            hayesAdapter.closePort();
            serverService.stop();
        }

        public void setLastOperationResult(string message) {
            modemPresenter.onDataReceived("MODEM: " + message);
        }

        public void tryToConnectModem(string port) {
            hayesAdapter.setPort(port);
        }

        public void dialWith(string number) {
            hayesAdapter.dialWith(number);
        }

        public bool isServerRunning() {
            return serverService.isRunning();
        }

        public void stopServer() {
            serverService.stop();
        }

        public void startServer() {
            serverService.start();
        }

        public string[] getPorts() {
            return hayesAdapter.getPorts();
        }

        public void executeCommand(string text) {
            if (hayesAdapter.isConnected()) {
                hayesAdapter.write(text);
            }
        }
    }
}
