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
        private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string input = txtInput.Text;

                if (string.IsNullOrWhiteSpace(input))
                {
                    MessageBox.Show("Введите текст для шифрования", "Предупреждение",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_publicKey == 0 || _modulus == 0)
                {
                    MessageBox.Show("Сначала сгенерируйте ключи", "Предупреждение",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string encrypted = RsaAlgorithm.EncryptString(input, _publicKey, _modulus);
                txtOutput.Text = encrypted;
                SetStatus("Текст зашифрован успешно");
            }
            catch (Exception ex)
            {
                SetStatus($"Ошибка шифрования: {ex.Message}", true);
            }
        }
        private void BtnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string input = txtOutput.Text;

                if (string.IsNullOrWhiteSpace(input))
                {
                    MessageBox.Show("Нет зашифрованного текста для расшифрования", "Предупреждение",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_privateKey == 0 || _modulus == 0)
                {
                    MessageBox.Show("Сначала сгенерируйте ключи", "Предупреждение",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string decrypted = RsaAlgorithm.DecryptString(input, _privateKey, _modulus);
                txtDecrypted.Text = decrypted;
                SetStatus("Текст расшифрован успешно");
            }
            catch (Exception ex)
            {
                SetStatus($"Ошибка расшифрования: {ex.Message}", true);
            }
        }
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Clear();
            txtOutput.Clear();
            txtDecrypted.Clear();
            SetStatus("Все поля очищены");
        }
    }
}