using ClientConvertisseurV1.Models;
using ClientConvertisseurV1.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClientConvertisseurV1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConvertisseurEuroPage : Page
    {
        private WSService service = new WSService();
        public ConvertisseurEuroPage()
        {
            this.InitializeComponent();
            ActionGetData();
        }
        private void btn_convertir_Click(object sender, RoutedEventArgs e)
        {
            int id_devise = -1;
            var combobox_value = this.combo_devise.SelectedValue;
            if(combobox_value != null)
            {
                id_devise = Int32.Parse(combobox_value.ToString());
            }
            else
            {
                this.DisplayErrorDialog("Pas de devise sélectionnée", "Merci de sélectionner une devise !");
            }
            if (id_devise != -1) {
                Double val_origine = -1;
                try
                {
                    val_origine = Double.Parse(this.input_montant_euros.Text.ToString());
                }
                catch {
                    this.DisplayErrorDialog("Montant invalide !", "Merci de noter un montant valide.");
                }
                if (val_origine != -1)
                {
                    this.CalculConvertion(id_devise, val_origine);
                }
            }
        }

        private async void DisplayErrorDialog(String title, String content)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "Ok"
            };
            dialog.XamlRoot = this.Content.XamlRoot;
            await dialog.ShowAsync();
        }

        private async void CalculConvertion(int id_devise, Double value_origine)
        {
            try { 
            var result = await this.service.GetDeviseAsync(id_devise);
            Devise devise = result;
            Double val_new = value_origine * devise.Taux;
            this.input_montant_devise.Text = val_new.ToString();
            } catch
            {
                this.DisplayErrorDialog("Erreur réseau", "Impossible d'accéder au serveur.");
            }
        }   

        private async void ActionGetData()
        {
            try
            {
                var result = await this.service.GetDevisesAsync();
                this.combo_devise.DataContext = new List<Devise>(result);
            } catch
            {
                this.DisplayErrorDialog("Erreur réseau", "Impossible d'accéder au serveur.");
            }
        }

    }
}
