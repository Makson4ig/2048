using System;
using System.Drawing;

namespace u2048
{
    class Button
    {
        private readonly int iXPos;
        private readonly int iYPos;
        private readonly int iWidth, iHeight;
        private readonly int imgId;
        private readonly Boolean clickTable;

        public Button(int iXPos, int iYPos, int iWidth, int iHeight, int imgId, bool clickTable)
        {
            this.iXPos = iXPos;
            this.iYPos = iYPos;
            this.iWidth = iWidth;
            this.iHeight = iHeight;
            this.imgId = imgId;
            this.clickTable = clickTable;
        }
        public void Draw(Graphics g, Bitmap oB)
        {
            g.DrawImage(oB, new Point(iXPos, iYPos));
        }
        public int GetXpos()
        {
            return iXPos;
        }

        public int GetYPos()
        {
            return iYPos;
        }

        public int GetWidth()
        {
            return iWidth;
        }

        public int GetHeight()
        {
            return iHeight;
        }

        public int GetImgid()
        {
            return imgId;
        }

        public Boolean GetClickable()
        {
            return clickTable;
        }
    }
}
