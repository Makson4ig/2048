using System;
using System.Collections.Generic;
using System.Drawing;

namespace u2048
{
    class Game
    {
        private enum GameState
        {
            EGame,
            EAbout,
        };

        private GameState currentGameState = GameState.EGame;

        private readonly int[][] iBoard;

        private int iScore, iBest;

        private readonly List<Button> oButton = new List<Button>();
        private readonly List<Bitmap> oBitmap = new List<Bitmap>();

        private readonly Font fFontS2 = new Font("Clear Sans", 10, FontStyle.Bold);
        private readonly Font fFontS = new Font("Clear Sans", 12, FontStyle.Bold);
        private readonly Font fFont = new Font("Clear Sans", 22, FontStyle.Bold);
        private SizeF stringSize;

        private int addNum = 2;

        private readonly Random oR = new Random();

        private bool gameOver;
        private readonly Rectangle rRect;

        private int iNewX, iNewY;

        public bool KTop, KRight, KBottom, KLeft;

        public bool BRender = true;

        public enum Direction
        {
            ETop,
            ERight,
            EBottom,
            ELeft,
        };

        public Game()
        {
            iBoard = new int[4][];
            for (int i = 0; i < 4; i++)
            {
                iBoard[i] = new int[4];
            }

            oBitmap.Add(new Bitmap(@"images\1.png"));
            oBitmap.Add(new Bitmap(@"images\2.png"));
            oBitmap.Add(new Bitmap(@"images\3.png"));
            oBitmap.Add(new Bitmap(@"images\4.png"));
            oBitmap.Add(new Bitmap(@"images\5.png"));
            oBitmap.Add(new Bitmap(@"images\6.png"));
            oBitmap.Add(new Bitmap(@"images\7.png"));
            oBitmap.Add(new Bitmap(@"images\8.png"));
            oBitmap.Add(new Bitmap(@"images\9.png"));
            oBitmap.Add(new Bitmap(@"images\k0.png"));
            oBitmap.Add(new Bitmap(@"images\10.png"));
            oBitmap.Add(new Bitmap(@"images\11.png"));
            oBitmap.Add(new Bitmap(@"images\12.png"));
            oBitmap.Add(new Bitmap(@"images\13.png"));
            oBitmap.Add(new Bitmap(@"images\14.png"));
            oBitmap.Add(new Bitmap(@"images\15.png"));
            oBitmap.Add(new Bitmap(@"images\16.png"));
            oBitmap.Add(new Bitmap(@"images\17.png"));
            oBitmap.Add(new Bitmap(@"images\18.png"));

            oButton.Add(new Button(18, 18, 125, 130, 0, true));  // -- LEFT BIG

            oButton.Add(new Button(161, 18, 100, 66, 1, false)); // -- SCORE
            oButton.Add(new Button(279, 18, 100, 66, 1, false)); // -- BEST

            oButton.Add(new Button(161, 96, 100, 38, 2, true));  // -- NEW GAME

            oButton.Add(new Button(64, 103, 32, 32, 9, true)); // -- S
            oButton.Add(new Button(64, 30, 32, 32, 9, true));  // -- W
            oButton.Add(new Button(30, 67, 32, 32, 9, true));  // -- A
            oButton.Add(new Button(97, 67, 32, 32, 9, true));  // -- D

            rRect = new Rectangle(0, 0, 416, 640);
        }

        /* ******************************************** */

        public void Update()
        {
            while (!gameOver && addNum > 0)
            {
                int nX = oR.Next(0, 4), nY = oR.Next(0, 4);

                if (iBoard[nX][nY] == 0)
                {
                    iBoard[nX][nY] = oR.Next(0, 20) == 0 ? oR.Next(0, 15) == 0 ? 8 : 4 : 2;
                    iNewX = nX;
                    iNewY = nY;
                    --addNum;
                }
            }
        }

        public void Draw(Graphics g)
        {
            switch (currentGameState)
            {
                case GameState.EGame:
                    DrawGame(g);
                    if (gameOver)
                    {
                        GameOverDraw(g);
                    }

                    BRender = false;
                    break;
                case GameState.EAbout:
                    DrawGame(g);
                    BRender = false;
                    break;
            }
        }

        public void DrawGame(Graphics g)
        {
            for (var i = 0; i < oButton.Count; i++)
            {
                oButton[i].Draw(g, oBitmap[oButton[i].GetImgid()]);
            }

            DrawTextCenterXws(g, "SCORE", fFontS2, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), new SolidBrush(Color.FromArgb(235, 221, 208)), 211, 32);
            DrawTextCenterXws(g, iScore.ToString(), fFontS, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), new SolidBrush(Color.White), 211, 54);
            
            DrawTextCenterXws(g, "BEST", fFontS2, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), new SolidBrush(Color.FromArgb(235, 221, 208)), 329, 32);
            DrawTextCenterXws(g, iBest.ToString(), fFontS, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), new SolidBrush(Color.White), 329, 54);

            DrawTextCenterWs(g, "NEW GAME", fFontS2, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), new SolidBrush(Color.FromArgb(255, 241, 224)), 211, 115);
            
            DrawTextCenterWs(g, "W", fFontS2, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), KTop ? new SolidBrush(Color.FromArgb(247, 190, 48)) : new SolidBrush(Color.FromArgb(120, 110, 101)), 80, 46);
            DrawTextCenterWs(g, "S", fFontS2, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), KBottom ? new SolidBrush(Color.FromArgb(247, 190, 48)) : new SolidBrush(Color.FromArgb(120, 110, 101)), 80, 119);
            DrawTextCenterWs(g, "A", fFontS2, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), KLeft ? new SolidBrush(Color.FromArgb(247, 190, 48)) : new SolidBrush(Color.FromArgb(120, 110, 101)), 46, 83);
            DrawTextCenterWs(g, "D", fFontS2, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), KRight ? new SolidBrush(Color.FromArgb(247, 190, 48)) : new SolidBrush(Color.FromArgb(120, 110, 101)), 113, 83);

            g.DrawImage(oBitmap[3], new Point(18, 166));

            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    g.DrawImage(oBitmap[i == iNewX && j == iNewY ? 18 : GetBitmapId(iBoard[i][j])], new Point(30 + 87 * i, 178 + 87 * j));
                    if (iBoard[i][j] > 0)
                    {
                        DrawTextCenterWs(g, iBoard[i][j].ToString(), fFont, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), (i == iNewX && j == iNewY ? new SolidBrush(Color.FromArgb(255, 255, 255)) : iBoard[i][j] < 8 ? new SolidBrush(Color.FromArgb(120, 110, 101)) : new SolidBrush(Color.FromArgb(249, 245, 235))), 68 + 87 * i, 217 + 87 * j);
                    }
                }
            }
        }

        public void GameOverDraw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(150, 251, 248, 239)), rRect);

            DrawTextCenterXws(g, "GAME OVER", fFontS, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), new SolidBrush(Color.FromArgb(120, 110, 101)), 198, 250);
            DrawTextCenterXws(g, "SCORE: " + iScore.ToString(), fFontS, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), new SolidBrush(Color.FromArgb(120, 110, 101)), 198, 282);
        }

        /* ******************************************** */

        public void DrawTextCenterX(Graphics g, String sText, Font nFont, SolidBrush nSolidBrush, int x, int y)
        {
            stringSize = g.MeasureString(sText, nFont);
            g.DrawString(sText, nFont, nSolidBrush, new PointF(x - stringSize.Width / 2, y));
        }

        public void DrawTextCenterXws(Graphics g, String sText, Font nFont, SolidBrush nSolidBrush, SolidBrush nSolidBrush2, int x, int y)
        {
            stringSize = g.MeasureString(sText, nFont);
            g.DrawString(sText, nFont, nSolidBrush, new PointF(x - stringSize.Width / 2 + 1, y + 1));
            g.DrawString(sText, nFont, nSolidBrush2, new PointF(x - stringSize.Width / 2, y));
        }

        public void DrawTextCenterWs(Graphics g, String sText, Font nFont, SolidBrush nSolidBrush, SolidBrush nSolidBrush2, int x, int y)
        {
            stringSize = g.MeasureString(sText, nFont);
            g.DrawString(sText, nFont, nSolidBrush, new PointF(x - stringSize.Width / 2 + 1, y - stringSize.Height / 2 + 1));
            g.DrawString(sText, nFont, nSolidBrush2, new PointF(x - stringSize.Width / 2, y - stringSize.Height / 2));
        }

        /* ******************************************** */

        public void MoveBoard(Direction nDirection)
        {
            var bAdd = false;

            if (currentGameState == GameState.EAbout) currentGameState = GameState.EGame;

            switch (nDirection)
            {
                case Direction.ETop:
                    for (var i = 0; i < 4; i++)
                    {
                        for (var j = 0; j < 4; j++)
                        {
                            for (var k = j + 1; k < 4; k++)
                            {
                                if (iBoard[i][k] == 0)
                                {
                                }
                                else if (iBoard[i][k] == iBoard[i][j])
                                {
                                    iBoard[i][j] *= 2;
                                    iScore += iBoard[i][j];
                                    iBoard[i][k] = 0;
                                    bAdd = true;
                                    break;
                                }
                                else
                                {
                                    if (iBoard[i][j] == 0 && iBoard[i][k] != 0)
                                    {
                                        iBoard[i][j] = iBoard[i][k];
                                        iBoard[i][k] = 0;
                                        j--;
                                        bAdd = true;
                                        break;
                                    }
                                    else if(iBoard[i][j] != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Direction.ERight:
                    for (var j = 0; j < 4; j++)
                    {
                        for (var i = 3; i >= 0; i--)
                        {
                            for (var k = i - 1; k >= 0; k--)
                            {
                                if (iBoard[k][j] == 0)
                                {
                                }
                                else if (iBoard[k][j] == iBoard[i][j])
                                {
                                    iBoard[i][j] *= 2;
                                    iScore += iBoard[i][j];
                                    iBoard[k][j] = 0;
                                    bAdd = true;
                                    break;
                                }
                                else
                                {
                                    if (iBoard[i][j] == 0 && iBoard[k][j] != 0)
                                    {
                                        iBoard[i][j] = iBoard[k][j];
                                        iBoard[k][j] = 0;
                                        i++;
                                        bAdd = true;
                                        break;
                                    }
                                    else if (iBoard[i][j] != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Direction.EBottom:
                    for (var i = 0; i < 4; i++)
                    {
                        for (var j = 3; j >= 0; j--)
                        {
                            for (var k = j - 1; k >= 0; k--)
                            {
                                if (iBoard[i][k] == 0)
                                {
                                }
                                else if (iBoard[i][k] == iBoard[i][j])
                                {
                                    iBoard[i][j] *= 2;
                                    iScore += iBoard[i][j];
                                    iBoard[i][k] = 0;
                                    bAdd = true;
                                    break;
                                }
                                else
                                {
                                    if (iBoard[i][j] == 0 && iBoard[i][k] != 0)
                                    {
                                        iBoard[i][j] = iBoard[i][k];
                                        iBoard[i][k] = 0;
                                        j++;
                                        bAdd = true;
                                        break;
                                    }
                                    else if (iBoard[i][j] != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Direction.ELeft:
                    for (int j = 0; j < 4; j++)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            for (int k = i + 1; k < 4; k++)
                            {
                                if (iBoard[k][j] == 0)
                                {
                                }
                                else if (iBoard[k][j] == iBoard[i][j])
                                {
                                    iBoard[i][j] *= 2;
                                    iScore += iBoard[i][j];
                                    iBoard[k][j] = 0;
                                    bAdd = true;
                                    break;
                                }
                                else
                                {
                                    if (iBoard[i][j] == 0 && iBoard[k][j] != 0)
                                    {
                                        iBoard[i][j] = iBoard[k][j];
                                        iBoard[k][j] = 0;
                                        i--;
                                        bAdd = true;
                                        break;
                                    }
                                    else if (iBoard[i][j] != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

            if (iScore > iBest)
            {
                iBest = iScore;
            }

            if (bAdd)
            {
                ++addNum;
            }

            /* ----- GAME OVER ----- */

            CheckGameOver();
            BRender = true;
        }

        public void CheckGameOver()
        {
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (i - 1 >= 0)
                    {
                        if (iBoard[i - 1][j] == iBoard[i][j])
                        {
                            return;
                        }
                    }

                    if (i + 1 < 4)
                    {
                        if (iBoard[i + 1][j] == iBoard[i][j])
                        {
                            return;
                        }
                    }

                    if (j - 1 >= 0)
                    {
                        if (iBoard[i][j - 1] == iBoard[i][j])
                        {
                            return;
                        }
                    }

                    if (j + 1 < 4)
                    {
                        if (iBoard[i][j + 1] == iBoard[i][j])
                        {
                            return;
                        }
                    }

                    if (iBoard[i][j] == 0)
                    {
                        return;
                    }
                }
            }

            gameOver = true;
        }

        public int GetBitmapId(int iNum)
        {
            switch (iNum)
            {
                case 0:
                    return 4;
                case 2:
                    return 5;
                case 4:
                    return 6;
                case 8:
                    return 7;
                case 16:
                    return 8;
                case 32:
                    return 10;
                case 64:
                    return 11;
                case 128:
                    return 12;
                case 256:
                    return 13;
                case 512:
                    return 14;
                case 1024:
                    return 15;
                case 2048:
                    return 16;
                case 4096: case 8192: case 16384:
                    return 17;
            }

            return 4;
        }

        public void CheckButton(int nXPos, int nYPos)
        {
            for(var i = 0; i < oButton.Count; i++)
            {
                if (!oButton[i].GetClickable()) continue;
                if (nXPos >= oButton[i].GetXpos() && nXPos <= oButton[i].GetXpos() + oButton[i].GetWidth() && nYPos >= oButton[i].GetYPos() && nYPos <= oButton[i].GetYPos() + oButton[i].GetHeight())
                {
                    ActionButton(i);
                }
            }
        }

        public void ActionButton(int iButtonId)
        {
            switch (iButtonId)
            {
                case 0:
                    
                    break;
                case 3: // NEW GAME
                    ResetGameData();
                    break;
                case 4:
                    currentGameState = currentGameState == GameState.EGame ? GameState.EAbout : GameState.EGame;
                    break;
            }
            BRender = true;
        }
        private void ResetGameData()
        {
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    iBoard[i][j] = 0;
                }
            }

            addNum = 2;
            iScore = 0;
            gameOver = false;
            currentGameState = GameState.EGame;
            BRender = true;
        }
    }
}
