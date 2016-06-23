using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisfitThemes.Presentation
{
    /// <summary>
    /// Represents a displayable link.
    /// </summary>
    public class Link
        : Displayable
    {
        private Uri source;

        public Link()
        {

        }

        public Link(string displayName, string source)
        {
            this.DisplayName = displayName;
            this.Source = new Uri(source, UriKind.Relative);
        }

        /// <summary>
        /// Gets or sets the source uri.
        /// </summary>
        /// <value>The source.</value>
        public Uri Source
        {
            get { return this.source; }
            set
            {
                if (this.source != value)
                {
                    this.source = value;
                    OnPropertyChanged("Source");
                }
            }
        }
    }
}
