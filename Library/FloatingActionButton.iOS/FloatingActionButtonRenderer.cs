using System;
using FAB.Forms;
using FAB.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FloatingActionButton), typeof(FloatingActionButtonRenderer))]

namespace FAB.iOS
{
    public partial class FloatingActionButtonRenderer : ViewRenderer<FloatingActionButton, MNFloatingActionButton>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<FloatingActionButton> e)
        {
            base.OnElementChanged(e);

            if (this.Element == null)
                return;

            if (this.Control == null)
            {
                var fab = new MNFloatingActionButton(this.Element.AnimateOnSelection);
                fab.Frame = new CoreGraphics.CGRect(0, 0, 24, 24);

                SetNativeControl(fab);

                UpdateStyles();
            }

            if (e.NewElement != null)
                Control.TouchUpInside += Fab_TouchUpInside;

            if (e.OldElement != null)
                Control.TouchUpInside -= Fab_TouchUpInside;
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == FloatingActionButton.SizeProperty.PropertyName)
                SetSize();
            else if (e.PropertyName == FloatingActionButton.NormalColorProperty.PropertyName ||
                     e.PropertyName == FloatingActionButton.RippleColorProperty.PropertyName ||
                     e.PropertyName == FloatingActionButton.DisabledColorProperty.PropertyName)
                SetBackgroundColors();
            else if (e.PropertyName == FloatingActionButton.HasShadowProperty.PropertyName)
                SetHasShadow();
            else if (e.PropertyName == FloatingActionButton.SourceProperty.PropertyName ||
                     e.PropertyName == VisualElement.WidthProperty.PropertyName ||
                     e.PropertyName == VisualElement.HeightProperty.PropertyName)
                SetImage();
            else if (e.PropertyName == VisualElement.IsEnabledProperty.PropertyName)
                UpdateEnabled();
            else if (e.PropertyName == FloatingActionButton.AnimateOnSelectionProperty.PropertyName)
                UpdateAnimateOnSelection();
        }

        public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            var viewSize = Element.Size == FabSize.Normal ? 56 : 40;

            return new SizeRequest(new Size(viewSize, viewSize));
        }

        void UpdateStyles()
        {
            SetHasShadow();
            SetImage();
            SetSize();
            UpdateEnabled();
        }

        void SetSize()
        {
            switch (Element.Size)
            {
                case FabSize.Mini:
                    Control.Size = MNFloatingActionButton.FABSize.Mini;
                    break;

                case FabSize.Normal:
                    Control.Size = MNFloatingActionButton.FABSize.Normal;
                    break;
            }
        }

        void SetBackgroundColors()
        {
            if (Control.Enabled == false)
                Control.BackgroundColor = Element.DisabledColor.ToUIColor();
            else
                Control.BackgroundColor = Element.NormalColor.ToUIColor();
            //this.Control.PressedBackgroundColor = this.Element.Ripplecolor.ToUIColor();
        }

        void SetHasShadow()
        {
            if (Element.HasShadow && Element.IsEnabled)
                Control.HasShadow = true;
            else
                Control.HasShadow = false;
        }

        void SetImage()
        {
            SetImageAsync(Element.Source, Control);
        }

        void UpdateEnabled()
        {
            Control.Enabled = Element.IsEnabled;

            SetHasShadow();
            SetBackgroundColors();
        }

        void UpdateAnimateOnSelection()
        {
            Control.AnimateOnSelection = Element.AnimateOnSelection;
        }

        void Fab_TouchUpInside(object sender, EventArgs e)
        {
            Element.SendClicked();
        }

        async static void SetImageAsync(ImageSource source, MNFloatingActionButton targetButton)
        {
            if (source != null)
            {
                var widthRequest = targetButton.Frame.Width;
                var heightRequest = targetButton.Frame.Height;

                var handler = GetHandler(source);
                using (UIImage image = await handler.LoadImageAsync(source))
                {
                    if (image != null)
                    {
                        UIGraphics.BeginImageContextWithOptions(new CoreGraphics.CGSize(widthRequest, heightRequest), false, UIScreen.MainScreen.Scale);
                        image.Draw(new CoreGraphics.CGRect(0, 0, widthRequest, heightRequest));
                        using (var resultImage = UIGraphics.GetImageFromCurrentImageContext())
                        {
                            if (resultImage != null)
                            {
                                UIGraphics.EndImageContext();
                                using (var resizableImage = resultImage.CreateResizableImage(new UIEdgeInsets(0f, 0f, widthRequest, heightRequest)))
                                {
                                    targetButton.CenterImageView.Image = resizableImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
                                }
                            }
                        }
                    }
                    else
                        targetButton.CenterImageView.Image = null;
                }
            }
            else
                targetButton.CenterImageView.Image = null;
        }
    }
}