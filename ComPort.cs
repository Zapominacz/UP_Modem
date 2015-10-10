using System;
using System.IO.Ports;

namespace ModemConnect.Repository.Ports {
    class ComPort {

        private SerialPort comPort;
        private String currentPort;
        private String buffer = "";

        public delegate void PortDataReceivedListener(object sender, EventArgs e);
        public event PortDataReceivedListener changedListener;

        public ComPort() {
            comPort = new SerialPort();
        }

        public String[] getAvailablePorts() {
            String[] result = SerialPort.GetPortNames();
            return result;
        }

        public void openPort(String port) {
            comPort.PortName = port;
            comPort.DataReceived += new SerialDataReceivedEventHandler(DataReceviedHandler);
            comPort.Open();
        }

        public void closePort(String port) {
            comPort.DataReceived -= DataReceviedHandler;
            comPort.Close();
        }

        public void setListener(PortDataReceivedListener listener) {
            this.changedListener = listener;
        }

        private void DataReceviedHandler(object sender, SerialDataReceivedEventArgs e) {
            buffer += comPort.ReadExisting();
            int index = buffer.IndexOf('\n') + 1;
            if (changedListener != null && index > 0) {
                String data = buffer.Substring(0, index);
                changedListener(this, new PortEvent(data));
            }
        }

        public void sendMessage(String result) {
            comPort.WriteLine(result);
        }
    }

    class PortEvent : EventArgs {

        private String eventMessage;

        public String getEventMessage() {
            return eventMessage;
        }

        public PortEvent(String msg) {
            this.eventMessage = msg;
        }
    }
}
