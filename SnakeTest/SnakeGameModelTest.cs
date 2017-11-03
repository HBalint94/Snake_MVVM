using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakeWindowsFormsApplication.Model;

namespace SnakeTest
{
    [TestClass]
    public class SnakeGameModelTest
    {
        private SnakeGameModel _model; // a tesztelendő modell

        [TestInitialize]
        public void Initialize()
        {
            _model = new SnakeGameModel(null,15);
            //perzisztencia nélküli modellt hozunk létre

            _model.GameOver += new EventHandler<SnakeEventArgs>(Model_GameOver);
            _model.SnakeMoved += new EventHandler<SnakeEventArgs>(Model_SnakeMoved);

        }

        [TestMethod]
        public void SnakeGameModelTestNewGameEasyTest()
        {
            _model.NewGame(11);

            Assert.AreEqual(0, _model.GameScore);

            Int32 emptyFields = 0;
            for(Int32 i = 0; i< 11; i++)
            {
                for(Int32 j = 0;j<11;j++)
                {
                    if(_model.Table.GetValue(i,j) == 0)
                    {
                        emptyFields++;
                    }
                }
            }
            Assert.AreEqual(119, emptyFields);
        }

        [TestMethod]
        public void SnakeGameModelTestMoveTest()
        {
            Assert.AreEqual(0, _model.GameScore);

            _model.NewGame(11);
           
            _model.Table.SetValue(6, 5, 2);   // át állítom a kígyó mellett álló mezőt falatra.

            _model.Snake.DirectionX = 0; // beállítom, hogy a falat felé induljon a kígyó
            _model.Snake.DirectionY = -1;

            _model.move();  // léptetem a kígyót bele a falatba
            
            
          
            Assert.AreEqual(1, _model.GameScore);  // mivel megette a kígyó a falatot 1 el nő a pontszám.
                
        }

        private void Model_GameOver(Object sender, SnakeEventArgs e)
        {
            Assert.IsTrue(_model.IsGameOver);
            Assert.IsTrue(e.headPosY > _model.GameTableSize || e.headPosX > _model.GameTableSize || e.headPosX < 0 || e.headPosY < 0 || _model.Table.GetValue(e.headPosX,e.headPosY) == 1);
            
        }

        private void Model_SnakeMoved(Object sender, SnakeEventArgs e)
        {
            Assert.AreEqual(e.GameScore, _model.GameScore);
            Assert.IsFalse(e.isLose);
        }
         
    }
}

