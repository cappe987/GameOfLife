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
    int size = 20;

    public Game() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      form = new Form1();
      board = new Board(size, form);

      AddButtons();


      timer = new Timer();
      timer.Tick += new EventHandler(Next);
      timer.Interval = 1000 / 5;
    }

    public void Run(){
      Application.Run(form);
    }

    private void ToggleTimer(object sender, EventArgs e) { if (timer.Enabled) { timer.Stop(); } else { timer.Start(); } }

    private void Reset(object sender, EventArgs e){
      board = new Board(size, form);
      form.Refresh();
    }

    private void Next(object sender, EventArgs e) {
      board.Tick();
      form.Refresh();
    }

    private void AddButtons(){
      var b = new Button();
      b.Location = new Point(500, 500);
      b.Text = "Next Gen";
      b.Click += new EventHandler(Next);
      form.Controls.Add(b);

      var b2 = new Button();
      b2.Location = new Point(500, 520);
      b2.Text = "Play/Pause";
      b2.Click += new EventHandler(ToggleTimer);
      form.Controls.Add(b2);

      var b3 = new Button();
      b3.Location = new Point(500, 540);
      b3.Text = "Reset";
      b3.Click += new EventHandler(Reset);
      form.Controls.Add(b3);
    }
  }
}
