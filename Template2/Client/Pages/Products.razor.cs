using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MudBlazor;
using Template2.Shared.DTOs;

namespace Template2.Client.Pages;

public partial class Products : ComponentBase
{
    [Parameter] public string Platform { get; set; }

    private List<ProductDto> products = new();
    //private List<ProductDto> displayProducts = new();

    private static readonly string activeOrderStyle = $"color:{Colors.Grey.Darken4};";
    private static readonly string inactiveOrderStyle = $"color:{Colors.Grey.Lighten3};";
    private string orderPriceDesc = activeOrderStyle;
    private string orderPriceAsc = inactiveOrderStyle;

    private List<BreadcrumbItem> breadcrumbs = new();

    private IEnumerable<string>? filterGenre;
    private IEnumerable<string>? filterAge;


    protected override async Task OnInitializedAsync()
    {
        breadcrumbs = new()
        {
            new("Home", href: "/", icon: Icons.Material.Filled.Home),
            new("Games", href:"#"),
            new(Platform.ToUpper(), "", disabled: true)
        };
        Uri uri = new Uri(new($"Product/List/{Platform}"));
        await GetProducts(uri);
    }

    private async Task GetProducts(Uri uri)
    {
        try
        {
            ServiceResponse<List<ProductDto>>? res = await _public.Client.GetFromJsonAsync<ServiceResponse<List<ProductDto>>>(uri);
            if (res?.IsSuccessful ?? false)
            {
                products = res.Data ?? new();
            }
            else
            {
                Console.WriteLine(res?.Message ?? "Fatal Error");
            }
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }
    }

    private async void FilterProducts(ChangeEventArgs e)
    {
        Console.WriteLine("Genre: " + filterGenre?.Count());
        Console.WriteLine("Age: " + filterAge?.Count());
        //UriBuilder builder = new($"Product/List/{Platform}");


        //if ((filterGenre?.Any() ?? false) || (filterAge?.Any() ?? false))
        //{
        //    string query;

            

        //}

        //await GetProducts(builder.Uri);
        //displayProducts = products
        //    .Where(p => p.Genre.Equals(filterGenre.First(), StringComparison.InvariantCultureIgnoreCase))
        //    .ToList();
    }

    private async void ClearFilters()
    {
        filterAge = null;
        filterGenre = null;

        await GetProducts(new($"Product/List/{Platform}"));
    }

    private void Sort(bool desc)
    {
        if (desc)
        {
            orderPriceDesc = activeOrderStyle;
            orderPriceAsc = inactiveOrderStyle;
        }
        else
        {
            orderPriceDesc = inactiveOrderStyle; ;
            orderPriceAsc = activeOrderStyle;
        }
    }
}