using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeWindowsFormsApplication.Persistence;
using System.Timers;

namespace SnakeWindowsFormsApplication.Model
{


    /// <summary>
    /// Snake játék típusa.
    /// </summary>
    public class SnakeGameModel
    {
        #region Difficulty constants


        private const Int32 SizeSmall = 11;
        private const Int32 SizeMedium = 15;
        private const Int32 SizeLarge = 19;

        #endregion

        #region Fields

        private ISnakeDataAccess _dataAccess; // adatelérés
        private SnakeGameTable _table; // játéktábla
        private Int32 _gameTableSize;
        private Int32 _gameScore; // lépések száma
        private Timer _gameTimer;
        private Boolean _gameOver;
        private Boolean _gamePaused;
        private Snake _snake;

        #endregion

        #region Properties

        /// <summary>
        /// Lépések számának lekérdezése.
        /// </summary>
        public Int32 GameScore { get { return _gameScore; } }
        
        public Int32 GameTime { get; set; }

        /// <summary>
        /// Játéktábla lekérdezése.
        /// </summary>
        public SnakeGameTable Table { get { return _table; } }

        public Timer GameTimer { get { return _gameTimer; } }

        /// <summary>
        /// Játék végének lekérdezése.
        /// </summary>
        public Boolean IsGameOver { get { return (_gameOver); } }

        public Snake Snake { get { return _snake; } }

        /// <summary>
        /// Játék pálya lekérdezése vagy beállítása
        /// </summary>
        public Int32 GameTableSize { get { return _gameTableSize; } set { _gameTableSize = value; } }

        public Boolean isGamePaused { get { return _gamePaused; } set { _gamePaused = value; } } 
        #endregion

        #region Events

        /// <summary>
        /// A kigyo mozgásának eseménye
        /// </summary>
        public event EventHandler<SnakeEventArgs> SnakeMoved;

        /// <summary>
        /// Játék végének eseménye.
        /// </summary>
        public event EventHandler<SnakeEventArgs> GameOver;

        #endregion

        #region Constructor

        /// <summary>
        /// Sudoku játék példányosítása.
        /// </summary>
        /// <param name="dataAccess">Az adatelérés.</param>
        public SnakeGameModel(ISnakeDataAccess dataAccess, Int32 size)
        {
            _dataAccess = dataAccess;
            _table = new SnakeGameTable(size);
            _gameTimer = new Timer();
            NewGame(size);
            _gameTimer.Elapsed += new ElapsedEventHandler(doMove);
        }

        private void doMove(object sender, ElapsedEventArgs e)
        {   
            move();
        }
        #endregion

        #region Public game methods

        /// <summary>
        /// Új játék kezdése.
        /// </summary>
        public void NewGame(Int32 size)
        {
            _snake = new Snake((size / 2) + 1, (size / 2) + 1);
            _gameScore = 0; // pontok 0
            _gameTableSize = size; // pályaméret beállítása
             GenerateFields(size); // generáljuk a mezőket
            _gameTimer.Start(); // Timer indítása
            _gamePaused = false;
            _gameOver = false;
           
        }


        /// <summary>
        /// Beállítjuk a kígyó írányát
        /// </summary>
        
        public void setDirection(Int32 direction)
        {
            if (_gamePaused || _gameOver)
            {
                return;
            }

            /*
            * 0->lefelé
            * 1->balra
            * 2->felfelé
            * 3->jobbra
            * */
                switch (direction)
                {
                case 0: _snake.getHead()._directionX = 1; _snake.getHead()._directionY = 0; break;
                case 1: _snake.getHead()._directionX = 0; _snake.getHead()._directionY = -1; break;
                case 2: _snake.getHead()._directionX = -1; _snake.getHead()._directionY = 0; break;
                case 3: _snake.getHead()._directionX = 0; _snake.getHead()._directionY = 1; break;
            }
           
        }


        /// <summary>
        /// Játék betöltése.
        /// </summary>
        /// <param name="path">Elérési útvonal.</param>
        public async Task LoadGameAsync(String path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            _table = await _dataAccess.LoadAsync(path);
            _gameScore = _table.Score;
            _gameTimer.Start();
            _gamePaused = false;
            _gameTableSize = _table.Size;

        }

        /// <summary>
        /// Játék mentése.
        /// </summary>
        /// <param name="path">Elérési útvonal.</param>
        public async Task SaveGameAsync(String path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            await _dataAccess.SaveAsync(path, _table,_snake);
        }


        #endregion

        #region Pirvate methods

        private void move()
        {
            if (_gamePaused || _gameOver)
            {
                return;
            }

            Int32 newX = _snake.getHead()._directionX + _snake.getHead()._posX;
            Int32 newY = _snake.getHead()._directionY + _snake.getHead()._posY;

            if (ValidStep(newX,newY))
            {
                if (_table.GetValue(newX,newY) == 2) // ha kajába ütköztem, akkor a farok az marad ami volt
                {
                    _table.SetValue(newX, newY, 1); // a fejet mozgatom egyel a megfelelő irányba
                    _snake.AddElementToSnake(new SnakeBodyPart(newX, newY));
                    _gameScore++;

                    SnakeMoved(this, new SnakeEventArgs(_gameScore, false, _snake.getHead()._posX, _snake.getHead()._posY, _snake.getLast()._posX,
                        _snake.getLast()._posY, true));

                }
                else  // ha nem kajába ütköztem, akkor leveszem a farkat.
                {

                    _table.SetValue(newX, newY, 1); // a fejet mozgatom egyel a megfelelő irányba
                    _table.SetValue(_snake.getLast()._posX, _snake.getLast()._posY, 0); // s a faroknak pedig új helye lesz.
                    _snake.AddElementToSnake(new SnakeBodyPart(newX, newY)); // ToDo: directiont beállítani.
                    Int32 oldPosXofTail = _snake.getLast()._posX; // ez a két kordináta azért kell, hogy tudjam léptetni a kígyót a felületen
                    Int32 oldPosYofTail = _snake.getLast()._posY;
                    _snake.StepTheTailOfTheSnake();
                    if (SnakeMoved != null)
                    {
                        SnakeMoved.Invoke(this, new SnakeEventArgs(_gameScore, false, _snake.getHead()._posX, _snake.getHead()._posY, oldPosXofTail,
                            oldPosYofTail, false));
                    }
                }

            }
            else
            {
                GameOver(this, new SnakeEventArgs(_gameScore, true, _snake.getHead()._posX, _snake.getHead()._posY, _snake.getLast()._posX,
                        _snake.getLast()._posY, false));
                _gameTimer.Stop();
            }

        }
        // Ha nem fal vagy nem maga a kigyónak egy része az új mező akkor valid a lépés.
        private Boolean ValidStep(Int32 x,Int32 y)
        {
            if ((x >= 0 && x < _gameTableSize && _gameTableSize > y && y >= 0) || _table.GetValue(x,y) != 1)
            {
                return true;
            }
            return false;
        }

        private void GenerateFields(Int32 size)
        {
            for (Int32 i = 0; i < size; ++i)
            {
                for (Int32 j = 0; j < size; ++j)
                {
                    if (i == (size / 2) + 1 && j == (size / 2) + 1)
                    {
                        _table.SetValue(i, j, 1);
                    }
                    else
                    {
                        _table.SetValue(i, j, 0);

                    }
                }
            }
        }


        #endregion

    }
}
