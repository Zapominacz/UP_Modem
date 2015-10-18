using System;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ModemConnect {
    public partial class MainWindowViewImpl : Form {

        public delegate void AddDataDelegate(String myString);
        public AddDataDelegate myDelegate;
        public delegate void ReplaceDataDelegate(String myString);
        public ReplaceDataDelegate replaceDelegate;

        private SerialPort serialPort;

        public MainWindowViewImpl() {
            InitializeComponent();
            setupSerialPort();
            myDelegate = new AddDataDelegate(AddDataMethod);
            replaceDelegate = new ReplaceDataDelegate(ReplaceDataMethod);
            listPorts();
        }

        private void listPorts() {
            portListBox.Items.Clear();
            foreach (string item in SerialPort.GetPortNames()) {
                portListBox.Items.Add(item);
            }
        }

        private void onPortConnectPressed(object sender, EventArgs e) {
            try {
                serialPort.PortName = (string)portListBox.SelectedItem;
                serialPort.Open();
                serialPort.DataReceived += SerialPort_DataReceived;
                serialPort.ErrorReceived += SerialPort_ErrorReceived;
            } catch (Exception exception) {
                AddDataMethod(exception.Message);
            }
        }

        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e) {
            SerialPort port = (SerialPort) sender;
            string msg = port.ReadExisting();
            sendToHistoryAsync("error:" + msg + serialPort.NewLine);
        }

        private string buffer;

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            SerialPort port = (SerialPort)sender;
            buffer += port.ReadExisting();
            while (buffer.IndexOf(port.NewLine) > -1) {
                int newline = buffer.IndexOf(port.NewLine);
                string line = buffer.Substring(0, newline);
                buffer = buffer.Remove(0, newline + 1);
                procesLine(line);
            }
        }

        private bool ftMode = false;
        bool dataStarted = false;
        private FileStream stream = null;
        private long bytes = 0;

        private void procesLine(string line) {
            string command = line.ToUpper();
            if (command.Equals("__FT_END__")) {
                sendToHistoryAsync("POBRANO!");
                ftMode = false;
                stream.Close();
                stream = null;
                dataStarted = false;
            } else if (ftMode && dataStarted) {
                byte[] plainBytes = System.Convert.FromBase64String(line);
                stream.Write(plainBytes, 0, plainBytes.Length);
                bytes += plainBytes.LongLength;
                commandHistoryTextBox.Invoke(replaceDelegate, "POBRANO: " + (bytes / 1024) + "kB");
            } else if (command.Equals("__FT_START__")) {
                sendToHistoryAsync("ODBIERAM PLIK...");
                ftMode = true;
                dataStarted = false;
            } else if (command.Equals("__FT_DATA__")) {
                ftMode = true;
                bytes = 0;
                dataStarted = true;
            } else if (command.StartsWith("_FF")) {
                line = line.Remove(0, 3);
                sendFile(line);
            } else if (ftMode && !dataStarted) {
                if (command.StartsWith("NAME_")) {
                    string filename = line.Substring(5);
                    stream = new FileStream(line.Substring(5), FileMode.Create);
                    sendToHistoryAsync("nazwa: " + filename);
                } else if (command.StartsWith("SIZE_")) {
                    sendToHistoryAsync("wielkosc: " + line.Substring(5) + "kB");
                }
            } else {
                sendToHistoryAsync(line);
            }
        }

        private void getFile(string msg) {
            if (msg.Contains("__FT_DATA__")) {
                dataStarted = true;
            }
            if (dataStarted) {

            } else {

            }
        }

        private void sendToHistoryAsync(string message) {
            commandHistoryTextBox.Invoke(myDelegate, message + Environment.NewLine);
        }

        private void onSendCommandButtonPressed(object sender, EventArgs e) {
            sendCommand();
        }

        private void serverButtonClicked(object sender, EventArgs e) {
        }

        public void AddDataMethod(string myString) {
            commandHistoryTextBox.AppendText(myString + Environment.NewLine);
        }

        public void ReplaceDataMethod(string myString) {
            string text = commandHistoryTextBox.Text;
            text = text.Substring(0, text.LastIndexOf(Environment.NewLine) + 1);
            commandHistoryTextBox.Text = text;
            commandHistoryTextBox.AppendText(myString);
        }

        private void setupSerialPort() {
            serialPort = new SerialPort();
            serialPort.BaudRate = 9600;
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;
            serialPort.DataBits = 8;
            serialPort.Handshake = Handshake.XOnXOff;
            serialPort.Encoding = Encoding.ASCII;
            serialPort.NewLine = "\r";
        }

        private void commandLineTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (Keys.Return.Equals(e.KeyChar)) {
                sendCommand();
            }
        }

        private void sendCommand() {
            try {
                String command = commandLineTextBox.Text;
                serialPort.Write(command + serialPort.NewLine);
                commandHistoryTextBox.Text += ">" + commandLineTextBox.Text + Environment.NewLine;
                commandLineTextBox.Clear();
            } catch (Exception e) {
                commandHistoryTextBox.AppendText(Environment.NewLine + e.Message + Environment.NewLine);
            }
        }

        public void sendFile(string path) {
            byte[] plainTextBytes = File.ReadAllBytes(path);
            string filename = path.Substring(path.LastIndexOf('/') + 1);
            string data = System.Convert.ToBase64String(plainTextBytes);
            serialPort.Write("__FT_START__" + serialPort.NewLine);
            serialPort.Write("NAME_" + filename + serialPort.NewLine);
            serialPort.Write("SIZE_" + (plainTextBytes.LongLength / 1024) + serialPort.NewLine);
            serialPort.Write("__FT_DATA__" + serialPort.NewLine);
            int start = 0;
            int end = data.Length;
            int bufferSize = 512;
            while (start + bufferSize < end) {
                serialPort.Write(data.Substring(start, bufferSize) + serialPort.NewLine);
                start += bufferSize;
                Thread.Sleep(50);
            }
            serialPort.Write(data.Substring(start) + serialPort.NewLine);
            serialPort.Write("__FT_END__" + serialPort.NewLine);
        }
    }
}
