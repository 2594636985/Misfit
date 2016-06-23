using MisfitThemes.Controls;
using MisfitThemes.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace MisfitThemes
{
    /// <summary>
    /// 调动者
    /// </summary>
    public abstract class Bootstrapper
    {
        private MisfitApplication _misfitApplication;

        public ModernWindow ModularWindow { private set; get; }

        public LinkGroupCollection MenuLinkGroup { private set; get; }

        public LinkCollection TitleLinks { private set; get; }

        /// <summary>
        /// 窗体标题
        /// </summary>
        public abstract string Titile { get; }

        /// <summary>
        /// 窗本LOGO
        /// </summary>
        public abstract Geometry LogoData { get; }




        public Bootstrapper(MisfitApplication misfitApplication)
        {
            this._misfitApplication = misfitApplication;
            this.MenuLinkGroup = new LinkGroupCollection();
        }

        public virtual void Run()
        {
            this.TitleLinks = this.InitializeTitleLinks();
            this.MenuLinkGroup = this.InitializeMenuLinkGroup();
            this.ModularWindow = this.InitializeModularWindow();

            this.ModularWindow.Title = this.Titile;
            this.ModularWindow.LogoData = this.LogoData;

            if (this.ModularWindow == null)
                throw new NullReferenceException(MisfitThemes.Resources.NoMainWindowException);

            if (this.TitleLinks != null && this.TitleLinks.Count > 0)
                this.ModularWindow.TitleLinks = this.TitleLinks;

            if (this.MenuLinkGroup != null && this.MenuLinkGroup.Count > 0)
                this.ModularWindow.MenuLinkGroups = this.MenuLinkGroup;

            this._misfitApplication.MainWindow = this.ModularWindow;
            this._misfitApplication.MainWindow.Show();
        }

        protected virtual LinkCollection InitializeTitleLinks()
        {
            return null;
        }


        protected virtual LinkGroupCollection InitializeMenuLinkGroup()
        {
            return null;
        }

        protected virtual ModernWindow InitializeModularWindow()
        {
            return null;
        }

    }
}
