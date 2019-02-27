using System;
using System.Drawing;
using System.Windows.Forms;

namespace u2048
{
    public partial class Main : Form
    {
        private readonly Game oGame;
        private readonly Graphics gGraphics, gG;
        private readonly Bitmap bBackground;


        public Main()
        {
            InitializeComponent();

            bBackground = new Bitmap(396, 600);

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            gGraphics = CreateGraphics();
            gG = Graphics.FromImage(bBackground);

            oGame = new Game();
            Data.GetInstance();
        }

        public void UpdateGame()
        {
            oGame.Update();
        }
        public void Draw(Graphics g)
        {
            g.Clear(Color.FromArgb(251, 248, 239));

            oGame.Draw(g);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateGame();
            if (!oGame.BRender) return;
            Draw(gG);

            gGraphics.DrawImage(bBackground, new Point(0, 0));
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!oGame.KTop && !oGame.KRight && !oGame.KBottom && (e.KeyCode == Keys.A || e.KeyCode == Keys.Left))
            {
                oGame.KLeft = true;
                oGame.MoveBoard(Game.Direction.ELeft);
            }
            else if (!oGame.KLeft && !oGame.KRight && !oGame.KBottom && (e.KeyCode == Keys.W || e.KeyCode == Keys.Up))
            {
                oGame.KTop = true;
                oGame.MoveBoard(Game.Direction.ETop);
            }
            else if (!oGame.KTop && !oGame.KLeft && !oGame.KBottom && (e.KeyCode == Keys.D || e.KeyCode == Keys.Right))
            {
                oGame.KRight = true;
                oGame.MoveBoard(Game.Direction.ERight);
            }
            else if (!oGame.KTop && !oGame.KRight && !oGame.KLeft && (e.KeyCode == Keys.S || e.KeyCode == Keys.Down))
            {
                oGame.KBottom = true;
                oGame.MoveBoard(Game.Direction.EBottom);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (oGame.KLeft && (e.KeyCode == Keys.A || e.KeyCode == Keys.Left))
            {
                oGame.KLeft = false;
            }

            if (oGame.KTop && (e.KeyCode == Keys.W || e.KeyCode == Keys.Up))
            {
                oGame.KTop = false;
            }

            if (oGame.KRight && (e.KeyCode == Keys.D || e.KeyCode == Keys.Right))
            {
                oGame.KRight = false;
            }

            if (oGame.KBottom && (e.KeyCode == Keys.S || e.KeyCode == Keys.Down))
            {
                oGame.KBottom = false;
            }
        }

        private void Main_MouseClick(object sender, MouseEventArgs e)
        {
            oGame.CheckButton(e.X, e.Y);
        }
    }
}
