// Copyright 2016 Esri.
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at: http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an 
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific 
// language governing permissions and limitations under the License.

using ArcGISRuntime.Samples.Shared.Data;
using ArcGISRuntime.Samples.Shared.Managers;
using Forms.Helpers;
using System;
using System.IO;
using Xamarin.Forms;

namespace ArcGISRuntime
{
    public partial class App : Application
    {
        static SQliteHelper db;
        public App ()
        {

            InitializeComponent();
            // Página raíz de su aplicación
            var navigationPage = new NavigationPage(new CategoryListPage
            {
                Title = "AR SIG para usuarios de salud"
            });

            MainPage = navigationPage;
        }

        public static SQliteHelper SQLiteDB
        {
            get
            {
                if (db==null)
                {
                    db = new SQliteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"SaludRed.db3"));
                }
                return db;
            }
        }
        protected override async void OnStart ()
        {
            // Handle when your app starts
            //await FileAccess.CopyVideoIfNotExists("XamarinForms101UsingEmbeddedImages.mp4");

        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }
    }
}
