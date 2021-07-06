# TCP_Multiclient
TCP Multi Client adalah program chat sederhana yang bisa menghubungkan beberapa pengguna (client).
Pada program ini, untuk berkomunikasi antar server dan client menggunakan protokol tipe TCP.

# Installation
1. Clone Repository
2. Pada Source Code "Server" [This File](/Server.cs), ubah lokasi penyimpanan yang awalnya :
```
"D:\!!!!!TUGAS TUGAS KULIAH\SEMS 4\Teori Arsitektur Jaringan dan Komputer\FP\Tugas_Final\history.txt"
```
ke lokasi .txt sudah anda tentukan.
3. Jalankan/Run Server terlebh dahulu , kemudian jalankan/Run Client (bisa lebh dari 1 "multiple")
4. Client diharuskan mengisi username terlebih dahulu, agar dapat terhubunng ke Server

# Preview
![Preview](https://user-images.githubusercontent.com/72332713/124576994-11709f00-de77-11eb-92fc-ce34ac3c8cee.png)

Penjelasan
1. Setelah mengisi username, client akan terhubung ke server, dan pada server terdapat Ppenjelasan "Client sudah terhubung"
2. Saat salah satu client menngirim chat, chat tersebut akan diterima di server, dan di broadcast ke clien lainnya, kecuali si pengirim chat.
3. 

