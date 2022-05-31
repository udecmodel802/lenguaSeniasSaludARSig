// Copyright 2019 Esri.
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at: http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an 
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific 
// language governing permissions and limitations under the License.

#if XAMARIN_ANDROID
using ArcGISRuntime;
using Esri.ArcGISRuntime.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ArcGISRuntimeXamarin.Samples.ReverseGeocode
{
    [ArcGISRuntime.Samples.Shared.Attributes.Sample(
        name: "Información Centros de Salud",
        category: "Información Centros de Salud",
        description: "Use an online service to find the address for a tapped point.",
        instructions: "Tap the map to see the nearest address displayed in a callout.",
        tags: new[] { "address", "geocode", "locate", "Información Centros de Salud", "search" })]
    public partial class ReverseGeocode : ContentPage
    {

        public bool IsVisibleTablaSeviciosCentroSalud = false;
        public ReverseGeocode()
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
                MySuggestButton.IsEnabled = true;
                CopyButton2.IsEnabled = true;
                tablaSeviciosCentroSalud.IsEnabled = true;
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.ToString(), "OK");
            }
        }

        private async void SuggestionButtonTapped(object sender, System.EventArgs e)
        {
            if (CentroSaludSelector.SelectedIndex != -1)
            {
                IsVisibleTablaSeviciosCentroSalud = !IsVisibleTablaSeviciosCentroSalud;
                tablaSeviciosCentroSalud.IsVisible = IsVisibleTablaSeviciosCentroSalud;
                CopyButton2.IsVisible = IsVisibleTablaSeviciosCentroSalud;

                if (IsVisibleTablaSeviciosCentroSalud)
                {
                    var centroSaludR = await App.SQLiteDB.GetCentroSaludByNameAsync(CentroSaludSelector.Items[CentroSaludSelector.SelectedIndex]);

                    tieneMedicinaGeneral.Text = centroSaludR.MedicinaGeneralCentroSalud;
                    tieneUrgencias.Text = centroSaludR.UrgenciasCentroSalud;
                    tieneOdontologia.Text = centroSaludR.OdontologiaCentroSalud;
                    tieneMedicinaFamiliar.Text = centroSaludR.MedicinaFamilCentroSalud;
                    tieneFonoaudiologia.Text = centroSaludR.FonoaudiologiaCentroSalud;
                    tieneMamografias.Text = centroSaludR.MamografiasCentroSalud;
                    tienePsiquiatria.Text = centroSaludR.PsiquiatriaCentroSalud;
                    tieneTrabajoSocial.Text = centroSaludR.TrabajoSocialCentroSalud;
                    tienePlanificacionFamiliar.Text = centroSaludR.PlanificacionFamilCentroSalud;
                    tieneFisioterapia.Text = centroSaludR.FisioterapiaCentroSalud;
                    txtDireccion.Text = centroSaludR.DireccionCentroSalud;
                    txtHorario.Text = centroSaludR.HorarioCentroSalud;

                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Mensaje", "Por favor, seleccione un centro de salud.", "Ok");
            }
        }

        private async void CopyButtonTapped2(object sender, System.EventArgs e)
        {
            await Clipboard.SetTextAsync(txtDireccion.Text);
        }

        private async void CambioCentroSaludSelector(object sender, System.EventArgs e)
        {
            if (CentroSaludSelector.SelectedIndex != -1 && IsVisibleTablaSeviciosCentroSalud == true)
            {
                IsVisibleTablaSeviciosCentroSalud = !IsVisibleTablaSeviciosCentroSalud;
                tablaSeviciosCentroSalud.IsVisible = IsVisibleTablaSeviciosCentroSalud;
                CopyButton2.IsVisible = IsVisibleTablaSeviciosCentroSalud;
            }
            else if (IsVisibleTablaSeviciosCentroSalud == true)
            {
                await Application.Current.MainPage.DisplayAlert("Mensaje", "Por favor, seleccione un centro de salud.", "Ok");
            }


        }
    }
}
#endif