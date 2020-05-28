using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZooHack
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfile : ContentPage
    {
        public UserProfile()
        {
            InitializeComponent();

        }
        private void OnSaveButton_Clicked(object sender, EventArgs e)
        {
           
            Classes.Person.SecondName = secondName.Text;
            Classes.Person.FirstName = firstName.Text;
            Classes.Person.ThirdName = thirdName.Text;
            Classes.Person.WorkPlace = workPlace.Text;
            Classes.Person.Status = status.Text;

            Navigation.PopAsync();
        }
    }
}