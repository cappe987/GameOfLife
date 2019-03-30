using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GameOfLife{
    public class Cell /*: Button*/{
        State state;
        int X;
        int Y;
        SolidBrush br; /////////
        int Size;

        public Cell(int y, int x, int size, State s){
            X = x;
            Y = y;

            Size = size;
            //Location = new Point(x * size, y * size);
            //Width = size;
            //Height = size;
            state = s;
        }

        public enum State{
            Alive, Dead, Dying, Spawning
        }

        enum Neighbour{
            Up, Down, Left, Right, UpRight, UpLeft, DownRight, DownLeft
        }

        //public override Color BackColor {
        //    get {
        //        if(state == State.Alive || state == State.Dying){
        //            return Color.Black;
        //        }
        //        else{
        //            return Color.White;
        //        }
        //    }
        //    set => base.BackColor = value;
        //}

        //protected override void OnClick(EventArgs e){
        //    //base.OnClick(e);
        //    if(state == State.Alive){
        //        state = State.Dead;
        //    }
        //    else if(state == State.Dead){
        //        state = State.Alive;
        //    }
        //}

        public void NextGen(Board b){
            int count = 0;
            var ns = Enum.GetValues(typeof(Neighbour));
            foreach(Neighbour n in ns){
                var p = GetNeighbour(n);
                if(InBounds(p, b) && (b[p].state == State.Alive || b[p].state == State.Dying)){
                    count++;
                }
            }
            if (count == 3)
            {
                if (state == State.Dead)
                {
                    state = State.Spawning;
                }
            }
            else if (count != 2){
                if(state == State.Alive) {
                    state = State.Dying;
                }
            }
        }

        public void Tick(Board b){
            foreach (Cell c in b) {
                if (c.state == State.Spawning){
                    c.state = State.Alive;
                }
                else if(c.state == State.Dying){
                    c.state = State.Dead;
                }
            }
        }

        private Point GetNeighbour(Neighbour n){
            switch (n){
                case Neighbour.Up:          return new Point(X    , Y - 1);
                case Neighbour.Down:        return new Point(X    , Y + 1);
                case Neighbour.Left:        return new Point(X - 1, Y    );
                case Neighbour.Right:       return new Point(X + 1, Y    );
                case Neighbour.UpRight:     return new Point(X + 1, Y - 1);
                case Neighbour.UpLeft:      return new Point(X - 1, Y - 1);
                case Neighbour.DownRight:   return new Point(X + 1, Y + 1);
                case Neighbour.DownLeft:    return new Point(X - 1, Y + 1);
            }
            return new Point(0, 0);
        }

        private bool InBounds(Point p, Board b){
            return p.X >= 0 && p.X < b.Size && p.Y >= 0 && p.Y < b.Size;
        }
        private bool InBounds(int y, int x, Board b) => InBounds(new Point(x, y), b);



        public void Draw(Graphics g) /////////
        {
            if(state == State.Alive || state == State.Dying)
            {
                br = new SolidBrush(Color.Black);
            }
            else
            {
                br = new SolidBrush(Color.White);
            }
            g.FillRectangle(br, X * Size, Y * Size, Size, Size);
        }
    }
}
