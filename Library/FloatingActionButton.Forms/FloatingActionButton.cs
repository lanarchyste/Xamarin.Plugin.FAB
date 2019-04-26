using System;
using Xamarin.Forms;
using System.Windows.Input;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FAB.Droid")]
[assembly: InternalsVisibleTo("FAB.iOS")]
namespace FAB.Forms
{
    public class FloatingActionButton : View
    {
        public static readonly BindableProperty SizeProperty = BindableProperty.Create<FloatingActionButton, FabSize>(mn => mn.Size, FabSize.Normal);

        public static readonly BindableProperty NormalColorProperty = BindableProperty.Create<FloatingActionButton, Color>(mn => mn.NormalColor, Color.Blue);

        public static readonly BindableProperty RippleColorProperty = BindableProperty.Create<FloatingActionButton, Color>(mn => mn.RippleColor, Color.Gray);

        public static readonly BindableProperty DisabledColorProperty = BindableProperty.Create<FloatingActionButton, Color>(mn => mn.DisabledColor, Color.Gray);

        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create<FloatingActionButton, bool>(mn => mn.HasShadow, true);

        public static readonly BindableProperty SourceProperty = BindableProperty.Create<FloatingActionButton, ImageSource>(mn => mn.Source, null);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create<FloatingActionButton, ICommand>(mn => mn.Command, null, propertyChanged: HandleCommandChanged);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create<FloatingActionButton, object>(mn => mn.CommandParameter, null);

        public static readonly BindableProperty AnimateOnSelectionProperty = BindableProperty.Create<FloatingActionButton, bool>(mn => mn.AnimateOnSelection, true);

        public event EventHandler<EventArgs> Clicked;

        public FabSize Size
        {
            get { return (FabSize)this.GetValue(SizeProperty); }
            set { this.SetValue(SizeProperty, value); }
        }

        public Color NormalColor
        {
            get { return (Color)this.GetValue(NormalColorProperty); }
            set { this.SetValue(NormalColorProperty, value); }
        }

        public Color RippleColor
        {
            get { return (Color)this.GetValue(RippleColorProperty); }
            set { this.SetValue(RippleColorProperty, value); }
        }

        public Color DisabledColor
        {
            get { return (Color)this.GetValue(DisabledColorProperty); }
            set { this.SetValue(DisabledColorProperty, value); }
        }

        public bool HasShadow
        {
            get { return (bool)this.GetValue(HasShadowProperty); }
            set { this.SetValue(HasShadowProperty, value); }
        }

        [TypeConverter(typeof(ImageSourceConverter))]
        public ImageSource Source
        {
            get { return (ImageSource)this.GetValue(SourceProperty); }
            set { this.SetValue(SourceProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)this.GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return (object)this.GetValue(CommandParameterProperty); }
            set { this.SetValue(CommandParameterProperty, value); }
        }

        public bool AnimateOnSelection
        {
            get { return (bool)this.GetValue(AnimateOnSelectionProperty); }
            set { this.SetValue(AnimateOnSelectionProperty, value); }
        }

        internal virtual void SendClicked()
        {
            var param = this.CommandParameter;

            if (this.Command != null && this.Command.CanExecute(param))
            {
                this.Command.Execute(param);
            }

            if (this.Clicked != null)
            {
                this.Clicked(this, EventArgs.Empty);
            }
        }

        private void InternalHandleCommand(ICommand oldValue, ICommand newValue)
        {
            // TOOD: attach to CanExecuteChanged
        }

        private static void HandleCommandChanged(BindableObject bindable, ICommand oldValue, ICommand newValue)
        {
            (bindable as FloatingActionButton).InternalHandleCommand(oldValue, newValue);
        }
    }
}