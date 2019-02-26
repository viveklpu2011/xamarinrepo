using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalaenderScheduler.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using CalaenderScheduler.Models;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ImageButtonnew), typeof(ImageButtonRenderer))]
namespace CalaenderScheduler.iOS
{
    public class ImageButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (e.NewElement.Image != null)
                {
                    Control.SetImage(Control.CurrentImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
                }
            }
        }
    }
}