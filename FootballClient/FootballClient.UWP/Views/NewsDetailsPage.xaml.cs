using AppStudio.Uwp.Controls;
using FootballClient.Models;
using FootballClient.UWP.ViewModels;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Composition;
using Prism.Windows.Mvvm;
using SamplesCommon;
using SamplesCommon.ImageLoader;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.DirectX;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;

namespace FootballClient.UWP.Views
{
    public sealed partial class NewsDetailsPage : SessionStateAwarePage
    {
        private Compositor _compositor;


        public NewsDetailsPage()
        {
            this.InitializeComponent();
            Loaded += NewsDetailsPage_Loaded;
            SizeChanged += NewsDetailsPage_SizeChanged;
            ParallaxingImage.SizeChanged += ParallaxingImage_SizeChanged;
        }

        private void ParallaxingImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            imageSpaceImitationGrid.Height = ParallaxingImage.ActualHeight;
        }

        private void NewsDetailsPage_SizeChanged(object sender, RoutedEventArgs e)
        {
            contentOverlayColumn.Width = ParallaxingImage.ActualWidth;
            UpdateScalars();
        }

        private async void NewsDetailsPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NewsDetailsPage_SizeChanged(null, null);
            _compositor = ElementCompositionPreview.GetElementVisual(sender as UIElement).Compositor;

            var scrollProperties = ElementCompositionPreview.GetScrollViewerManipulationPropertySet(scrollViewer);
            ParallaxingImage.Brush = await InitializeCrossFadeEffect(ParallaxingImage.Source);

            var _backVisual = _compositor.CreateSpriteVisual();

            ElementCompositionPreview.SetElementChildVisual(ContainerGrid, _backVisual);
            _backVisual.Scale = new Vector3(_finalScaleAmount, _finalScaleAmount, 1);

            _backgroundVisual = ElementCompositionPreview.GetElementVisual(ParallaxingImage);
            _backgroundTranslateAnimation = _compositor.CreateExpressionAnimation("Min(scrollingProperties.Translation.Y*scaleeOffset, 0)");
            _backgroundTranslateAnimation.SetScalarParameter("scaleeOffset", 0.5f);
            _backgroundTranslateAnimation.SetReferenceParameter("scrollingProperties", scrollProperties);

            _backgroundScaleAnimation = _compositor.CreateExpressionAnimation(
                "Lerp(" +
                        "1," +
                        "1+Amount," +
                        "Clamp(scrollingProperties.Translation.Y/someParam,0,1)" +
                    ")");


            _backgroundScaleAnimation.SetReferenceParameter("scrollingProperties", scrollProperties);

            _backgroundBlurAnimation = _compositor.CreateExpressionAnimation(
                "Clamp(-scrollingProperties.Translation.Y / (BackgroundPeekSize),0,1)");
            _backgroundBlurAnimation.SetScalarParameter("Amount", _backgroundScaleAmount);
            _backgroundBlurAnimation.SetReferenceParameter("scrollingProperties", scrollProperties);

            _backgroundInverseBlurAnimation = _compositor.CreateExpressionAnimation(
                "1-Clamp(-scrollingProperties.Translation.Y / (BackgroundPeekSize),0,1)");
            _backgroundInverseBlurAnimation.SetScalarParameter("Amount", _backgroundScaleAmount);
            _backgroundInverseBlurAnimation.SetReferenceParameter("scrollingProperties", scrollProperties);

            _isLoaded = true;

            UpdateScalars();
        }

        private void UpdateScalars()
        {

            if (!_isLoaded)
                return;

            ////
            var backgroundImageSize = new Vector2((float)ParallaxingImage.ActualWidth, (float)ParallaxingImage.ActualHeight);
            var crossoverTranslation = (_finalScaleAmount / 2 + _followMargin) / (1 - _parallaxRatio);
            var offset = new Vector3((float)ParallaxingImage.ActualWidth / 2, (float)ParallaxingImage.ActualHeight, 0);
            var backgroundPeekSize = backgroundImageSize.Y * _backgroundShowRatio;

            _backgroundVisual.Size = backgroundImageSize;
            _backgroundVisual.CenterPoint = new Vector3(backgroundImageSize.X / 2, 0, 1);

            _backgroundScaleAnimation.SetScalarParameter("someParam", (float)(this.ActualHeight / 2));
            _backgroundScaleAnimation.SetScalarParameter("Amount", (float)(this.ActualHeight / this.ParallaxingImage.ActualHeight));


            _backgroundVisual.StartAnimation("Scale.X", _backgroundScaleAnimation);
            _backgroundVisual.StartAnimation("Scale.Y", _backgroundScaleAnimation);
            _backgroundVisual.StartAnimation("Offset.Y", _backgroundTranslateAnimation);

            _backgroundBlurAnimation.SetScalarParameter("BackgroundPeekSize", backgroundPeekSize);
            _backgroundInverseBlurAnimation.SetScalarParameter("BackgroundPeekSize", backgroundPeekSize);
            ParallaxingImage.Brush.StartAnimation("Arithmetic.Source1Amount", _backgroundInverseBlurAnimation);
            ParallaxingImage.Brush.StartAnimation("Arithmetic.Source2Amount", _backgroundBlurAnimation);
        }

        private async Task<CompositionEffectBrush> InitializeCrossFadeEffect(Uri uri)
        {
            var graphicsEffect = new ArithmeticCompositeEffect
            {
                Name = "Arithmetic",
                Source1 = new CompositionEffectSourceParameter("ImageSource"),
                Source1Amount = 1,
                Source2 = new CompositionEffectSourceParameter("BlurImage"),
                Source2Amount = 0,
                MultiplyAmount = 0
            };

            var factory = _compositor.CreateEffectFactory(graphicsEffect, new[] { "Arithmetic.Source1Amount", "Arithmetic.Source2Amount" });

            CompositionDrawingSurface blurSurface = await SurfaceLoader.LoadFromUri(uri, Size.Empty, ApplyBlurEffect);
            CompositionEffectBrush crossFadeBrush = factory.CreateBrush(); ;
            crossFadeBrush.SetSourceParameter("ImageSource", ParallaxingImage.SurfaceBrush);
            crossFadeBrush.SetSourceParameter("BlurImage", _compositor.CreateSurfaceBrush(blurSurface));

            return crossFadeBrush;
        }

        CompositionDrawingSurface ApplyBlurEffect(CanvasBitmap bitmap, Windows.UI.Composition.CompositionGraphicsDevice device, Size sizeTarget)
        {
            GaussianBlurEffect blurEffect = new GaussianBlurEffect()
            {
                Source = bitmap,
                BlurAmount = 20.0f,
                BorderMode = EffectBorderMode.Hard,
            };

            float fDownsample = .3f;
            Size sizeSource = bitmap.Size;
            if (sizeTarget == Size.Empty)
            {
                sizeTarget = sizeSource;
            }

            sizeTarget = new Size(sizeTarget.Width * fDownsample, sizeTarget.Height * fDownsample);
            CompositionDrawingSurface blurSurface = device.CreateDrawingSurface(sizeTarget, DirectXPixelFormat.B8G8R8A8UIntNormalized, DirectXAlphaMode.Premultiplied);
            using (var ds = CanvasComposition.CreateDrawingSession(blurSurface))
            {
                Rect destination = new Rect(0, 0, sizeTarget.Width, sizeTarget.Height);
                ds.Clear(Windows.UI.Color.FromArgb(255, 255, 255, 255));
                ds.DrawImage(blurEffect, destination, new Rect(0, 0, sizeSource.Width, sizeSource.Height));
            }

            return blurSurface;
        }

        private float _finalScaleAmount = .4f;
        private float _followMargin = 20f;
        private float _backgroundShowRatio = .7f;
        private float _backgroundScaleAmount = 1.5f;
        private float _parallaxRatio = .2f;
        private Visual _backgroundVisual;
        private ExpressionAnimation _backgroundScaleAnimation;
        private ExpressionAnimation _backgroundBlurAnimation;
        private ExpressionAnimation _backgroundInverseBlurAnimation;
        private ExpressionAnimation _backgroundTranslateAnimation;
        private bool _isLoaded;
    }
}
