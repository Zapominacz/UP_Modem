using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModemConnect.adapter.presenters.implementations;
using ModemConnect.service;

namespace ModemConnect.service.implementations {
    class ServerControlServiceImpl : ServerService {
        private ModemPresenterImpl modemPresenterImpl;

        public ServerControlServiceImpl(ModemPresenterImpl modemPresenterImpl) {
            this.modemPresenterImpl = modemPresenterImpl;
        }

        public string readFileToString(string v) {
            throw new NotImplementedException();
        }
    }
}
