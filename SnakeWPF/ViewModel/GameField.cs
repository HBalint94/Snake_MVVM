using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace SnakeWPF.ViewModel
{
    public class GameField : ViewModelBase
    {
        private Int32 fieldValue;
        //mező értéke ( 0: üres , 1: kígyó , 2: falat) ( ha változik a mező értéke => változik a mező színe)
        public Int32 FieldValue { get { return fieldValue; } set { fieldValue = value; OnPropertyChanged("fieldColor"); } }
        //sorszám
        public Int32 Number { get; set; }
        public Int32 X { get; set; }
        public Int32 Y { get; set; }
        private SolidColorBrush _fieldColor;
        public SolidColorBrush fieldColor
        {
            get
            {
                Color color;
                if (FieldValue == 0)
                {
                    color = Colors.WhiteSmoke;
                }
                else if (FieldValue == 1)
                {
                    color = Colors.DarkOliveGreen;
                }
                else color = Colors.Red;

                return new SolidColorBrush(color);
            }
            set { _fieldColor = value;}
        }
       
    }
}
