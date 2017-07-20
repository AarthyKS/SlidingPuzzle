using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SliddingPuzzle.Model
{
    public class Tile
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
        public Tile(string id, string value)
        {
            Id = id;
            Value = value;
        }

        #endregion
    }
}
