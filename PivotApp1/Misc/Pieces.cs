using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace PivotApp1.Misc
{
    public sealed class Pieces
    {
        public MobileServiceCollection<Piece, Piece> all { get; set; }
        public IMobileServiceTable<Piece> pieceTable = App.MobileService.GetTable<Piece>();
        public Piece current { get; set; }
        public User user = User.CreateObject();

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

        public Piece Selector (int option1, int option2) 
        {
            if (all.Count() > 0)
            {
                Random random = new Random();
                int number = random.Next(0, all.Where(Piece => Piece.Option1 == option1 && Piece.Option2 == option2 && Piece.User == user.Name).Count());

                return (Piece)all.Where(Piece => Piece.Option1 == option1 && Piece.Option2 == option2 && Piece.User == user.Name).ToArray()[number];
            }
            else throw new ArgumentException("No Clothes");
        }

    }
}
