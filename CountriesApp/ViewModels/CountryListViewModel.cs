using System.Windows.Input;
using System.Collections.ObjectModel;

using CountriesApp.Models;
using CountriesApp.Services;

namespace CountriesApp.ViewModels
{
    internal class CountryListViewModel : BaseViewModel
    {
        public ObservableCollection<Country> Countries { get; }

        public ICommand LoadCountriesCommand { get; private set; }

        private async Task LoadData()
        {
            IsBusy = true;

            var list = await GeonamesService.GetCountries();
            Countries.Clear();

            foreach (Country country in list)
            {
                if (!string.IsNullOrWhiteSpace(country.CurrencyCode))
                {
                    string code = country.CountryCode.ToLower();
                    country.FlagUrl = $"https://raw.githubusercontent.com/hjnilsson/country-flags/master/png250px/{code}.png";
                }

                Countries.Add(country);
            }

            IsBusy = false;

        }

        public CountryListViewModel()
        {
            Countries = new ObservableCollection<Country>();
            LoadCountriesCommand = new Command(async() => await LoadData());
        }
    }
}
