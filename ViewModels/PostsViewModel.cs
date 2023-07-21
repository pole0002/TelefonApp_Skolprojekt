using App1._1.Models;
using App1._1.Views;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1._1.ViewModels
{
   public partial class PostsViewModel: BaseViewModel
    {

        public string Image { get; set; }
        private ObservableCollection<PostModel> _postList = new ObservableCollection<PostModel>();

        public ObservableCollection<PostModel> Posts
        {
            get
            {
                return _postList;
            }
        }
        public async void GetPosts()
        {
            try
            {
                //10.0.2.2 for Android, localhost for windows
                string baseUrl = @"http://10.0.2.2:5183/api/PostModel";// @"http://10.0.2.2:5183/api/PostModel"; // http://localhost:5183/api/PostModel;
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(new Uri(baseUrl));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    ObservableCollection<PostModel> jsonResponse = JsonConvert.DeserializeObject<ObservableCollection<PostModel>>(content);

                    _postList.Clear();
                    foreach (var post in jsonResponse)
                    {
                        _postList.Add(post);
                    }

                }
                else
                    await Application.Current.MainPage.DisplayAlert("Error", "We are sorry, the internet connection is not available", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "We are sorry, the internet connection is not available.(" + ex.Message + ")", "OK");
            }

        }

        // Source code for going to detailpages of each item in the collectionview : https://www.youtube.com/watch?v=DuNLR_NJv8U&t=4246s
        public async void SaveNewPost(PostModel postModel) // sparametoden
        {
            try
            {
                //10.0.2.2 for Android, localhost for windows
                string baseUrl = @"http://10.0.2.2:5183/api/PostModel";// @"http://10.0.2.2:5183/api/PostModel"; // http://localhost:5183/api/PostModel;
                HttpClient httpClient = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(postModel), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(baseUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Saving", "Your post was saved succesfully!", "Ok");
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Something went wrong.", "Double check if the problem has to do with the database or the application it-self.", "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "We are sorry, the connection to the webservice is not available. (" + ex.Message + ")", "Ok");
            }

        }
        public async void SaveUpdatedPosts(PostModel postModel) // sparametoden
        {

            try
            {
                //10.0.2.2 for Android, localhost for windows
                string baseUrl = @"http://10.0.2.2:5183/api/PostModel/" + postModel.Id;// @"http://10.0.2.2:5183/api/PostModel"; // http://localhost:5183/api/PostModel;
                HttpClient httpClient = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(postModel), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PutAsync(baseUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Saving", "Your post was updated succesfully", "OK");
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Error", "We are sorry, Double check if the problem has to do with the database or the application it-self.", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "We are sorry, the internet connection is not available. (" + ex.Message + ")", "OK");
            }

        }
        public async void SaveDeteledPost(int id) // sparametoden
        {
            try
            {
                //10.0.2.2 for Android, localhost for windows
                string baseUrl = @"http://10.0.2.2:5183/api/PostModel/" + id;// @"http://10.0.2.2:5183/api/PostModel"; // http://localhost:5183/api/PostModel;
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.DeleteAsync(baseUrl);
                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Saving", "Your changes were saved succesfully", "OK");
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Error", "Have you filled out all of the entries?.", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "We are sorry, the internet connection is not available. (" + ex.Message + ")", "OK");
            }
        }    
    }
}
