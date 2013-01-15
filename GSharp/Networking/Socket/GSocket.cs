using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace GSharp.Networking.Socket {
    public class GSocket {
        public string Name = "";

        private bool _isActive = false;

        public bool isActive {
            get { return _isActive; }
        }

        private TcpClient _tcpClient;

        public TcpClient TCP_Client {
            get { return _tcpClient; }
        }

        private Thread _readerThread;

        public delegate void DataArrivedHandler(GSocket sender, byte[] Data);
        public event DataArrivedHandler DataArrived;

        public delegate void ClosedHandler(GSocket sender);
        public event ClosedHandler Closed;

        public int MaxPackSize;

        public GSocket(TcpClient tcpclient, int maxPackSize = 2048) {
            if (!tcpclient.Connected) throw new Exception("Must be connected");
            _tcpClient = tcpclient;
            _isActive = true;
            MaxPackSize = maxPackSize;

            _readerThread = new System.Threading.Thread(new System.Threading.ThreadStart(_readerloop));
            _readerThread.Start();
        }

        public void Kill() {
            if (_isActive == false) return;
            _isActive = false;
            if (_tcpClient.Connected) _tcpClient.Close();
            try {
                if (_readerThread.IsAlive) _readerThread.Abort();
            } catch (Exception ex) { } finally {
                if (Closed != null) Closed(this);
            }
        }

        public int Send(string data) {
            return this.Send(new System.Text.ASCIIEncoding().GetBytes(data));
        }

        public int Send(byte[] data) {
            NetworkStream networkStream = _tcpClient.GetStream();

            networkStream.Write(data, 0, data.Length);
            networkStream.Flush();
            return data.Length;
        }

        #region Loops
        private void _readerloop() {
            NetworkStream networkStream = _tcpClient.GetStream();
            byte[] data = new byte[MaxPackSize];
            while (_isActive) {
                try {
                    int size = networkStream.Read(data, 0, MaxPackSize);
                    if (size == 0) {
                        this.Kill();
                        //Thread.Sleep(10);
                        //continue;
                    }
                    byte[] data1 = new byte[size];

                    Array.Copy(data, data1, size);


                    if (DataArrived != null) DataArrived(this, data1);
                } catch (System.IO.IOException ex) {
                    //Console.WriteLine("ex0: " + ex.Message);
                    this.Kill();
                } catch (Exception ex) {
                    //Console.WriteLine("ex1: " + ex.Message);
                }
            }
        }
        #endregion
    }
}
