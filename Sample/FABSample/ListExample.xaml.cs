using System.Collections.Generic;
using Xamarin.Forms;

namespace FABSample
{
    public partial class ListExample : ContentPage
    {
        public ListExample()
        {
            InitializeComponent();

            var items = new List<string>();
            for (int i = 0; i < 50; i++)
            {
                items.Add(string.Format("Item {0}", i));
            }

            this.list.ItemsSource = items;
        }

        void Handle_FabClicked(object sender, System.EventArgs e)
        {
            this.DisplayAlert("Floating Action Button", "You clicked the FAB!", "Awesome!");
        }
    }
}

