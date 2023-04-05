using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR_C
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi Ke DataBase\n");
                    Console.Write("Masukkan User ID : ");
                    string user = Console.ReadLine();
                    Console.Write("Masukkan Password : ");
                    string pass = Console.ReadLine();
                    Console.Write("Masukkan DataBase tujuan : ");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk koneksi ke DataBase : ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'k':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source = PRIBOEN\\PRIBOEN; " +
                                    "initial catalog = {0}; " +
                                    "User ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.Clear();
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat data Produk");
                                        Console.WriteLine("2. Tambah data Produk");
                                        Console.WriteLine("3. Hapus data Produk");
                                        Console.WriteLine("4. Keluar");
                                        Console.Write("\nEnter your choice (1-4) : ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Data Produk\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                    conn.Close();
                                                    Console.WriteLine("Tekan enter untuk melanjutkan.");
                                                    Console.ReadLine();
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Input Data Produk\n");
                                                    Console.Write("Masukkan ID Produk : ");
                                                    string id_produk = Console.ReadLine();
                                                    Console.Write("Masukkan Nama Produk : ");
                                                    string nama_produk = Console.ReadLine();
                                                    Console.Write("Masukkan Deskripsi Produk : ");
                                                    string tentang_produk = Console.ReadLine();
                                                    Console.Write("Masukkan Cara pakai Produk : ");
                                                    string cara_pakai = Console.ReadLine();
                                                    Console.Write("Masukkan Harga Produk : ");
                                                    string harga_produk = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(id_produk, nama_produk, tentang_produk, cara_pakai, harga_produk, conn);
                                                        conn.Close();
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki akses untuk menambah data");
                                                        Console.WriteLine("Tekan enter untuk melanjutkan.");
                                                        Console.ReadLine();
                                                    }
                                                }
                                                break;
                                            case '3':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Hapus Data Produk\n");
                                                    Console.Write("Masukan Nama Produk : ");
                                                    string nama_produk = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.delete(nama_produk, conn);
                                                        conn.Close();
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki akses untuk menambah data");
                                                        Console.WriteLine("Tekan enter untuk melanjutkan.");
                                                        Console.ReadLine();
                                                    }

                                                }
                                                break;
                                            case '4':
                                                {
                                                    conn.Close();
                                                    return;
                                                }
                                                default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid option");
                                                    Console.WriteLine("Tekan enter untuk melanjutkan.");
                                                    Console.ReadLine();
                                                }
                                                break;
                                        }

                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }
                            }
                            default:
                            {

                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak dapat mengakses database menggunakan user tersebut\n");
                    Console.ResetColor();
                }
            }

        }

        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select*From dbo.Produk", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
        }

        public void insert(string id_produk, string nama_produk, string tentang_produk, string cara_pakai, string harga_produk, SqlConnection con)
        {
            string str = "";
            str = "insert into dbo.Produk (id_produk, nama_produk, tentang_produk, cara_pakai, harga_produk)" + "values(@idProduk, @namaProduk, @tentangProduk, @caraPakai, @hargaProduk)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("idProduk", id_produk));
            cmd.Parameters.Add(new SqlParameter("namaProduk", nama_produk));
            cmd.Parameters.Add(new SqlParameter("tentangProduk", tentang_produk));
            cmd.Parameters.Add(new SqlParameter("caraPakai", cara_pakai));
            cmd.Parameters.Add(new SqlParameter("hargaProduk", harga_produk));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }

        public void delete(string nama_produk, SqlConnection con)
        {
            string str = "";
            str = "delete from dbo.Produk where nama_produk " + " = '" + nama_produk + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Sudah berhasil dihapus.");
        }
    }
}
