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
        public FrmSinistroAdicionar()
        {
            InitializeComponent();
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

					var sql = "INSERT INTO Sinistro (DataSinistro, DescSinistro, VeiculoId) VALUES  (@DataSinistro, @DescSinistro, @VeiculoId)";

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
