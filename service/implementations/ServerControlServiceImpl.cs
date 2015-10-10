using System;
using System.IO;
using ModemConnect.adapter.presenters;
using ModemConnect.adapter.presenters.implementations;

namespace ModemConnect.service.implementations {
    class ServerControlServiceImpl : ServerService {

        private ModemService modemService;
        private bool running;

        public ServerControlServiceImpl(ModemService modemService) {
            this.modemService = modemService;
        }

        public bool isRunning() {
            return running;
        }

        public string readFileToString(string path) {
            String result = null;
            try {
                using (StreamReader sr = new StreamReader("TestFile.txt")) {
                    result = sr.ReadToEnd();
                }
            } catch (Exception e) {
                result = "The file could not be read:" + e.Message;
            }
            return result;
        }

        public void start() {
            running = true;
        }

        public void stop() {
            running = false;
        }
    }
}
