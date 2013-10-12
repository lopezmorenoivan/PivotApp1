using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PivotApp1.Misc
{
    public sealed class Pieces
    {
        public List<Piece> all { get; set; }
        public Piece current { get; set; }

        private static Pieces pieces;
        Pieces() {}

        public static Pieces CreateObject()
        {
            // If the object is null for first time instantiate it once.  
            if (pieces == null)
            {
                pieces = new Pieces();
            }

            // Return the emp object, when user request for create an instance.  
            return pieces;
        }  
    }
}
