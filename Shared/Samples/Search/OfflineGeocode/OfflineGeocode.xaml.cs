// Copyright 2019 Esri.
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at: http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an 
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific 
// language governing permissions and limitations under the License.


using ArcGISRuntime;
using ArcGISRuntime.Samples.Managers;
using ArcGISRuntime.Samples.Shared.Models;
#if XAMARIN_ANDROID
using Esri.ArcGISRuntime.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace ArcGISRuntimeXamarin.Samples.OfflineGeocode
{
    [ArcGISRuntime.Samples.Shared.Attributes.Sample(
        name: "Mi información",
        category: "Mi información",
        description: "Geocode addresses to locations and reverse geocode locations to addresses offline.",
        instructions: "Type the address in the Search menu option or select from the list to `Geocode` the address and view the result on the map. Tap the location you want to reverse geocode. Tap the pin to see the full address.",
        tags: new[] { "geocode", "geocoder", "locator", "offline", "package", "query", "search" })]
    //[ArcGISRuntime.Samples.Shared.Attributes.OfflineData("22c3083d4fa74e3e9b25adfc9f8c0496", "3424d442ebe54f3cbf34462382d3aebe")]
    public partial class OfflineGeocode : ContentPage
    {

        public OfflineGeocode()
        {
            InitializeComponent();
            Initialize();
        }

        private async void Initialize()
        {

            
            // Initialize the LocatorTask with the provided service Uri.
            try
            {
                

                // Enable UI controls now that the LocatorTask is ready.
                MySaveButtonUsuario.IsEnabled = true;

                var usuarioSaludR = await App.SQLiteDB.GetUsuarioSaludByIdAsync(1);

                if (usuarioSaludR != null)
                {
                    regitroUsuarioHecho.Text = "¡" + usuarioSaludR.NombresUsuarioSalud + " tus datos ya estan registrados.";
                    regitroUsuarioHecho.TextColor = Color.Orange;
                    MySaveButtonUsuario.IsEnabled = false;
                    MySaveButtonUsuario.BackgroundColor = Color.Gray;
                    await Application.Current.MainPage.DisplayAlert("Mensaje", "Tus datos ya estan registrados", "OK");
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.ToString(), "OK");
            }
        }

        private async void SaveButtonTapped11(object sender, System.EventArgs e)
        {

            if (ValidarDatosUsuario())
            {
                UsuarioSalud usuarioSalud = new UsuarioSalud
                {
                    NombresUsuarioSalud = txtNombreUsuario.Text,
                    ApellidosUsuarioSalud = txtApellidosUsuario.Text,
                    EdadUsuarioSalud = int.Parse(txtEdadUsuario.Text),
                    EpsUsuarioSalud = EpsSelector.Items[EpsSelector.SelectedIndex],
                };
                await App.SQLiteDB.SaveUsuariosSalud(usuarioSalud);
                txtNombreUsuario.Text = "";
                txtApellidosUsuario.Text = "";
                txtEdadUsuario.Text = "";
                txtEpsUsuario.Text = "";

                await Application.Current.MainPage.DisplayAlert("Mensaje","Se guardaron los datos correctamente.","Ok");

                var usuarioSaludR = await App.SQLiteDB.GetUsuarioSaludByIdAsync(1);

                if (usuarioSaludR != null)
                {
                    regitroUsuarioHecho.Text = "¡" + usuarioSaludR.NombresUsuarioSalud + " tus datos ya estan registrados.";
                    regitroUsuarioHecho.TextColor = Color.Orange;
                    MySaveButtonUsuario.IsEnabled = false;
                    MySaveButtonUsuario.BackgroundColor = Color.Gray;
                }

                
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "Ingresar todos los datos", "Ok");
            }
        }

        public bool ValidarDatosUsuario()
        {
            bool respuesta;
            
            if (string.IsNullOrEmpty(txtNombreUsuario.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellidosUsuario.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEdadUsuario.Text))
            {
                respuesta = false;
            }
            else if (EpsSelector.SelectedIndex==-1)
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }

            return respuesta;
        }




    }
}

#endif