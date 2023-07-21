using App1._1.Models;
using App1._1.ViewModels;

namespace App1._1.Views;

public partial class DetailsPage : ContentPage
{
    private PostsViewModel _postViewModel;
    private PostModel _postModel;

    public DetailsPage(PostModel postModel)
	{
        
        _postViewModel = new PostsViewModel();
        _postModel = postModel; 
        InitializeComponent();
        
	}


   
    private async void UpdateBtn_Clicked(object sender, EventArgs e)
    {
            _postModel.Image = _postViewModel.Image;
            _postModel.Text = inputText.Text;
            _postViewModel.SaveUpdatedPosts(_postModel);
        
       
        await Navigation.PushAsync(new MainPage());
    }

    private async void DeleteBtn_Clicked(object sender, EventArgs e)
    {
        _postModel.Text = inputText.Text;
        _postViewModel.SaveDeteledPost(_postModel.Id);
        await Navigation.PushAsync(new MainPage());
    }
    //  Bigger parts of Code source: https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/device-media/picker?tabs=android 
    private async void TakePictureBtn_Clicked(object sender, EventArgs e)
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult picture = await MediaPicker.Default.CapturePhotoAsync();
            _postViewModel.Image = picture.FullPath;

            if (picture != null)
            {
                // saving the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, picture.FileName);

                // Stream converts a screenshot in to an imagefile that can be used by the application
                var stream = await picture.OpenReadAsync();
                anImage.Source = ImageSource.FromStream(() => stream);

            }
        }
    }
    // Code source = https://www.youtube.com/watch?v=Wg1fhr3iwKY&list=LL&index=10&t=615s
    private async void FilePickerBtn_Clicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Pick Image Please",
            FileTypes = FilePickerFileType.Images

        });
        if (result == null)
        {
            return;
        }
        _postViewModel.Image = result.FileName;

        var stream = await result.OpenReadAsync();

        anImage.Source = ImageSource.FromStream(() => stream);
    }
}