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

        public void insert(string id_produk, string nama_produk, string tentang_produk, string cara_pakai, int harga_produk, SqlConnection con)
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

        public void delete(string id_produk, string nama_produk, string tentang_produk, string cara_pakai, int harga_produk, SqlConnection con)
        {
            string str = "";
            str = "delete from dbo.Produk where id_produk " + " = '" + id_produk + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Sudah berhasil dihapus.");
        }
    }
}
