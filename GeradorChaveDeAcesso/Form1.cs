using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeradorChaveDeAcesso
{
    public partial class GeradorChaveDeAcesso : Form
    {
        public GeradorChaveDeAcesso()
        {
            InitializeComponent();
        }

        private void PreencheEstado(List<string> Estado)
        {
            Estado.Add("Selecione..."); //00
            Estado.Add("AC");//12
            Estado.Add("AL");//27
            Estado.Add("AM");//13
            Estado.Add("AP");//16
            Estado.Add("BA");//29
            Estado.Add("CE");//23
            Estado.Add("DF");//53
            Estado.Add("ES");//32
            Estado.Add("GO");//52
            Estado.Add("MA");//21
            Estado.Add("MG");//31
            Estado.Add("MS");//50
            Estado.Add("MT");//51
            Estado.Add("PA");//15
            Estado.Add("PB");//25
            Estado.Add("PE");//26
            Estado.Add("PI");//22
            Estado.Add("PR");//41
            Estado.Add("RJ");//33
            Estado.Add("RN");//24
            Estado.Add("RO");//11
            Estado.Add("RR");//14
            Estado.Add("RS");//43
            Estado.Add("SC");//42
            Estado.Add("SE");//28
            Estado.Add("SP");//35
            Estado.Add("TO");//17
            //Vinculando com o combobox
            cmbEstado.DataSource = Estado;

        }

        private void PreencheModelo(List<string> Modelo)
        {
            //Null
            Modelo.Add("Selecione...");
     
            //Modelos
            Modelo.Add("55");
            Modelo.Add("57");

            //Vinculando com o combobox
            cmbModelo.DataSource = Modelo;
        }

        private void GeradorChaveDeAcesso_Load(object sender, EventArgs e)
        {
            List<string> Estados = new List<string>();
            List<string> Modelos = new List<string>();

            PreencheEstado(Estados);
            PreencheModelo(Modelos);
        }

        private string validaEstado(string Estado)
        {
            switch (Estado.ToUpper())
            {
                case "RO":
                    return "11";
                //break;
                case "AC":
                    return "12";
                //break;
                case "AM":
                    return "13";
                //break;
                case "RR":
                    return "14";
                //break;
                case "PA":
                    return "15";
                //break;
                case "AP":
                    return "16";
                //break;
                case "TO":
                    return "17";
                //break;
                case "MA":
                    return "21";
                //break;
                case "PI":
                    return "22";
                //break;
                case "CE":
                    return "23";
                //break;
                case "RN":
                    return "24";
                //break;
                case "PB":
                    return "25";
                //break;
                case "PE":
                    return "26";
                //break;
                case "AL":
                    return "27";
                //break;
                case "SE":
                    return "28";
                //break;
                case "BA":
                    return "29";
                //break;
                case "MG":
                    return "31";
                //break;
                case "ES":
                    return "32";
                //break;
                case "RJ":
                    return "33";
                //break;
                case "SP":
                    return "35";
                //break;
                case "PR":
                    return "41";
                //break;
                case "SC":
                    return "42";
                //break;
                case "RS":
                    return "43";
                //break;
                case "MS":
                    return "50";
                //break;
                case "MT":
                    return "51";
                //break;
                case "GO":
                    return "52";
                //break;
                case "DF":
                    return "53";
                //break;
                default:
                    return "0";
                //break;
            }
        }

        private static int CharToValue(char c)
        {
            if (char.IsDigit(c))
                return c - '0';
            char upper = char.ToUpper(c);
            if (upper >= 'A' && upper <= 'Z')
                return upper - 'A' + 10;
            return 0;
        }

        public static string digito(string chave)
        {
            int soma = 0;
            int resto = 0;
            int[] peso = { 4, 3, 2, 9, 8, 7, 6, 5 };
            int digitoRetorno;

            for (int i = 0; i < chave.Length; i++)
            {
                soma += peso[i % 8] * CharToValue(chave[i]);
            }

            resto = soma % 11;
            if (resto == 0 || resto == 1)
            {
                digitoRetorno = 0;
            }
            else
            {
                digitoRetorno = 11 - resto;
            }

            return chave + digitoRetorno.ToString();
        }

        public bool validaComboBox(string Est, string Mod)
        {
            if ((Est.Equals("0")) || (Mod.Equals("Selecione...")))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool validaCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            if (cnpj.Length != 14)
                return false;

            for (int i = 0; i < 12; i++)
            {
                char upper = char.ToUpper(cnpj[i]);
                if (!char.IsDigit(cnpj[i]) && (upper < 'A' || upper > 'Z'))
                    return false;
            }

            for (int i = 12; i < 14; i++)
                if (!char.IsDigit(cnpj[i]))
                    return false;

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += CharToValue(tempCnpj[i]) * multiplicador1[i];

            int resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += CharToValue(tempCnpj[i]) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public bool isVazio(string est, string mod, string ano, string mes, string cnp, string ser, string num)
        {
            if (est.Equals(""))
                return true;
            else if (mod.Equals(""))
                return true;
            else if (ano.Equals(""))
                return true;
            else if (mes.Equals(""))
                return true;
            else if (cnp.Equals(""))
                return true;
            else if (ser.Equals(""))
                return true;
            else if (num.Equals(""))
                return true;
            else
                return false;
        }

        public bool VerificTxt(string uni, int validador)
        {
            if (string.IsNullOrEmpty(uni))
                return false;

            foreach (char c in uni)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            if (validador == 1)
                return uni.Length <= 3;

            if (validador == 2)
                return uni.Length <= 9;

            return true;
        }

        private void BtnGerarChave_Click(object sender, EventArgs e)
        {
            string est, ano, mes, cnp, mod, ser, num, ranTxt, chave, aux;
            Random rdn = new Random();
            int ranInt;
            bool cnpj, vazio, valid, quantRegS, quantRegN;

            //Pegando os valores da tela
            est = validaEstado(cmbEstado.Text);
            mod = cmbModelo.Text;

            string anoRaw = txtAno.Text.Trim();
            string mesRaw = txtMes.Text.Trim();
            if (anoRaw.Length == 2 && anoRaw.All(char.IsDigit))
                anoRaw = "20" + anoRaw;
            mesRaw = mesRaw.PadLeft(2, '0');
            ano = anoRaw.Length == 4 ? anoRaw.Substring(2) : anoRaw;
            mes = mesRaw;

            cnp = txtCNPJ.Text.Trim().ToUpper();
            ser = txtserie.Text;
            num = txtNum.Text;
            ranInt = rdn.Next(0, 1000000000);
            ranTxt = ranInt.ToString().PadLeft(9, '0');

            //Efetuando validações
            vazio = isVazio(est, mod, anoRaw, mesRaw, cnp, ser, num);
            quantRegS = VerificTxt(ser, 1);
            quantRegN = VerificTxt(num, 2);
            valid = validaComboBox(est, mod);
            cnpj = validaCnpj(cnp);
            
            if (!vazio)
            {
                if (valid)
                {
                    if (cnpj)
                    {
                        if ((quantRegS) && (quantRegN))
                        {
                            aux = est + ano + mes + cnp + mod + ser.PadLeft(3, '0') + num.PadLeft(9, '0') + ranTxt;
                            chave = digito(aux);

                            txtChaveAcesso.Text = chave;
                        }
                        else
                        {
                            MessageBox.Show("Campo Número ou Série com valor inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("CNPJ inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Estado ou Modelo está vazio","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Existem campos vazios", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimp_Click(object sender, EventArgs e)
        {
            cmbEstado.SelectedIndex = 0;
            cmbModelo.SelectedIndex = 0;
            txtAno.Text = "";
            txtMes.Text = "";
            txtCNPJ.Text = "";
            txtserie.Text = "";
            txtNum.Text = "";
            txtChaveAcesso.Text = "";
        }

        private void txtAno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtAno_Leave(object sender, EventArgs e)
        {
            string text = txtAno.Text.Trim();
            if (text.Length == 2 && text.All(char.IsDigit))
                txtAno.Text = "20" + text;
        }

        private void txtMes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtMes_Leave(object sender, EventArgs e)
        {
            string text = txtMes.Text.Trim();
            if (text.Length == 1 && char.IsDigit(text[0]))
                txtMes.Text = "0" + text;
        }

        private void BtnInfo_Click(object sender, EventArgs e)
        {
            string mensagem;

            mensagem = "Estado: Selecione ou digite o estado do emitente\n\n" +
                        "Ano: Informe o ano com 2 ou 4 dígitos (ex: 26 ou 2026)\n\n" +
                        "Mês: Informe o mês com 1 ou 2 dígitos (ex: 1 ou 01)\n\n" +
                        "CNPJ: Informe o CNPJ do emitente (14 caracteres, alfanumérico)\n\n" +
                        "Modelo: Selecione o modelo do documento fiscal\n\n" +
                        "Série: Informe a série (até 3 dígitos; zeros à esquerda são inseridos automaticamente)\n\n" +
                        "Número: Informe o número da NF-e (até 9 dígitos; zeros à esquerda são inseridos automaticamente)\n\n" +
                        "Desenvolvido por Bruno Fonseca\n" + "Bruno.Fonseca@pentare.com.br\n\n";

            MessageBox.Show("As informações dos campos são: \n\n" + mensagem,"Informação",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            string mensagem;
            string ver = "4.00";


            mensagem = "Suporte a CNPJ alfanumérico (A=10, B=11, ..., Z=35).\n" +
                       "Campos separados para Ano e Mês com autocompletar (ex: '26' → '2026', '1' → '01').\n" +
                       "Preenchimento automático de zeros à esquerda nos campos Série e Número.\n" +
                       "Estado com autocompletar ao digitar (ex: 'sc' → 'SC').\n" +
                       "Modernização do código (ArrayList → List<string>).";

            MessageBox.Show("As atualizações disponibilizadas na versão " + ver + "\n\n" + mensagem,"Informações",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
