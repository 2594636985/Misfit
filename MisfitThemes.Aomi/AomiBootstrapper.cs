using MisfitThemes.Controls;
using MisfitThemes.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisfitThemes.Aomi
{
    public abstract class AomiBootstrapper : Bootstrapper
    {

        public AomiBootstrapper(MisfitApplication misfitApplication)
            : base(misfitApplication)
        {

        }

        public override void Run()
        {
            base.Run();
        }

        protected override LinkCollection InitializeTitleLinks()
        {
            LinkCollection linkCollection = new LinkCollection();
            linkCollection.Add(new Link(Resources.MainWindowSetting, "/Pages/Settings.xaml"));
            linkCollection.Add(new Link(Resources.MainWindowAbout, ""));
            return linkCollection;
        }


        protected override ModernWindow InitializeModularWindow()
        {
            return new AomiWindow();
        }



        //public override string Titile
        //{
        //    get { return ""; }
        //}
    }
}
