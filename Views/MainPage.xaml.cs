using App1._1.Models;
using App1._1.ViewModels;
using App1._1.Views;
using CommunityToolkit.Mvvm.Input;

namespace App1._1;
// DETTA ÄR DEN NYA APPEN
public partial class MainPage : ContentPage
{
    private PostsViewModel _postsViewModel;

    public MainPage()
	{
        _postsViewModel = new PostsViewModel();
        BindingContext = _postsViewModel;
        _postsViewModel.GetPosts();
        InitializeComponent();
		
	}
    private async void postList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item == null)
        {
            return;
        }
        PostModel postModel = (PostModel)e.Item;
        var updateDetailsPage = new DetailsPage(postModel);
        updateDetailsPage.BindingContext = postModel;

        await Navigation.PushAsync(updateDetailsPage);
    }
}

