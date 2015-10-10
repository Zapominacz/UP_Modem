using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModemConnect.adapter.presenters {
    interface ModemPresenter : Presenter {

        void onCommandTyped(String command);
        void onPortSelected(String port);
        void onDataReceived(String data);
        void onError(String error);
        void onServerStatusChanged();
        void onDial(string number);
    }
}
