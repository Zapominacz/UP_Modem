using System;
using System.Windows.Forms;

namespace ModemConnect {
    public partial class MainWindowView : Form {

        private ModemPresenter presenter;

        public MainWindowView() {
            InitializeComponent();
        }

        private void onPortConnectPressed(object sender, EventArgs e) {
            presenter.onPortSelected((String) portListBox.SelectedItem);
        }

        private void onSendCommandButtonPressed(object sender, EventArgs e) {
            String command = commandLineTextBox.Text;
            presenter.onCommandTyped(command);
            commandHistoryTextBox.Text += (command + '\n');
            commandLineTextBox.Clear();
        }
    }
}
