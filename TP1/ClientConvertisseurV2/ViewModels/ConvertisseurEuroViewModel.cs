using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConvertisseurV2.ViewModels
{
    internal class ConvertisseurEuroViewModel:ObservableObject
    {

        private WSService service = new WSService();
        public IRelayCommand BtnSetConversion { get; }
        public ConvertisseurEuroViewModel()
        {
            ActionGetData();

            BtnSetConversion = new RelayCommand(ActionSetConversion);
        }
        private async void ActionGetData()
        {
            var result = await service.GetDevisesAsync();
            Devises = new ObservableCollection<Devise>(result);
        }
        private void ActionSetConversion()
        {
            Double originale_value = -1;
            try
            {
                originale_value = Double.Parse(montantEuros);
            }
            catch
            {
                this.DisplayErrorDialog("Montant invalide !", "Merci de noter un montant valide.");
            }
            if (originale_value != -1)
            {
                if(deviseSelected == null)
                {
                    this.DisplayErrorDialog("Pas de devise sélectionnée !", "Merci de sélectionner une devise.");
                }
                else
                {
                    Double new_value = originale_value * deviseSelected.Taux * originale_value;
                    MontantDevise = new_value.ToString();
                }
            }
        }

        private ObservableCollection<Devise> devises;
        public ObservableCollection<Devise> Devises
        {
            get { return devises; }
            set
            {
                devises = value;
                OnPropertyChanged();// Pour notifier la vue de la modification de ses données
            }
        }

        private string montantEuros;
        public string MontantEuros
        {
            get { return montantEuros; }
            set
            {
                montantEuros = value;
                OnPropertyChanged();
            }
        }

        private Devise deviseSelected;
        public Devise DeviseSelected
        {
            get { return deviseSelected; }
            set
            {
                deviseSelected = value;
                OnPropertyChanged();
            }
        }

        private string montantDevise;
        public string MontantDevise
        {
            get { return montantDevise; }
            set
            {
                montantDevise = value;
                OnPropertyChanged();
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
            dialog.XamlRoot = App.MainRoot.XamlRoot; ;
            await dialog.ShowAsync();
        }
    }

}
