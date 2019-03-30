using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GameOfLife { 
  public class Game {
    Form1 form;
    Board board;
    Timer timer;
    Button pause;
    int size = 50;
    int fps = 120;

    public Game() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      form = new Form1();
      board = new Board(size, form);
      form.MouseClick += new MouseEventHandler(Click);
      form.Paint += new PaintEventHandler(board.Draw); /////

      AddButtons();


      timer = new Timer();
      timer.Tick += new EventHandler(Next);
      timer.Interval = 1000 / fps;
    }

    public void Run(){
      Application.Run(form);
    }

    private void Next(object sender, EventArgs e) {
      board.Tick();
      form.Refresh();
    }

    private void ToggleTimer(object sender, EventArgs e) {
      if (timer.Enabled) {
        timer.Stop();
        pause.Text = "Play";
      }
      else {
        timer.Start();
        pause.Text = "Pause";
      }
      form.Refresh();
    }

    private void Restart(object sender, EventArgs e){
      board.Clear();
      board.Init();
      form.Refresh();
    }

    private void Clear(object sender, EventArgs e) {
      board.Clear();
      form.Refresh();
    }

    private void Click(object sender, MouseEventArgs e) {

      int x = e.X / board.CellSize;
      int y = e.Y / board.CellSize;
      board.Toggle(y, x);
      form.Refresh();
    }

    private void AddButtons(){
      var b = new Button();
      b.Location = new Point(1020, 500);
      b.Text = "Next Gen";
      b.Click += new EventHandler(Next);
      form.Controls.Add(b);

      pause = new Button();
      pause.Location = new Point(1020, 520);
      pause.Text = "Play";
      pause.Click += new EventHandler(ToggleTimer);
      form.Controls.Add(pause);

      var b3 = new Button();
      b3.Location = new Point(1020, 540);
      b3.Text = "Restart";
      b3.Click += new EventHandler(Restart);
      form.Controls.Add(b3);

      var b4 = new Button();
      b4.Location = new Point(1020, 560);
      b4.Text = "Clear";
      b4.Click += new EventHandler(Clear);
      form.Controls.Add(b4);
    }
  }
}
