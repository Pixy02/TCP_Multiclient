using System;
using System.Collections.Generic;
using System.Text;


namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //membuat objek server baru, dan menjalankan fungsi startserver untuk memulai server 
            Server server = new Server();
            server.startServer();
        }
    }

}
