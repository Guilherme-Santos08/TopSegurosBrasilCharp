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
    public partial class Sinistro : Form
    {
        public Sinistro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Conectando, aguarde";
            statusStrip1.Refresh();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conn.StrCon))
                {
                    cn.Open();

                    var sqlQuery = "SELECT * FROM Sinistro Where VeiculoId = " + txtBuscar.Text + "";
                    using (SqlDataAdapter da = new SqlDataAdapter(sqlQuery, cn))
                    {
                        using(DataTable dt = new DataTable())
                        {
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                    toolStripStatusLabel1.Text = "Pronto!";
                    statusStrip1.Refresh();
                }
            } catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Falha!";
                statusStrip1.Refresh();

                MessageBox.Show("Falha ao Tentar conectar \n\n" + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "";
        }
    }
}
