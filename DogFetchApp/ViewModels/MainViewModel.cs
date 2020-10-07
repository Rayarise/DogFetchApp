using ApiHelper;
using DogFetchApp.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DogFetchApp.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private ObservableCollection<DogModel> listeRace = new ObservableCollection<DogModel>();
        private ObservableCollection<BitmapImage> imgs = new ObservableCollection<BitmapImage>();
        private ObservableCollection<int> nb = new ObservableCollection<int>();
        private DogModel selectedRace;
        private int selectedNum;
        private string race;
        private string raceN;


        public ObservableCollection<int> Nb
        {
            get => nb;
            set
            {
               nb = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DogModel> ListeRace
        {
            get => listeRace;
            set
            {
                listeRace = value;
                OnPropertyChanged();
            }
        }

        public DogModel SelectedRace
        {
            get => selectedRace;
            set
            {
                selectedRace = value;
                OnPropertyChanged();
            }
        }

        public int SelectedNum
        {
            get => selectedNum;
            set
            {
                selectedNum = value;
                OnPropertyChanged();
            }
        }
        public string Race
        {
            get => race;
            set
            {
                race = value;
                OnPropertyChanged();
            }
        }
        public string RaceN
        {
            get => raceN;
            set
            {
                raceN = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<BitmapImage> Imgs
        {
            get => imgs;
            set
            {
                imgs = value;
                OnPropertyChanged();
            }
        }


        public DelegateCommand<object> LoadImage { get; private set; }
        public DelegateCommand<string> changeLanguage { get; private set; }

        public MainViewModel() {
           
            LoadImage = new DelegateCommand<object>(FetchB);
            changeLanguage = new DelegateCommand<string>(ChangeL);

            initValue();
            initnb();
        }

        void initValue() {

            LoadRace();

        }

        void initnb()
        {
            Nb = new ObservableCollection<int> { 1, 3, 5, 10 };

        }

       private async void LoadRace() {


            var res = await DogApiProcessor.LoadBreedList();

            ListeRace = new ObservableCollection<DogModel>(res);



        }

        private async void FetchB(object sender) {

            if (SelectedRace.race == null || SelectedNum == null)
            {
                
            }
            else
            {
                //selectedNum;
                Race = SelectedRace.race;

                await LoadImageList(selectedNum);

            }





        }
        private async Task LoadImageList(int imageN) {

            Imgs = new ObservableCollection<BitmapImage>();

           //int Nb = Int32.Parse(imageN.ToString());

            for (int i = 0; i < imageN; i++)
            {
                var dogimage = await DogApiProcessor.GetImageUrl(Race);

                string UriLink = dogimage.src;

                var uriSource = new Uri(dogimage.src, UriKind.Absolute);


                Imgs.Add(new BitmapImage(uriSource,
                    new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable)));

            }




        }

        private void ChangeL(string sender)
        {

            MessageBoxResult result = MessageBox.Show(Properties.Resources.Msg1, "My App", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Properties.Settings1.Default.Language = sender;
                    Properties.Settings1.Default.Save();
                    Restart();
                    break;
                case MessageBoxResult.No:
                    Properties.Settings1.Default.Language = sender;
                    Properties.Settings1.Default.Save();
                    break;

            }
        }


       

        private void Restart()
        {
            var filename = Application.ResourceAssembly.Location;
            var newFile = Path.ChangeExtension(filename, ".exe");
            Process.Start(newFile);
            Application.Current.Shutdown();
        }




    }
}
