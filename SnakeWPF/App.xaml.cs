using SnakeWPF.Model;
using SnakeWPF.Persistence;

using SnakeWPF.ViewModel;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using SnakeWPF.View;

namespace SnakeWPF
{
    /// <summary>
    /// Alkalmazás típusa.
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        private Int32 _mapSize;
        private SnakeGameModel _model;
        private SnakeViewModel _viewModel;
        private MainWindow _view;
       

        #endregion

        #region Constructors

        /// <summary>
        /// Alkalmazás példányosítása.
        /// </summary>
        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        #endregion

        #region Application event handlers

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _model = new SnakeGameModel(10);
            _model.GameOver += new EventHandler<SnakeEventArgs>(Model_GameOver);
            
            // nézemodell létrehozása
            _viewModel = new SnakeViewModel(10,_model);
            _viewModel.NewGame += new EventHandler(ViewModel_NewGame);
            _viewModel.ExitGame += new EventHandler(ViewModel_ExitGame);
            _viewModel.LoadGame += new EventHandler(ViewModel_LoadGame);
            _viewModel.SaveGame += new EventHandler(ViewModel_SaveGame);
            //_model = _viewModel.GameModel;
            _mapSize = 10;

            // nézet létrehozása
            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Closing += new System.ComponentModel.CancelEventHandler(View_Closing); // eseménykezelés a bezáráshoz
            _view.Show();

        }


        #endregion
       
        #region View event handlers

        /// <summary>
        /// Nézet bezárásának eseménykezelője.
        /// </summary>
        private void View_Closing(object sender, CancelEventArgs e)
        {
        
            if (MessageBox.Show("Biztos, hogy ki akar lépni?", "Snake", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true; // töröljük a bezárást
               
            }
        }

        #endregion

        #region ViewModel event handlers

        /// <summary>
        /// Új játék indításának eseménykezelője.
        /// </summary>
        private void ViewModel_NewGame(object sender, EventArgs e)
        {
            _model.NewGame(_mapSize);

        }

        /// <summary>
        /// Játék betöltésének eseménykezelője.
        /// </summary>
        private async void ViewModel_LoadGame(object sender, System.EventArgs e)
        {
            
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog(); // dialógusablak
                openFileDialog.Title = "Sudoku tábla betöltése";
                openFileDialog.Filter = "Sudoku tábla|*.stl";
                if (openFileDialog.ShowDialog() == true)
                {
                    // játék betöltése
                    await _model.LoadGameAsync(openFileDialog.FileName);

                    _model.GameTimer.Start();
                }
            }
            catch (SnakeDataException)
            {
                MessageBox.Show("A fájl betöltése sikertelen!", "Sudoku", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          


        }

        /// <summary>
        /// Játék mentésének eseménykezelője.
        /// </summary>
        private async void ViewModel_SaveGame(object sender, EventArgs e)
        {
            Boolean restartTimer = _model.GameTimer.Enabled;
            _model.GameTimer.Stop();

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog(); // dialógablak
                saveFileDialog.Title = "Snake tábla betöltése";
                saveFileDialog.Filter = "Snake tábla|*.stl";
                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        // játéktábla mentése
                        await _model.SaveGameAsync(saveFileDialog.FileName);
                    }
                    catch (SnakeDataException)
                    {
                        MessageBox.Show("Játék mentése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a könyvtár nem írható.", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("A fájl mentése sikertelen!", "Snake", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (restartTimer)
                _model.GameTimer.Start();

        }

        /// <summary>
        /// Játékból való kilépés eseménykezelője.
        /// </summary>
        private void ViewModel_ExitGame(object sender, System.EventArgs e)
        {
            _view.Close(); // ablak bezárása
            _model.GameTimer.Stop();
        }

        #endregion

        #region Model event handlers

        /// <summary>
        /// Játék végének eseménykezelője.
        /// </summary>
        private void Model_GameOver(object sender, SnakeEventArgs e)
        {
            Boolean restartTimer = _model.GameTimer.Enabled;
            _model.GameTimer.Stop();

            if (e.isLose) // győzelemtől függő üzenet megjelenítése
            {
                MessageBox.Show("Sajnálom, vesztettél !",
                                "Snake játék",
                                MessageBoxButton.OK,
                                MessageBoxImage.Asterisk);
            }
        }

        #endregion
    }
}
