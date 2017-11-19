using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWPF.Model
{

    /// <summary>
    /// Snake eseményargumentum típusa.
    /// </summary>
    public class SnakeEventArgs : EventArgs
    {

        private Int32 _score;
        private Boolean _isLose;
        private Int32 _posXofHead;
        private Int32 _posYofHead;
        private Int32 _posXofTail;
        private Int32 _posYofTail;
        private Boolean _isEat;

        /// <summary>
        /// getterek.
        /// </summary>
        public Int32 GameScore { get { return _score; } }
        public Boolean isLose { get { return _isLose; } }
        public Int32 headPosX { get { return _posXofHead; } }
        public Int32 headPosY { get { return _posYofHead; } }
        public Int32 tailPosX { get { return _posXofTail; } }
        public Int32 tailPosY { get { return _posXofTail; } }
        public Boolean isEat { get { return _isEat; } }

        /// <summary>
        /// Sudoku eseményargumentum példányosítása.
        /// </summary>
        /// <param name="gameScore">Pontok számának lekérdezése.</param>
        /// <param name="lose">Akadályba ütköztünk e a kigyoval.</param>
        /// <param name="xH">Kigyó fejének x kordinátája.</param>
        /// <param name="yH">Kigyó fejének y kordinátája.</param>
        /// <param name="xT">Kigyó farkának x kordinátája.</param>
        /// <param name="yT">Kigyó farkának y kordinátája.</param>
        /// <param name="eat"> Evett-e kigyó vagy csak haladt.</param>
        public SnakeEventArgs(Int32 gameScore, Boolean lose, Int32 xH, Int32 yH, Int32 xT, Int32 yT, Boolean eat)
        {
            _score = gameScore;
            _isLose = lose;
            _posXofHead = xH;
            _posYofHead = yH;
            _posXofTail = xT;
            _posYofTail = yT;
            _isEat = eat;

        }
    }
}
