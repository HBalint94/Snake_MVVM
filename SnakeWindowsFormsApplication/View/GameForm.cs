using SnakeWindowsFormsApplication.Model;
using SnakeWindowsFormsApplication.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeWindowsFormsApplication
{
    public partial class GameForm : Form
    {
        #region VARIABLES
        private SnakeFileDataAccess _dataAccess; 
        private SnakeGameModel _gamemodel;
        private Button[,] _buttonGrid;
        private Int32 _mapsize;
        private Boolean _gamestarted;
        #endregion

        #region CONSTRUCTOR
        public GameForm()
        {
            InitializeComponent();
            _gamestarted = false;
        }
        #endregion

        private void GameForm_Load(Object sender, EventArgs e)
        {
            _mapsize = 15;
            
        }

        private void setTheOptions()
        {
            //adatelérés példányosítása
            _dataAccess = new SnakeFileDataAccess();
            // Játék model példányosítása
            _gamemodel = new SnakeGameModel(_dataAccess, _mapsize);
            // Snake mozgásának esemény kezelőjének és a játék vége eseménykezelőjének felvétele
            _gamemodel.SnakeMoved -= new EventHandler<SnakeEventArgs>(SnakeMoved);
            _gamemodel.GameOver -= new EventHandler<SnakeEventArgs>(gameOver);
            // Generálom a táblát a kiválasztott mérettel a játék indul.
            generateTable(_mapsize);
            _gamestarted = true;
            progressLabel.Text = "A játék folyik!";
            // felveszem a megjelenített pályára a nyomógombokat és aktiválom is őket.
            gameTableBox.KeyUp += new KeyEventHandler(keyPressed);
            gameTableBox.Focus();
        }
        #region METHODS

        
        private void SnakeMoved(object sender, SnakeEventArgs e)
        {
            if (e.isEat)
            {
                _buttonGrid[e.headPosX, e.headPosY].BackColor = Color.DarkOliveGreen;
            }
            else
            {
                _buttonGrid[e.tailPosX, e.tailPosY].BackColor = DefaultBackColor;
                _buttonGrid[e.headPosX, e.headPosY].BackColor = Color.DarkOliveGreen;
            }   
            
        }
        private void gameOver(object sender, SnakeEventArgs e)
        {
            if (progressLabel.InvokeRequired)
            {
                Invoke(new EventHandler<SnakeEventArgs>(gameOver), sender, e);
                return;
            }
            progressLabel.Text = "A játék véget ért!";
            gameTableBox.KeyUp -= new KeyEventHandler(keyPressed);
            MessageBox.Show("Vége a játéknak!" +
                Environment.NewLine + "Ennyi pontja lett " + e.GameScore + MessageBoxButtons.OK + MessageBoxIcon.Information);
        }

        private void keyPressed(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                /*
                * 0->lefelé
                * 1->balra
                * 2->felfelé
                * 3->jobbra
                * */

                case Keys.W:
                    _gamemodel.setDirection(3);
                    break;
                case Keys.A:
                    _gamemodel.setDirection(2);
                    break;
                case Keys.S:
                    _gamemodel.setDirection(1);
                    break;
                case Keys.D:
                    _gamemodel.setDirection(0);
                    break;

                case Keys.P:
                    if (!_gamemodel.isGamePaused)
                    {
                        progressLabel.Text = "A játék folyik!";
                        _gamemodel.isGamePaused = false;
                    }
                    else
                    {
                        progressLabel.Text = "A játék szünetel!";
                        _gamemodel.isGamePaused = true;
                    }
                    break;
            }
        }

        private void generateTable(Int32 size)
        {
            gameTableBox.Visible = false;
            _buttonGrid = new Button[size, size];
            gameTableBox.Controls.Clear();
            gameTableBox.Size = new Size(size * 16, size * 16);

            for (Int32 i = 0; i < size; i++)
                for (Int32 j = 0; j < size; j++)
                {
                    _buttonGrid[i, j] = new Button();
                    _buttonGrid[i, j].Size = new Size(15,15);
                    _buttonGrid[i, j].Text = String.Empty;
                    _buttonGrid[i, j].Location = new Point(5 + 15 * j, 10 + 15 * i);
                    _buttonGrid[i, j].Enabled = false;
                    if (_gamemodel.Table.GetValue(i, j) == 0)
                    {
                        _buttonGrid[i, j].BackColor = Color.Black;
                    }
                    else if (_gamemodel.Table.GetValue(i,j) == 1)
                    {
                        _buttonGrid[i, j].BackColor = Color.DarkOliveGreen;
                    }
                    else if(_gamemodel.Table.GetValue(i,j) == 2)
                    {
                        _buttonGrid[i, j].BackColor = Color.Red;
                    }
                    gameTableBox.Controls.Add(_buttonGrid[i, j]);
                }
            gameTableBox.Visible = true;
        }

        private void SetupTable()
        {
            for(Int32 i = 0; i < _buttonGrid.GetLength(0); i++)
            {
                for(Int32 j = 0;j < _buttonGrid.GetLength(1); j++)
                {
                    if( _gamemodel.Table.GetValue(i,j) == 0)
                    {
                        _buttonGrid[i, j].BackColor = Color.Black;
                    }
                    else if (_gamemodel.Table.GetValue(i, j) == 1)
                    {
                        _buttonGrid[i, j].BackColor = Color.DarkOliveGreen;
                    }
                    else if (_gamemodel.Table.GetValue(i, j) == 2)
                    {
                        _buttonGrid[i, j].BackColor = Color.Red;
                    }
                }
            }
            scoreLabel.Text = _gamemodel.GameScore.ToString();
           // GameTimeLabel.Text = TimeSpan.FromSeconds(_gamemodel.GameTime).ToString("g");
        }

        private void newGameOption_Click(Object sender, EventArgs e)
        {
            setTheOptions();
            saveGameOption.Enabled = true;
            _gamemodel.NewGame(_mapsize);
            _gamemodel.GameTimer.Start();
        }

        private void exitOption_Click(Object sender, EventArgs e)
        {
            Boolean restartTimer = _gamemodel.GameTimer.Enabled;
            _gamemodel.GameTimer.Stop();

            // megkérdezzük, hogy biztos ki szeretne-e lépni
            if (MessageBox.Show("Biztosan ki szeretne lépni?", "Snake játék", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // ha igennel válaszol
                Close();
            }
            else
            {
                if (restartTimer)
                    _gamemodel.GameTimer.Start();
            }
        }

        private void smallGameTableOption_Click(Object sender, EventArgs e)
        {
            _mapsize = 10;
        }

        private void mediumGameTableOption_Click(Object sender, EventArgs e)
        {
            _mapsize= 15;
        }

        private void bigGameTableOption_Click(Object sender, EventArgs e)
        {
            _mapsize= 20;
        }

        /// <summary>
        /// Játék mentésének eseménykezelője.
        /// </summary>
        private async void saveGameOption_Click(Object sender, EventArgs e)
        {

            Boolean restartTimer = _gamemodel.GameTimer.Enabled;
            _gamemodel.GameTimer.Stop();

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // játé mentése
                    await _gamemodel.SaveGameAsync(saveFileDialog.FileName);
                }
                catch (SnakeDataException)
                {
                    MessageBox.Show("Játék mentése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a könyvtár nem írható.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (restartTimer)
                _gamemodel.GameTimer.Start();
        }
        /// <summary>
        /// Játék betöltésének eseménykezelője.
        /// </summary>
        private async void loadGameOption_Click(Object sender, EventArgs e)
        {
           
            Boolean restartTimer = _gamemodel.GameTimer.Enabled;
            _gamemodel.GameTimer.Stop();

            if (openFileDialog.ShowDialog() == DialogResult.OK) // ha kiválasztottunk egy fájlt
            {
                try
                {
                    // játék betöltése
                    await _gamemodel.LoadGameAsync(openFileDialog.FileName);
                    saveGameOption.Enabled = true;
                }
                catch (SnakeDataException)
                {
                    MessageBox.Show("Játék betöltése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a fájlformátum.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _gamemodel.NewGame(_mapsize);
                    saveGameOption.Enabled = true;
                }

                SetupTable();
            }

            if (restartTimer)
                _gamemodel.GameTimer.Start();
        }

        #endregion

      
    }
} 

