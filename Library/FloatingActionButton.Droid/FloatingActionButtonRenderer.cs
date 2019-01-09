using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Support.V4.View;
using Android.Views;
using FAB.Droid;
using FAB.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AppCompat = Xamarin.Forms.Platform.Android.AppCompat;
using Widget = Android.Support.Design.Widget;

[assembly: ExportRenderer(typeof(FloatingActionButton), typeof(FloatingActionButtonRenderer))]

namespace FAB.Droid
{
    public partial class FloatingActionButtonRenderer : AppCompat.ViewRenderer<FloatingActionButton, Widget.FloatingActionButton>
    {
        public FloatingActionButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<FloatingActionButton> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                ViewGroup.SetClipChildren(false);
                ViewGroup.SetClipToPadding(false);
                UpdateControlForSize();
            }

            if (e.NewElement != null)
                Control.Click += Fab_Click;
            else if (e.OldElement != null)
                Control.Click -= Fab_Click;
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == FloatingActionButton.SizeProperty.PropertyName)
                UpdateControlForSize();
            else if (e.PropertyName == FloatingActionButton.NormalColorProperty.PropertyName ||
                     e.PropertyName == FloatingActionButton.RippleColorProperty.PropertyName ||
                     e.PropertyName == FloatingActionButton.DisabledColorProperty.PropertyName)
                SetBackgroundColors();
            else if (e.PropertyName == FloatingActionButton.HasShadowProperty.PropertyName)
                SetHasShadow();
            else if (e.PropertyName == FloatingActionButton.SourceProperty.PropertyName)
                SetImage();
            else if (e.PropertyName == VisualElement.IsEnabledProperty.PropertyName)
                UpdateEnabled();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Control.Click -= Fab_Click;

            base.Dispose(disposing);
        }

        private void UpdateControlForSize()
        {
            var inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);

            Widget.FloatingActionButton fab = null;

            if (Element.Size == FabSize.Mini)
                fab = (Widget.FloatingActionButton)inflater.Inflate(FAB.Droid.Resource.Layout.mini_fab, null);
            else // then normal
                fab = (Widget.FloatingActionButton)inflater.Inflate(FAB.Droid.Resource.Layout.normal_fab, null);

            SetNativeControl(fab);
            UpdateStyle();
        }

        void UpdateStyle()
        {
            SetHasShadow();
            SetImage();
            UpdateEnabled();
        }

        void SetBackgroundColors()
        {
            if (Control.Enabled == false)
                ViewCompat.SetBackgroundTintList(Control, ColorStateList.ValueOf(Element.DisabledColor.ToAndroid()));
            else
                ViewCompat.SetBackgroundTintList(Control, ColorStateList.ValueOf(Element.NormalColor.ToAndroid()));

            try
            {
                Control.RippleColor = Element.RippleColor.ToAndroid();
            }
            catch (MissingMethodException)
            {
                // ignore
            }
        }

        void SetHasShadow()
        {
            try
            {
                if (Element.HasShadow && Element.IsEnabled)
                    Control.Elevation = 20f;
                else
                    Control.Elevation = 0f;
            }
            catch { }
        }

        void SetImage()
        {
            Task.Run(async () =>
            {
                var bitmap = await GetBitmapAsync(Element.Source);

                (Context as Activity).RunOnUiThread(() =>
                {
                    Control?.SetImageBitmap(bitmap);
                });
            });
        }

        void UpdateEnabled()
        {
            Control.Enabled = Element.IsEnabled;

            SetHasShadow();
            SetBackgroundColors();
        }

        async Task<Bitmap> GetBitmapAsync(ImageSource source)
        {
            var handler = GetHandler(source);
            var returnValue = (Bitmap)null;

            returnValue = await handler.LoadImageAsync(source, this.Context);

            return returnValue;
        }

        void Fab_Click(object sender, EventArgs e)
        {
            Element.SendClicked();
        }
    }
}