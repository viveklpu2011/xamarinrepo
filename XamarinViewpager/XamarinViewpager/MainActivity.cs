using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Support.Design.Widget;

using V4Fragment = Android.Support.V4.App.Fragment;
using V4FragmentManager = Android.Support.V4.App.FragmentManager;
using System.Collections.Generic;
using XamarinViewpager.Fragments;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;


namespace XamarinViewpager
{
    [Android.Runtime.Preserve(AllMembers = true)]
    [Activity(Label = "XamarinViewpager", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.DesignDemo")]
    public class MainActivity : AppCompatActivity
    {

        ViewPager viewpager;
        private int[] imageResId = {
        Resource.Drawable.icon1,
       Resource.Drawable.icon2,
        Resource.Drawable.icon3
};
        protected override void OnCreate(Bundle bundle)
        {
            Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            viewpager = FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.viewpager);

            if (viewpager.Adapter == null)
            {
                setupViewPager(viewpager);


            }
            else
            {
                viewpager.Adapter.NotifyDataSetChanged();
            }

           

            var tabLayout = FindViewById<TabLayout>(Resource.Id.tabs);

            tabLayout.SetupWithViewPager(viewpager);
            for (int i = 0; i < tabLayout.TabCount; i++)
            {
                tabLayout.GetTabAt(i).SetIcon(this.GetDrawable(imageResId[i]));
            }
        }
        void setupViewPager(Android.Support.V4.View.ViewPager viewPager)
        {
            var adapter = new Adapter(SupportFragmentManager);
            adapter.AddFragment(new TabFragment1(), "");
            adapter.AddFragment(new TabFragment2(), "");
            adapter.AddFragment(new TabFragment3(), "");
            viewPager.Adapter = adapter;
            viewpager.Adapter.NotifyDataSetChanged();


        }

    }
    class Adapter : Android.Support.V4.App.FragmentPagerAdapter
    {
        List<V4Fragment> fragments = new List<V4Fragment>();
        List<string> fragmentTitles = new List<string>();


        public Adapter(V4FragmentManager fm) : base(fm)
        {
        }

        public void AddFragment(V4Fragment fragment, String title)
        {
            fragments.Add(fragment);
            fragmentTitles.Add(title);


        }

        public override V4Fragment GetItem(int position)
        {
            return fragments[position];

        }

        public override int Count
        {
            get { return fragments.Count; }
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(fragmentTitles[position]);
        }


    }

}

