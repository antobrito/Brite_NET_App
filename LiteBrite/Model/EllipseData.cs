using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
/****************************************************
	Coder:		   Antonio Brito
	Project:	   BRITO -LITE - GUI Application
	File Name:	   Class : EllipseData
*****************************************************/
namespace LiteBrite.Model
{
    public class EllipseData :INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;     

        private Brush color_;
        public Brush color {
            get { return color_; }
            set
            {
                color_ = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("color"));
            }
        }

    }
}
