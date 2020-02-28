using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_IssueBlurringTextInViewBox
{
    public class MainViewModel : ViewModelBase
    {
        private string _name_A;
        public string Name_A
        {
            get { return _name_A; }
            set { Set(ref _name_A, value); }
        }

        private string _name_B;
        public string Name_B
        {
            get { return _name_B; }
            set { Set(ref _name_B, value); }
        }
    }
}
