using System;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Text;

namespace Client
{
    class Client
    {
        TcpClient tcpClient;

        string userName;
        bool isConnected = false;

        public void connectClient()
        {
            //memasukkan username client
            Console.WriteLine("Masukkan Username Anda");
            userName = Console.ReadLine();

            try
            {
                //menginisialisasi dan mengkoneksikan client ke  server up dan port yg sesuai
                tcpClient = new TcpClient("127.0.0.1", 5000);

                isConnected = true;

                //thread untuk mengatur saat client mengirim chat
                Thread threadSend = new Thread(clientSend);
                threadSend.Start();
                
                //thread untuk membaca data yg dikirim oleh server
                Thread threadRead = new Thread(clientRead);
                threadRead.Start();
            }                
            catch(Exception e)
            {
                Console.WriteLine("Gagal Tersambung");
            }
                
        }
        public void clientSend()
        {
            //untuk mengirim chat yg di input client
            StreamWriter sWriter = new StreamWriter(tcpClient.GetStream());

            while (true)
            {
                if (tcpClient.Connected)
                {
                    //client menginput data
                    string input = Console.ReadLine();
                    string message = userName + " : " + input;

                    //sebelum mengirim chat, clienr akan mengecek apakah masih terhubung dengan server atau tidak
                    if(isConnected)
                    {
                        //client mengirim data ke server (chat yg di input)
                        sWriter.Write(message);
                        sWriter.Flush();
                    }
                    else
                    {
                        Console.WriteLine("Kamu Terpututs Dari Server");
                        Console.ReadLine();
                        break;
                    }
                }
            }
        }

        public void clientRead()
        {

            NetworkStream net = tcpClient.GetStream();
            Byte[] bytes = new Byte[1024];
            int i;

            try
            {
                //menunggu kiriman data dari server
                while ((i = net.Read(bytes, 0, bytes.Length)) != 0)
                {
                    //mengubah byte ke string
                    string data = Encoding.ASCII.GetString(bytes, 0, i);

                    Console.WriteLine(data);
                    
                }
            }
            catch (Exception e)
            {

            }

            isConnected = false;
            //client akan menutup koneksi (hubungan) saat server berhenti
            tcpClient.Close();
        }

        
    }

}
