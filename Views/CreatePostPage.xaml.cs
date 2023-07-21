using App1._1.Models;
using App1._1.ViewModels;

namespace App1._1.Views;

public partial class CreatePostPage : ContentPage
{

    private PostsViewModel _postsViewModel;

    public CreatePostPage()
	{
        _postsViewModel = new PostsViewModel();
        InitializeComponent();
        BindingContext = _postsViewModel;
    }
    private async void TakePictureBtn_Clicked(object sender, EventArgs e)
    {
        //  Bigger parts of Code source: https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/device-media/picker?tabs=android 
        
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult picture = await MediaPicker.Default.CapturePhotoAsync();
            _postsViewModel.Image = picture.FullPath;

            if (picture != null)
            {
                // saving the file into temporal cashfile
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
        _postsViewModel.Image = result.FileName;
        
        var stream = await result.OpenReadAsync();

        anImage.Source = ImageSource.FromStream(() => stream);
    }

    private async void SaveBtn_Clicked(object sender, EventArgs e)
    {     if (_postsViewModel.Image == null)
        {
            anImage.Source = "noimage.png";
            _postsViewModel.SaveNewPost(new PostModel { Rubric = inputRubric.Text, Image = "noimage.png", Text = inputText.Text, DateTime = DateTime.Now });
        }
        else
        {
            _postsViewModel.SaveNewPost(new PostModel { Rubric = inputRubric.Text, Image = _postsViewModel.Image, Text = inputText.Text, DateTime = DateTime.Now });
        }
       
         await  Navigation.PushAsync(new MainPage());
    }

    private async void CancelBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}