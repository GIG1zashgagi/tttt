using System;               
using System.Numerics;      
using System.Windows;

namespace ttt
{
    public partial class MainWindow : Window
    {
        private BigInteger _publicKey;  
        private BigInteger _privateKey;  
        private BigInteger _modulus;    

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtP.Text = "17";
            txtQ.Text = "19";

            GenerateKeys();
        }

        private void SetStatus(string message, bool isError = false)
        {
            lblStatus.Text = isError ? $"{message}" : $"{message}";
            lblStatus.Foreground = isError ?
                (System.Windows.Media.Brush)System.Windows.Media.Brushes.Red :
                (System.Windows.Media.Brush)System.Windows.Media.Brushes.Green;
        }
        private void GenerateKeys()
        {
            try
            {
                if (!int.TryParse(txtP.Text, out int p))
                {
                    SetStatus("Ошибка: p должно быть целым числом", true);
                    return;
                }

                if (!int.TryParse(txtQ.Text, out int q))
                {
                    SetStatus("Ошибка: q должно быть целым числом", true);
                    return;
                }

                RsaAlgorithm.GenerateKeys(p, q, out _publicKey, out _privateKey, out _modulus);

                txtPublicKey.Text = _publicKey.ToString();
                txtPrivateKey.Text = _privateKey.ToString();

                SetStatus("Ключи успешно сгенерированы");
            }
            catch (Exception ex)
            {
                SetStatus($"Ошибка генерации ключей: {ex.Message}", true);
            }
        }
        private void BtnGenerateKeys_Click(object sender, RoutedEventArgs e)
        {
            GenerateKeys();
        }
    }
}