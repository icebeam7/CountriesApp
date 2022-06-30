using CountriesApp.ViewModels;

namespace CountriesApp.Views;

public partial class CountryListView : ContentPage
{
	public CountryListView()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		var vm = (CountryListViewModel)BindingContext;
        vm.LoadCountriesCommand.Execute(null);
    }
}