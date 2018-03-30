using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.ComponentModel;

namespace CulturesWebForms
{
    public class Model : INotifyPropertyChanged
    {
        public Model()
        {
            Cultures = CultureInfo.GetCultures(CultureTypes.UserCustomCulture | CultureTypes.SpecificCultures);
        }

        private CultureInfo _CurrentCulture;

        public CultureInfo CurrentCulture
        {
            get
            {
                return _CurrentCulture;
            }
            set
            {
                _CurrentCulture = value;
                OnPropertyChanged("Date");
                OnPropertyChanged("Size");
            }
        }

        public IEnumerable<CultureInfo> Cultures { get; set; }
        public string Date
        {
            get
            {
                if (CurrentCulture == null)
                    return null;
                return DateTime.Now.ToString("F", CurrentCulture);
            }
        }
        public string Size
        {
            get
            {
                if (CurrentCulture == null)
                    return null;
                DriveInfo di = new DriveInfo(@"C:\");
                return ((double)di.AvailableFreeSpace / 1024.0 / 1024.0).ToString(CurrentCulture);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
