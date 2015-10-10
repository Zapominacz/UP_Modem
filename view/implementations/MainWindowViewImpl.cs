using System;
using System.Windows.Forms;
using ModemConnect.adapter.presenters;
using ModemConnect.adapter.presenters.implementations;
using ModemConnect.view;

namespace ModemConnect {
    public partial class MainWindowViewImpl : Form, MainWindowView {

        private ModemPresenter presenter;

        public MainWindowViewImpl() {
            InitializeComponent();
            presenter = new ModemPresenterImpl(this);
            presenter.onViewCreated();
        }

        private void onPortConnectPressed(object sender, EventArgs e) {
            presenter.onPortSelected((String) portListBox.SelectedItem);
        }

        private void onSendCommandButtonPressed(object sender, EventArgs e) {
            String command = commandLineTextBox.Text;
            presenter.onCommandTyped(command);
            commandLineTextBox.Clear();
        }

        private void commandLineTextBox_TextChanged(object sender, EventArgs e) {

        }

        public void showInCommandHistory(string information) {
            commandHistoryTextBox.Text += ('>' + information + Environment.NewLine);
        }

        public void listAvailablePorts(string[] ports) {
            portListBox.Items.Clear();
            foreach(string item in ports) {
                portListBox.Items.Add(item);
            }
        }

        private void serverButtonClicked(object sender, EventArgs e) {
            presenter.onServerStatusChanged();
        }

        public void setConnectButtonText(string text) {
            connectPortButton.Text = text;
        }

        public void setServerButtonText(string text) {
            serverButton.Text = text;
        }

        private void onDialButtonClicked(object sender, EventArgs e) {
            String number = dialTextBox.Text;
            presenter.onDial(number);
            showInCommandHistory("Dzwonimy do: " + number);
            dialStarted();
        }

        public void dialEnded() {
            dialTextBox.Enabled = true;
            dialButton.Enabled = false;
        }

        public void dialStarted() {
            dialTextBox.Enabled = false;
            dialButton.Enabled = false;
        }
    }
}
