using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWindowsFormsApplication.Persistence
{

    public class SnakeBodyPart
    {
        public Int32 _posX { get; set; }
        public Int32 _posY { get; set; }

        public SnakeBodyPart(Int32 x,Int32 y)
        {
            _posX = x;
            _posY = y;
        }
    }
    public class Snake
    {
        private LinkedList<SnakeBodyPart> _snake;
        
        public Snake(Int32 x,Int32 y)
        {
            _snake = new LinkedList<SnakeBodyPart>();
            _snake.AddFirst(new SnakeBodyPart(x,y));
            DirectionX = 1;
            DirectionY = 0;

        }
        /// <summary>
        /// A fej iránya ( elég csak ezt tudnunk)
        /// </summary>
        public Int32 DirectionX{ get; set; }
        public Int32 DirectionY { get; set; }
        /// <summary>
        /// kígyó mérete
        /// </summary>
        public Int32 Size() { return _snake.Count(); }
        public SnakeBodyPart getHead() { return _snake.First(); }
        public SnakeBodyPart getLast() { return _snake.Last(); }
        public LinkedList<SnakeBodyPart> getSnake() { return _snake; }
        public void AddElementToSnake(SnakeBodyPart newElement) // ezt akkor használom ha a kigyo kajába ütközik
        {
            _snake.AddFirst(newElement);
         }
        public void StepTheTailOfTheSnake()
        {
            _snake.RemoveLast();
        }
    }
    /// <summary>
    /// Snake játéktábla típusa.
    /// </summary>
    public class SnakeGameTable
    {
        #region Fields

        private Int32[,] _fieldValues; // mezőértékek , 0:üres,1:kigyó,2:taplalek
       
        #endregion

        #region Properties

        /// <summary>
        /// Játéktábla méretének lekérdezése.
        /// </summary>
        public Int32 Size { get { return _fieldValues.GetLength(0); } set { Size = value; } }

        /// <summary>
        /// Mező értékének lekérdezése.
        /// </summary>
        /// <param name="x">Vízszintes koordináta.</param>
        /// <param name="y">Függőleges koordináta.</param>
        /// <returns>Mező értéke.</returns>
        public Int32 this[Int32 x, Int32 y] { get { return GetValue(x, y); } }

        /// <summary>
        /// Aktuális pontszám
        /// </summary>
        public Int32 Score { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Snake játéktábla példányosítása.
        /// </summary>
        public SnakeGameTable() : this(15,0) { }

        /// <summary>
        /// Snake játéktábla példányosítása.
        /// </summary>
        /// <param name="tableSize">Játéktábla mérete.</param>
        public SnakeGameTable(Int32 tableSize)
        {
            if (tableSize < 0)
                throw new ArgumentOutOfRangeException("The table size is less than 0.", "tableSize");

            _fieldValues = new Int32[tableSize, tableSize];
            Score = 0;

        }
         /// <summary>
        /// Snake játéktábla példányosítása.
        /// </summary>
        /// <param name="tableSize">Játéktábla mérete.</param>
        /// <param name="score">Aktuális pontszám</param>
        public SnakeGameTable(Int32 tableSize,Int32 score)
        {
            if (tableSize < 0 || !ValidScore(score))
                throw new ArgumentOutOfRangeException("The table size is less than 0.", "tableSize");
           
            _fieldValues = new Int32[tableSize, tableSize];
            score = Score;
        }
        #endregion

        #region Public_Methods

        /// <summary>
        /// Mező értékének lekérdezése.
        /// </summary>
        /// <param name="x">Vízszintes koordináta.</param>
        /// <param name="y">Függőleges koordináta.</param>
        /// <returns>A mező értéke.</returns>
        public Int32 GetValue(Int32 x, Int32 y)
        {
            if (x < 0 || x > Size)
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y > Size)
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldValues[x, y];
        }

        /// <summary>
        /// Mező értékének beállítása.
        /// </summary>
        /// <param name="x">Vízszintes koordináta.</param>
        /// <param name="y">Függőleges koordináta.</param>
        /// <param name="value">Érték.</param>
        public void SetValue(Int32 x, Int32 y,Int32 value)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");
            if (value < 0 || value > 2)
                throw new ArgumentOutOfRangeException("value", "The value is out of range.");
   

            _fieldValues[x, y] = value;
          
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Ellenörzöm, hogy nem negatív a kapott pontszám
        /// </summary>
        private Boolean ValidScore(Int32 score)
        {
            return score >= 0;
        }
        #endregion


    }
}
