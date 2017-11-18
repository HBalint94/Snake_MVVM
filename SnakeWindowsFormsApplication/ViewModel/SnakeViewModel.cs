using SnakeWindowsFormsApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWindowsFormsApplication.ViewModel
{
    /// <summary>
    /// Sudoku nézetmodell típusa.
    /// </summary>
    public class SnakeViewModel : ViewModelBase
    {
        #region Fields

        private SnakeGameModel _model; // modell

        #endregion

        #region Properties

        /// <summary>
        /// Új játék kezdése parancs lekérdezése.
        /// </summary>
        public DelegateCommand NewGameCommand { get; private set; }

        /// <summary>
        /// Játék betöltése parancs lekérdezése.
        /// </summary>
        public DelegateCommand LoadGameCommand { get; private set; }

        /// <summary>
        /// Játék mentése parancs lekérdezése.
        /// </summary>
        public DelegateCommand SaveGameCommand { get; private set; }

        /// <summary>
        /// Kilépés parancs lekérdezése.
        /// </summary>
        public DelegateCommand ExitCommand { get; private set; }

        /// <summary>
        /// Játékmező gyűjtemény lekérdezése.
        /// </summary>
        public ObservableCollection<SnakeField> Fields { get; set; }

        /// <summary>
        /// Lépések számának lekérdezése.
        /// </summary>
        public Int32 GameScore { get { return _model.GameScore; } }


        /// <summary>
        /// Könnyű nehézségi szint állapotának lekérdezése.
        /// </summary>
        public Boolean IsGameEasy
        {
            get { return _model.GameTableSize == 10; }
            set
            {
                if (_model.GameTableSize == 10)
                    return;

                _model.GameTableSize = 10;
                OnPropertyChanged("IsGameEasy");
                OnPropertyChanged("IsGameMedium");
                OnPropertyChanged("IsGameHard");
            }
        }

        /// <summary>
        /// Közepes nehézségi szint állapotának lekérdezése.
        /// </summary>
        public Boolean IsGameMedium
        {
            get { return _model.GameTableSize == 15; }
            set
            {
                if (_model.GameTableSize == 15)
                    return;

                _model.GameTableSize = 15;
                OnPropertyChanged("IsGameEasy");
                OnPropertyChanged("IsGameMedium");
                OnPropertyChanged("IsGameHard");
            }
        }

        /// <summary>
        /// Magas nehézségi szint állapotának lekérdezése.
        /// </summary>
        public Boolean IsGameHard
        {
            get { return _model.GameTableSize == 20; }
            set
            {
                if (_model.GameTableSize == 15)
                    return;

                _model.GameTableSize = 15;
                OnPropertyChanged("IsGameEasy");
                OnPropertyChanged("IsGameMedium");
                OnPropertyChanged("IsGameHard");
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Új játék eseménye.
        /// </summary>
        public event EventHandler NewGame;

        /// <summary>
        /// Játék betöltésének eseménye.
        /// </summary>
        public event EventHandler LoadGame;

        /// <summary>
        /// Játék mentésének eseménye.
        /// </summary>
        public event EventHandler SaveGame;

        /// <summary>
        /// Játékból való kilépés eseménye.
        /// </summary>
        public event EventHandler ExitGame;

        #endregion

        #region Constructors

        /// <summary>
        /// Sudoku nézetmodell példányosítása.
        /// </summary>
        /// <param name="model">A modell típusa.</param>
        public SnakeViewModel(SnakeGameModel model)
        {
            // játék csatlakoztatása
            _model = model;
            _model.SnakeMoved += new EventHandler<SnakeEventArgs>(Model_SnakeMoved);
            _model.GameOver += new EventHandler<SnakeEventArgs>(Model_GameOver);
            _model.OnMoveChange += RefreshTable;

            // parancsok kezelése
            NewGameCommand = new DelegateCommand(param => { OnNewGame(); RefreshTable(); });
            LoadGameCommand = new DelegateCommand(param => { OnLoadGame(); RefreshTable(); });
            SaveGameCommand = new DelegateCommand(param => OnSaveGame());
            ExitCommand = new DelegateCommand(param => OnExitGame());

            // játéktábla létrehozása
            Fields = new ObservableCollection<SnakeField>();
            for (Int32 i = 0; i < _model.GameTableSize; i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < _model.GameTableSize; j++)
                {
                    if (_model.Table.GetValue(i,j) == 1)
                    {
                        Fields.Add(new SnakeField
                        {
                            Colour = "GREEN",
                            X = i,
                            Y = j,
                            Number = i * _model.GameTableSize + j,
                            StepCommand = new DelegateCommand(param => StepGame(Convert.ToInt32(param)))
                        });
                    }
                    else if(_model.Table.GetValue(i,j) == 0)
                    {
                           Fields.Add(new SnakeField
                        {
                            Colour = "WHITESMOKE",
                            X = i,
                            Y = j,
                            Number = i * _model.Table.Size + j, // a gomb sorszáma, amelyet felhasználunk az azonosításhoz
                            StepCommand = new DelegateCommand(param => StepGame(Convert.ToInt32(param)))
                            // ha egy mezőre jött a kígyó változtatjuk a 
                        });
                    }
                    else if(_model.Table.GetValue(i,j) == 2)
                    {
                        Fields.Add(new SnakeField
                        {
                            Colour = "RED",
                            X = i,
                            Y = j,
                            Number = i * _model.Table.Size + j, // a gomb sorszáma, amelyet felhasználunk az azonosításhoz
                            StepCommand = new DelegateCommand(param => StepGame(Convert.ToInt32(param)))
                            // ha egy mezőre jött a kígyó változtatjuk a 
                        });
                    }
                }
            }

            RefreshTable();
        }

        private void Model_SnakeMoved(object sender, SnakeEventArgs e)
        {
           /* if (e.isEat)
            {
                Fields[e.headPosY*e.headPosX + e.headPosX].Colour= "GREEN";
            }
            else
            {
                Fields[e.tailPosY * e.tailPosX + e.tailPosX].Colour = "WHITESMOKE";
                Fields[e.headPosY * e.headPosX + e.headPosX].Colour = "GREEN";
            }*/
        }
        private void Model_GameOver(object sender, SnakeEventArgs e)
        {
            _model.GameTimer.Stop();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Tábla frissítése.
        /// </summary>
        private void RefreshTable()
        {
            foreach (SnakeField field in Fields) // inicializálni kell a mezőket is
            {
                if (_model.Table.GetValue(field.X, field.Y) == 1) field.Colour = "GREEN";
                if (_model.Table.GetValue(field.X, field.Y) == 0) field.Colour = "WHITESMOKE";
                if (_model.Table.GetValue(field.X, field.Y) == 2) field.Colour = "RED";
            }
           
        }

        /// <summary>
        /// Játék léptetése eseménykiváltása.
        /// </summary>
        /// <param name="index">A lépett mező indexe.</param>
        private void StepGame(Int32 index)
        {
            SnakeField field = Fields[index];

            _model.(field.X, field.Y);

            field.Text = _model.Table[field.X, field.Y] > 0 ? _model.Table[field.X, field.Y].ToString() : String.Empty; // visszaírjuk a szöveget
            OnPropertyChanged("GameStepCount"); // jelezzük a lépésszám változást

            field.Text = !_model.Table.IsEmpty(field.X, field.Y) ? _model.Table[field.X, field.Y].ToString() : String.Empty;
        }

        #endregion

        #region Game event handlers

        /// <summary>
        /// Játék végének eseménykezelője.
        /// </summary>
        private void Model_GameOver(object sender, SudokuEventArgs e)
        {
            foreach (SudokuField field in Fields)
            {
                field.IsLocked = true; // minden mezőt lezárunk
            }
        }

        /// <summary>
        /// Játék előrehaladásának eseménykezelője.
        /// </summary>
        private void Model_GameAdvanced(object sender, SudokuEventArgs e)
        {
            OnPropertyChanged("GameTime");
        }

        #endregion

        #region Event methods

        /// <summary>
        /// Új játék indításának eseménykiváltása.
        /// </summary>
        private void OnNewGame()
        {
            if (NewGame != null)
                NewGame(this, EventArgs.Empty);
        }



        /// <summary>
        /// Játék betöltése eseménykiváltása.
        /// </summary>
        private void OnLoadGame()
        {
            if (LoadGame != null)
                LoadGame(this, EventArgs.Empty);
        }

        /// <summary>
        /// Játék mentése eseménykiváltása.
        /// </summary>
        private void OnSaveGame()
        {
            if (SaveGame != null)
                SaveGame(this, EventArgs.Empty);
        }

        /// <summary>
        /// Játékból való kilépés eseménykiváltása.
        /// </summary>
        private void OnExitGame()
        {
            if (ExitGame != null)
                ExitGame(this, EventArgs.Empty);
        }



        #endregion
    }
}
