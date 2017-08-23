﻿using System;
using Xamarin.Forms;

namespace FootballClientForms.Controls
{
    public class ParallaxScrollView : ScrollView
    {
        public ParallaxScrollView()
        {
            Scrolled += (sender, e) => Parallax();
		}

		public static readonly BindableProperty ParallaxViewProperty =
			BindableProperty.Create(nameof(ParallaxView), typeof(View), typeof(ParallaxScrollView), null);

		public View ParallaxView
		{
			get { return (View)GetValue(ParallaxViewProperty); }
			set { SetValue(ParallaxViewProperty, value); }
		}

		double height;
		public void Parallax()
		{
            if (ParallaxView == null || Device.RuntimePlatform == Device.Windows || Device.RuntimePlatform == Device.WinPhone)
				return;

			if (height <= 0)
				height = ParallaxView.Height;

			var y = -(int)((float)ScrollY / 2.5f);
			if (y < 0)
			{
				//Move the Image's Y coordinate a fraction of the ScrollView's Y position
				ParallaxView.Scale = 1;
				ParallaxView.TranslationY = y;
			}
            else if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
			{
				//Calculate a scale that equalizes the height vs scroll
				double newHeight = height + (ScrollY * -1);
                var newScale = newHeight / height;
                if (newScale < 1)
                {

                    newScale = 1;
                }
                ParallaxView.Scale = newScale;
				ParallaxView.TranslationY = -(ScrollY / 2);
			}
			else
			{
				ParallaxView.Scale = 1;
				ParallaxView.TranslationY = 0;
			}
		}
    }
}