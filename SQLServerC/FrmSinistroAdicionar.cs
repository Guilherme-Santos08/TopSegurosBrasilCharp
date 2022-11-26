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
            toolStripStatusLabel1.Text = "Conectando...";
            btnSalvar.Visible = true;
            btnExcluir.Visible = false;

            if (this.id > 0)
            {
                GetSinistro(id);
                toolStripStatusLabel1.Text = "Pronto";
            } else
            {
                toolStripStatusLabel1.Text = "Pronto";
            }
        }

        public FrmSinistroAdicionar(int id, bool excluir)
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Conectando...";

            this.id = id;

            if (excluir)
            {
                if (this.id > 0)
                {
                    toolStripStatusLabel1.Text = "Pronto";
                    GetSinistro(id);
                    TravarControles();
                    btnSalvar.Visible = false;
                    btnExcluir.Visible = true;
                }
            }
            else
            {
                this.Close();
            }
        }

        private void TravarControles()
        {
            txtDataSinistro.Enabled = false;
            txtDesc.Enabled = false;
            txtVeiculoId.Enabled = false;
        }

        private void GetSinistro(int id)
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
            SalvarSinistro();
            this.Close();
        }

        private void SalvarSinistro()
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DialogResult resp = MessageBox.Show("Deseja excluir: ", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resp == DialogResult.Yes)
            {
                ExcluirSinistro();
                this.Close();
            }
        }

        private void ExcluirSinistro()
        {
            toolStripStatusLabel1.Text = "Conectando...";
            statusStrip1.Refresh();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conn.StrCon))
                {
                    cn.Open();

                    var sql = "Delete from Sinistro Where Id=" + id;

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        toolStripStatusLabel1.Text = "Excluindo sinistro...";
                        statusStrip1.Refresh();

                        cmd.ExecuteNonQuery();
                    }
                    toolStripStatusLabel1.Text = "Pronto";
                    statusStrip1.Refresh();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Falha...";
                statusStrip1.Refresh();

                MessageBox.Show("Não foi possivel excluir os dados!\n\n" + ex.Message);
            }
        }
    }
}
