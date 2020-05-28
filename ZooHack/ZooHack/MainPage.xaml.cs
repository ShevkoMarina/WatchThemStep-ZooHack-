using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ZooHack
{
    
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnNewItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewItemPageAutoCorrect());
        }

        private void EditProfile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserProfile());
        }
    }
}
