using System;
using System.Collections.Generic;
using System.Text;


namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //membuat objek client baru, dan menjalankan fungsi conncetclient untuk menyambungkan client 
            Client client = new Client();
            client.connectClient();
        }
    }
}
