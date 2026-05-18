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
    }
}