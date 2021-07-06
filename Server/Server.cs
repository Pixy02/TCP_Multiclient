using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Text;

namespace Server
{
    class Server
    {
        public  TcpListener tcpListener;
        public  List<TcpClient> tcpClientsList = new List<TcpClient>(); 
        private List<string> listMessageSave = new List<string>();  
        public void startServer()
        {
            //server membuat socket listener dengan IP dan port seperti berikut
            // lalu socket listener tersebut di start
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);
            tcpListener.Start();
            Console.WriteLine("Server Sudah Dibuat");

            //server membuat thread yg berguna untuk menunggu dan meng acc client
            Thread accClient = new Thread(clientAcc);
            accClient.Start();
        }

        public void clientAcc()
        {
            try
            {
                while (true)
                {
                    //listener akan menuggnu client yg terhubung, menggunakan fungsi AcceptTcpClient
                    //jika ada yg terhubung, akan disimpan di objek tcpClient
                    //client tersebut akan disimpan di list untuk client yg terhubung (tcpClientsList)
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    tcpClientsList.Add(tcpClient);
                    Console.WriteLine("Client Sudah Terhubung");

                    //server membuat thread baru untuk mengatur client yg terhubung tersebut
                    Thread handleClient = new Thread(new ParameterizedThreadStart(clientHandle));
                    handleClient.Start(tcpClient);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Server Berhenti");
            }
            
        }

        public void clientHandle(Object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream net = client.GetStream();

            //bytes berguna untuk tempat mengambil data dari clint
            Byte[] bytes = new Byte[1024];
            int i;

            try
            {
                // thread clienthandle akan membaca data yg dikirim dari client
                while ((i = net.Read(bytes, 0, bytes.Length)) != 0)
                {
                    //mengubah data yg awalnya byte menjadi string
                    string data = Encoding.ASCII.GetString(bytes, 0, i);

                    Console.WriteLine(data);

                    //menyimpan data chat (history)
                    listMessageSave.Add(data);
                    File.WriteAllLines(@"D:\!!!!!TUGAS TUGAS KULIAH\SEMS 4\Teori Arsitektur Jaringan dan Komputer\FP\Tugas_Final\history.txt", listMessageSave);

                    //server akan mem broadcast data yg diterima ke seluruh client
                    broadcast(client, data);
                }
            }
            catch (Exception e)
            {

            }
            //saat client terputus, akan di remove dari list client yang terhubung di server
            tcpClientsList.Remove(client);
        }

        public void broadcast(TcpClient clientSender, string msg)
        {
            //server akan mengirimkan data chat ke seluruh client, kecuali pengirim 
            foreach (TcpClient client in tcpClientsList)
            {               
                StreamWriter sWriter = new StreamWriter(client.GetStream());

                if(client != clientSender)
                {
                   sWriter.Write(msg);
                }
                sWriter.Flush();
            }
        }

    }

}