using FAB.Forms;
using Xamarin.Forms;

namespace FABSample
{
    public class CSharpExample : ContentPage
    {
        private FloatingActionButton miniFab;
        private FloatingActionButton normalFab;

        private FloatingActionToggleButton miniToggleFab;
        private FloatingActionToggleButton normalToggleFab;

        public CSharpExample()
        {
            this.Title = "C# Example";
            this.BackgroundColor = Color.White;

            var layout = new RelativeLayout();

            var green = new Button()
            {
                Text = "Green",
                Command = new Command(() => { this.UpdateButtonColor(Color.Green); })
            };

            var red = new Button()
            {
                Text = "Red",
                Command = new Command(() => { this.UpdateButtonColor(Color.Red); })
            };

            var blue = new Button()
            {
                Text = "Blue",
                Command = new Command(() => { this.UpdateButtonColor(Color.Blue); })
            };

            var noShadow = new Button()
            {
                Text = "Change Shadow",
                Command = new Command(() =>
                {
                    this.miniFab.HasShadow = !this.miniFab.HasShadow;
                    this.normalFab.HasShadow = !this.normalFab.HasShadow;
                })
            };

            var toggleAnimationOnSelection = new Button()
            {
                Text = "Toggle Animation on Selection",
                Command = new Command(() =>
                {
                    this.miniFab.AnimateOnSelection = !this.miniFab.AnimateOnSelection;
                    this.normalFab.AnimateOnSelection = !this.normalFab.AnimateOnSelection;
                })
            };

            Button disable = null;
            disable = new Button()
            {
                Text = "Disabled",
                Command = new Command(() =>
                {
                    this.miniFab.IsEnabled = !this.normalFab.IsEnabled;
                    this.normalFab.IsEnabled = !this.normalFab.IsEnabled;

                    disable.Text = this.miniFab.IsEnabled ? "Disable" : "Enable";
                })
            };

            layout.Children.Add(
                new StackLayout
                {
                    Padding = new Thickness(15),
                    Children =
                    {
                        green,
                        red,
                        blue,
                        disable,
                        noShadow,
                        toggleAnimationOnSelection
                    }
                },
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
            );

            normalFab = new FloatingActionButton();
            normalFab.Source = "plus.png";
            normalFab.Size = FabSize.Normal;
            normalFab.RippleColor = Color.Gray;

            layout.Children.Add(
                normalFab,
                xConstraint: Constraint.RelativeToParent((parent) => { return (parent.Width - normalFab.Width) - 16; }),
                yConstraint: Constraint.RelativeToParent((parent) => { return (parent.Height - normalFab.Height) - 16; })
            );

            normalFab.SizeChanged += (sender, args) => { layout.ForceLayout(); };

            miniFab = new FloatingActionButton();
            miniFab.Source = "plus.png";
            miniFab.Size = FabSize.Mini;
            miniFab.RippleColor = Color.Gray;

            layout.Children.Add(
                miniFab,
                xConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return (parent.Width - miniFab.Width) - 16;
                }),
                yConstraint: Constraint.RelativeToView(normalFab, (parent, view) =>
                {
                    return (view.Y - miniFab.Height) - 16;
                })
            );
            miniFab.SizeChanged += (sender, args) => { layout.ForceLayout(); };


            normalFab.Clicked += (sender, e) =>
            {
                this.DisplayAlert("Floating Action Button", "You clicked the normal FAB!", "Awesome!");
            };

            miniFab.Clicked += (sender, e) =>
            {
                this.DisplayAlert("Floating Action Button", "You clicked the mini FAB!", "Awesome!");
            };


            normalToggleFab = new FloatingActionToggleButton();
            normalToggleFab.CheckedImage = "csharp.png";
            normalToggleFab.UnCheckedImage = "xml.png";
            normalToggleFab.UnCheckedColor = Color.Gray;
            normalToggleFab.AnimateOnSelection = false;
            normalToggleFab.Size = FabSize.Normal;
            normalToggleFab.RippleColor = Color.Gray;

            layout.Children.Add(
                normalToggleFab,
                xConstraint: Constraint.RelativeToParent((parent) => { return (parent.Width - normalToggleFab.Width) - 116; }),
                yConstraint: Constraint.RelativeToParent((parent) => { return (parent.Height - normalToggleFab.Height) - 16; })
            );

            normalToggleFab.SizeChanged += (sender, args) => { layout.ForceLayout(); };

            miniToggleFab = new FloatingActionToggleButton();
            miniToggleFab.CheckedImage = "csharp.png";
            miniToggleFab.UnCheckedImage = "xml.png";
            miniToggleFab.CheckedColor = Color.Green;
            miniToggleFab.UnCheckedColor = Color.Yellow;
            miniToggleFab.AnimateOnSelection = false;
            miniToggleFab.Size = FabSize.Mini;
            miniToggleFab.RippleColor = Color.Gray;

            layout.Children.Add(
                miniToggleFab,
                xConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return (parent.Width - miniToggleFab.Width) - 116;
                }),
                yConstraint: Constraint.RelativeToView(normalFab, (parent, view) =>
                {
                    return (view.Y - miniFab.Height) - 16;
                })
            );
            miniToggleFab.SizeChanged += (sender, args) => { layout.ForceLayout(); };


            normalToggleFab.Clicked += (sender, e) =>
            {
                this.DisplayAlert("Floating Action Button", "You clicked the normal toggle FAB!", "Awesome!");
            };

            miniToggleFab.Clicked += (sender, e) =>
            {
                this.DisplayAlert("Floating Action Button", "You clicked the mini toggle FAB!", "Awesome!");
            };

            this.Content = layout;
        }

        private void UpdateButtonColor(Color color)
        {
            var normal = color;
            var disabled = color.MultiplyAlpha(0.25);

            miniFab.NormalColor = normal;
            miniFab.DisabledColor = disabled;

            normalFab.NormalColor = normal;
            normalFab.DisabledColor = disabled;
        }
    }
}


