using ApiHelper;
using DogFetchApp.ViewModels;
using System.Windows.Navigation;
using System.Diagnostics;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows;

namespace DogFetchApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel currentViewmodel;
        
        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.ApiHelper.InitializeClient();

            currentViewmodel = new MainViewModel();

            DataContext = currentViewmodel;

           
        }

       
    }
}
