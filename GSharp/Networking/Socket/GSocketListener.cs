using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace GSharp.Networking.Socket {
    public class GSocketListener {
        private int _port;

        public int Port {
            get { return _port; }
        }

        private TcpListener _tcpListener;

        public TcpListener TCP_Listener {
            get { return _tcpListener; }
        }

        private Thread _acceptorThread;

        public delegate void ClientArrivedHandler(GSocketListener sender, GSocket Client);
        public event ClientArrivedHandler ClientArrived;

        public GSocketListener(IPAddress address, int port, GSocketListener.ClientArrivedHandler handler) {
            _port = port;

            ClientArrived += handler;

            _tcpListener = new TcpListener(address, _port);
            _tcpListener.Start();

            _acceptorThread = new Thread(new ThreadStart(_acceptorloop));
            _acceptorThread.Start();
        }

        public GSocketListener(int port, GSocketListener.ClientArrivedHandler handler) {
            _port = port;

            ClientArrived += handler;

            _tcpListener = new TcpListener(_port);
            _tcpListener.Start();

            _acceptorThread = new Thread(new ThreadStart(_acceptorloop));
            _acceptorThread.Start();
        }

        public void Kill() {
            _acceptorThread.Abort();
            _tcpListener.Stop();
        }

        private void _acceptorloop() {
            while (true) {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = _tcpListener.AcceptTcpClient();
                GSocket gs = new GSocket(clientSocket);
                if (ClientArrived != null) ClientArrived(this, gs);
            }
        }
    }
}
