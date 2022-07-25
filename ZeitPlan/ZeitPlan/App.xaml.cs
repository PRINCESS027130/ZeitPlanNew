using Firebase.Database;
using Firebase.Storage;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.LoginSystem;
using ZeitPlan.Services;
using ZeitPlan.Views;
using ZeitPlan.Views.Admin;

namespace ZeitPlan
{
    public partial class App : Application
    {
        public static string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "dbZeitPlan.db3");
        public static SQLiteConnection db = new SQLiteConnection(dbpath);
        //Firebase Connections  ======================================
        public static FirebaseStorage FirebaseStorage = new FirebaseStorage("gs://zeitplan-6d6dc.appspot.com");

        public static FirebaseClient firebaseDatabase = new FirebaseClient("https://zeitplan-6d6dc-default-rtdb.firebaseio.com/");

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new StartPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
