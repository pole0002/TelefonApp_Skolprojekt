using App1._1.ViewModels;
using App1._1.Views;

namespace App1._1;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<CreatePostPage>();
        builder.Services.AddSingleton<AboutPage>();
        builder.Services.AddSingleton<SettingsPage>();

        builder.Services.AddSingleton<PostsViewModel>();

		//new instance for every time you go to the page
        builder.Services.AddTransient<DetailsPage>();
        
        return builder.Build();
	}
}
