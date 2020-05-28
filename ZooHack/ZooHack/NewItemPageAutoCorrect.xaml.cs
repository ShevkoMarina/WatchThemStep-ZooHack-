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
    public partial class NewItemPageAutoCorrect : ContentPage
    {
        public NewItemPageAutoCorrect()
        {
            InitializeComponent();

            secondName1.Text = Classes.Person.SecondName;
            firstName1.Text = Classes.Person.FirstName;
            status1.Text = Classes.Person.Status;
            thirdName1.Text = Classes.Person.ThirdName;        
            workPlace1.Text = Classes.Person.WorkPlace;
        }
        private void SaveMainInfo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewItemPage());
        }
    }
}