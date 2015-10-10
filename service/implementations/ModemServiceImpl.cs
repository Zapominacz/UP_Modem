using System;
using ModemConnect.adapter.presenters.implementations;

namespace ModemConnect.service.implementations {
    class ModemServiceImpl : ModemService {
        private ModemPresenterImpl modemPresenterImpl;

        public ModemServiceImpl(ModemPresenterImpl modemPresenterImpl) {
            this.modemPresenterImpl = modemPresenterImpl;
        }

        public void dataReceived(string message) {
            throw new NotImplementedException();
        }

        public void disconnect() {
            throw new NotImplementedException();
        }

        public void setLastOperationResult(string message) {
            throw new NotImplementedException();
        }

        public void tryToConnectModem(string port) {
            throw new NotImplementedException();
        }
    }
}
