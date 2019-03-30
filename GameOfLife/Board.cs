using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GameOfLife {
  public class Board : IEnumerable {
    Form1 form;
    Cell[,] matrix;
    public readonly int Size;
    public readonly int CellSize = 20;

    /* HashSet<Cell> Living = new HashSet<Cell>();
     * Keep a set of all living cells. Only check them 
     * and their neighbours on Tick().*/

    Random r = new Random();

    public Board(int size, Form1 form) {

      this.form = form;
      Size = size;
      matrix = new Cell[size, size];
      Init();
    }

    public void Init() {
      for (int y = 0; y < Size; y++) {
        for (int x = 0; x < Size; x++) {
          var s = r.Next(0, 3) == 0 ? Cell.State.Alive : Cell.State.Dead;
          var c = new Cell(y, x, CellSize, s);
          //form.Controls.Add(c);
          matrix[y, x] = c;
        }
      }
    }

    public Cell this[int y, int x] {
      get => matrix[y, x];
      set => matrix[y, x] = value;
    }
    public Cell this[Point p] {
      get => matrix[p.Y, p.X];
      set => matrix[p.Y, p.X] = value;
    }

    public void Tick() {
      for (int i = 0; i < Size; i++) {
        for (int j = 0; j < Size; j++) {
          matrix[i, j].NextGen(this);
        }
      }
      for (int i = 0; i < Size; i++) {
        for (int j = 0; j < Size; j++) {
          matrix[i, j].Tick(this);
        }
      }
    }

    public void Toggle(int y, int x) {
      if (InBounds(y, x)) {
      matrix[y, x].Toggle();
      }
    }

    public void Clear() {
      for (int i = 0; i < Size; i++) {
        for (int j = 0; j < Size; j++) {
          matrix[i, j].Reset();
        }
      }
    }

    public void Draw(object obj, PaintEventArgs args) { //////////
      for (int i = 0; i < Size; i++) {
        for (int j = 0; j < Size; j++) {
          matrix[i, j].Draw(args.Graphics);
        }
      }
    }

    public bool InBounds(Point p) {
      return p.X >= 0 && p.X < Size && p.Y >= 0 && p.Y < Size;
    }
    public bool InBounds(int y, int x) => InBounds(new Point(x, y));

    public IEnumerator GetEnumerator() => matrix.GetEnumerator();
  }
}
