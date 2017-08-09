using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SliddingPuzzle.Model
{
    public class Tile:INotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        /// Tile Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Tile value (numric or alphabet)
        /// </summary>
        public string Value { get; set; }

        private int x;
        /// <summary>
        /// X position in grid
        /// </summary>
        public int X
        {
            get { return x; }
            set
            {
                x = value;
                RaisePropertyChanged("X");
            }
        }

        private int y;

        /// <summary>
        /// Y position in grig
        /// </summary>
        public int Y
        {
            get { return y; }
            set
            {
                y = value;
                RaisePropertyChanged("Y");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public Tile()
        { }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public Tile(string id, string value, int x, int y)
        {
            Id = id;
            Value = value;
            X = x;
            Y = y;
        }

        #endregion

        #region Method

        /// <summary>
        /// Property changed notifier
        /// </summary>
        /// <param name="name"></param>
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
    }
}
