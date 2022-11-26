using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLServerC
{
    public partial class FrmSinistroAdicionar : Form
    {
        int id = 0;

        public FrmSinistroAdicionar(int id)
        {
            InitializeComponent();
            this.id = id;

            if (this.id > 0)
            {
                GetVeiculo(id);
            }
        }

        private void GetVeiculo(int id)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Conn.StrCon))
                {
                    cn.Open();

                    var sql = "Select * from Sinistro Where Id=" + id;

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    txtDataSinistro.Text = dr["DataSinistro"].ToString();
                                    txtDesc.Text = dr["DescSinistro"].ToString();
                                    txtVeiculoId.Text = dr["VeiculoId"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel buscar os dados!\n\n" + ex.Message);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            SalvarVeiculo();
            this.Close();
        }

        private void SalvarVeiculo()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Conn.StrCon))
                {
                    cn.Open();

                    var sql = "";

                    if (this.id == 0)
                    {
                        sql = "INSERT INTO Sinistro (DataSinistro, DescSinistro, VeiculoId) VALUES  (@DataSinistro, @DescSinistro, @VeiculoId)";
                    }
                    else
                    {
                        sql = "UPDATE Sinistro Set DataSinistro=@DataSinistro, DescSinistro=@DescSinistro, VeiculoId=@VeiculoId WHERE Id=" + this.id;
                    }

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@DataSinistro", txtDataSinistro.Text);
                        cmd.Parameters.AddWithValue("@VeiculoId", txtVeiculoId.Text);
                        cmd.Parameters.AddWithValue("@DescSinistro", txtDesc.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não inserir os dados!\n\n" + ex.Message);
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
