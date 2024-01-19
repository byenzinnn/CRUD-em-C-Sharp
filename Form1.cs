using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Crud
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection sqlCon = null;
        private string strCon = @"Password=@admin123;Persist Security Info=True;User ID=sa;Initial Catalog=MeuCRUD;Data Source=JLE207P060841";
        private string strSql = string.Empty;


        //Botão de adicionar - Novo -
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tsbNovo.Enabled = false;
            tsbSalvar.Enabled = true;
            tsbAlterar.Enabled = false;
            tsbCancelar.Enabled = true;
            tsbDeletar.Enabled = false;
            tsbBuscar.Enabled = false;
            tstIdBuscar.Enabled = false;
            txtId.Enabled = true;
            txtEndereco.Enabled = true;
            txtCidade.Enabled = true;
            txtBairro.Enabled = true;
            txtUF.Enabled = true;
            txtNome.Enabled = true;
            mskCep.Enabled = true;
            mskTelefone.Enabled = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbAlterar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbDeletar.Enabled = false;
            tsbBuscar.Enabled = true;
            tstIdBuscar.Enabled = true;
            txtId.Enabled = false;
            txtEndereco.Enabled = false;
            txtCidade.Enabled = false;
            txtBairro.Enabled = false;
            txtUF.Enabled = false;
            txtNome.Enabled = false;
            mskCep.Enabled = false;
            mskTelefone.Enabled = false;
            
        }


        //Botão de salvar
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            strSql = "insert into funcionarios (Id, Nome, Endereco, CEP, Bairro, Cidade, UF, Telefone) values (@Id, @Nome, @Endereco, @CEP, @Bairro, @Cidade, @UF, @Telefone)";
            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            comando.Parameters.Add("@Id", SqlDbType.Int).Value = txtId.Text;
            comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txtNome.Text;
            comando.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = txtEndereco.Text;
            comando.Parameters.Add("@CEP", SqlDbType.VarChar).Value = mskCep.Text;
            comando.Parameters.Add("@Bairro", SqlDbType.VarChar).Value = txtBairro.Text;
            comando.Parameters.Add("@Cidade", SqlDbType.VarChar).Value = txtCidade.Text;
            comando.Parameters.Add("@UF", SqlDbType.VarChar).Value = txtUF.Text;
            comando.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = mskTelefone.Text;

            try
            {
                sqlCon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro realizado com sucesso");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }

            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbAlterar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbDeletar.Enabled = false;
            tsbBuscar.Enabled = true;
            tstIdBuscar.Enabled = true;
            txtId.Enabled = true;
            txtEndereco.Enabled = true;
            txtCidade.Enabled = true;
            txtBairro.Enabled = true;
            txtUF.Enabled = true;
            txtNome.Enabled = true;
            mskCep.Enabled = true;
            mskTelefone.Enabled = true;
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtEndereco.Text = "";
            txtId.Text = "";
            txtNome.Text = "";
            txtUF.Text = "";
            mskCep.Text = "";
            mskTelefone.Text = "";

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tsbCancelar_Click(object sender, EventArgs e)
        {
            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbAlterar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbDeletar.Enabled = false;
            tsbBuscar.Enabled = true;
            tstIdBuscar.Enabled = true;
            txtId.Enabled = true;
            txtEndereco.Enabled = true;
            txtCidade.Enabled = true;
            txtBairro.Enabled = true;
            txtUF.Enabled = true;
            txtNome.Enabled = true;
            mskCep.Enabled = true;
            mskTelefone.Enabled = true;
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtEndereco.Text = "";
            txtId.Text = "";
            txtNome.Text = "";
            txtUF.Text = "";
            mskCep.Text = "";
            mskTelefone.Text = "";
        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            strSql = "select * from Funcionarios where Id = @Id";
            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);
            
            comando.Parameters.Add("@Id", SqlDbType.Int).Value = tstIdBuscar.Text;

            try
            {
                if(tstIdBuscar.Text == string.Empty)
                {
                    throw new Exception("Você precisa digitar um id!");
                }
                sqlCon.Open();
                SqlDataReader dr = comando.ExecuteReader();

                dr.Read();
                
                if(dr.HasRows == false)
                {
                    throw new Exception("Id não cadastrado!");
                }

                txtId.Text = Convert.ToString(dr["Id"]);
                txtNome.Text = Convert.ToString(dr["Nome"]);
                txtEndereco.Text = Convert.ToString(dr["Endereco"]);
                mskCep.Text = Convert.ToString(dr["CEP"]);
                txtBairro.Text = Convert.ToString(dr["Bairro"]);
                txtCidade.Text = Convert.ToString(dr["Cidade"]);
                txtUF.Text = Convert.ToString(dr["UF"]);
                mskTelefone.Text = Convert.ToString(dr["Telefone"]);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
            tsbNovo.Enabled = false;
            tsbSalvar.Enabled = false;
            tsbAlterar.Enabled = true;
            tsbCancelar.Enabled = true;
            tsbDeletar.Enabled = true;
            tsbBuscar.Enabled = true;
            tstIdBuscar.Enabled = true;
            txtId.Enabled = true;
            txtEndereco.Enabled = true;
            txtCidade.Enabled = true;
            txtBairro.Enabled = true;
            txtUF.Enabled = true;
            txtNome.Enabled = true;
            mskCep.Enabled = true;
            mskTelefone.Enabled = true;
            tsbBuscar.Text = "";
            txtNome.Focus();
        }

        private void tsbAlterar_Click(object sender, EventArgs e)
        {
            strSql = "update Funcionarios set Id = @Id, Nome = @Nome, Endereco = @Endereco, CEP = @CEP, Bairro = @Bairro, Cidade = @Cidade, UF = @UF, Telefone = @Telefone where Id = @IdBuscar";
            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            comando.Parameters.Add("@IdBuscar", SqlDbType.Int).Value = tstIdBuscar.Text;

            comando.Parameters.Add("@Id", SqlDbType.Int).Value = txtId.Text;
            comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txtNome.Text;
            comando.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = txtEndereco.Text;
            comando.Parameters.Add("@CEP", SqlDbType.VarChar).Value = mskCep.Text;
            comando.Parameters.Add("@Bairro", SqlDbType.VarChar).Value = txtBairro.Text;
            comando.Parameters.Add("@Cidade", SqlDbType.VarChar).Value = txtCidade.Text;
            comando.Parameters.Add("@UF", SqlDbType.VarChar).Value = txtUF.Text;
            comando.Parameters.Add("@Telefone", SqlDbType.VarChar).Value = mskTelefone.Text;

            try
            {
                sqlCon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro atualizado com sucesso!");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }

            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbAlterar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbDeletar.Enabled = false;
            tsbBuscar.Enabled = true;
            tstIdBuscar.Enabled = true;
            txtId.Enabled = true;
            txtEndereco.Enabled = true;
            txtCidade.Enabled = true;
            txtBairro.Enabled = true;
            txtUF.Enabled = true;
            txtNome.Enabled = true;
            mskCep.Enabled = true;
            mskTelefone.Enabled = true;
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtEndereco.Text = "";
            txtId.Text = "";
            txtNome.Text = "";
            txtUF.Text = "";
            mskCep.Text = "";
            mskTelefone.Text = "";
        }

        private void tsbDeletar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Deseja realmente deletar este funcionario?", "Cuidado", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                MessageBox.Show("Operação cancelada!");
            }
            else
            {
                strSql = "delete from Funcionarios where Id = @Id";
                sqlCon = new SqlConnection(strCon);
                SqlCommand comando = new SqlCommand(strSql, sqlCon);

                comando.Parameters.Add("@Id", SqlDbType.Int).Value = tstIdBuscar.Text;

                try
                {
                    sqlCon.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Funcionario deletado com sucesso!");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlCon.Close();
                }
            }
        }

        private void tsbLimpar_Click(object sender, EventArgs e)
        {
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtEndereco.Text = "";
            txtId.Text = "";
            txtNome.Text = "";
            txtUF.Text = "";
            mskCep.Text = "";
            mskTelefone.Text = "";
        }
    }
}
