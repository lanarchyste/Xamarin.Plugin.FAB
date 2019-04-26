using Xamarin.Forms;

namespace FABSample
{
    public class App : Application
    {
        public App()
        {
            var tabbed = new TabbedPage();

            tabbed.Children.Add(new NavigationPage(new CSharpExample()) { Icon = "csharp.png" });
            tabbed.Children.Add(new NavigationPage(new XamlExample()) { Icon = "xml.png" });
            tabbed.Children.Add(new NavigationPage(new ListExample()) { Icon = "list.png" });

            this.MainPage = tabbed;
        }
    }
}