using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GameOfLife
{

    public class Game{
        Form1 form;
        Board board;
        Timer timer;
        public Game(){
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form1();
            board = new Board(20, form);

            var b = new Button();
            b.Location = new Point(500, 500);
            b.Text = "Next Gen";
            b.Click += new EventHandler(Next);
            form.Controls.Add(b);

            timer = new Timer();
            timer.Tick += new EventHandler(Next);
            timer.Interval = 1000 /30;
        }

        public void Run(){
            timer.Start();
            Application.Run(form);
        }



        private void Next(object sender, EventArgs e){
            board.Tick();
            form.Refresh();
        }
    }
}
