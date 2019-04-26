using Xamarin.Forms;

namespace FABSample
{
    public partial class XamlExample : ContentPage
    {
        public XamlExample()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (sender == this.FindByName<Button>("greenBtn"))
            {
                this.UpdateButtonColor(Color.Green);
            }
            else if (sender == this.FindByName<Button>("redBtn"))
            {
                this.UpdateButtonColor(Color.Red);
            }
            else if (sender == this.FindByName<Button>("blueBtn"))
            {
                this.UpdateButtonColor(Color.Blue);
            }
            else if (sender == this.FindByName<Button>("disabledBtn"))
            {
                this.disabledBtn.Text = this.fabBtn.IsEnabled ? "Disable" : "Enable";
                this.fabBtn.IsEnabled = !this.fabBtn.IsEnabled;
            }
            else if (sender == this.FindByName<Button>("noShadowBtn"))
            {
                this.fabBtn.HasShadow = !this.fabBtn.HasShadow;
            }
        }

        void Handle_FabClicked(object sender, System.EventArgs e)
        {
            this.DisplayAlert("Floating Action Button", "You clicked the FAB!", "Awesome!");
        }

        private void UpdateButtonColor(Color color)
        {
            var normal = color;
            var disabled = color.MultiplyAlpha(0.25);

            fabBtn.NormalColor = normal;
            fabBtn.DisabledColor = disabled;
        }
    }
}