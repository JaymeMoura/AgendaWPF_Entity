using AgendaWPF_Entity.models;
using System.Linq;
using System.Windows;

namespace AgendaWPF_Entity
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        private string operacao;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
            this.AlteraBotoes(1);
        }
        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            //contato com os dados da tela

            if (string.IsNullOrWhiteSpace(TxtNome.Text))
            {
                ShowErrorMessage("Nome não pode ser vazio!");
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtTelefone.Text))
            {
                ShowErrorMessage("Telefone não pode ser vazio!");
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtEmail.Text))
            {
                ShowErrorMessage("E-mail nao pode ser vazio!");
                return;
            }

            contato c = new contato
            {
                nome = TxtNome.Text,
                email = TxtEmail.Text,
                telefone = TxtTelefone.Text
            };

            //gravar no banco de dados
            if (operacao == "inserir")
            {
                using (agendaEntities ctx = new agendaEntities())
                {
                    ctx.contatos.Add(c);
                    ctx.SaveChanges();
                }
            }
            if (operacao == "alterar")
            {

            }
            this.ListarContatos();
            this.AlteraBotoes(1);
            this.LimparCampo();
        }

        private void LimparCampo()
        {
            TxtID.IsEnabled = true;
            TxtID.Clear();
            TxtEmail.Clear();
            TxtNome.Clear();
            TxtTelefone.Clear();
        }

        private void btInserir_Click(object sender, RoutedEventArgs e)
        {
            this.operacao = "inserir";
            this.AlteraBotoes(2);
            TxtID.Text = "";
            TxtID.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ListarContatos();
        }

        private void ListarContatos()
        {
            using (agendaEntities ctx = new agendaEntities())
            {
                var consulta = ctx.contatos;
                dgDados.ItemsSource = consulta.ToList();
            }
        }

        private void AlteraBotoes(int op)
        {
            btAlterar.Visibility = Visibility.Collapsed;
            btInserir.Visibility = Visibility.Collapsed;
            btExcluir.Visibility = Visibility.Collapsed;
            btCancelar.Visibility = Visibility.Collapsed;
            btLocalizar.Visibility = Visibility.Collapsed;
            btSalvar.Visibility = Visibility.Collapsed;

            if (op == 1)
            {
                btInserir.Visibility = Visibility.Visible;
                btLocalizar.Visibility = Visibility.Visible;
            }

            if(op == 2)
            {//inserir um valor
                btCancelar.Visibility = Visibility.Visible;
                btSalvar.Visibility = Visibility.Visible;
            }

            if(op == 3)
            {
                btAlterar.Visibility = Visibility.Visible;
                btExcluir.Visibility = Visibility.Visible;
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.AlteraBotoes(1);
            this.LimparCampo();
        }
    }
}
