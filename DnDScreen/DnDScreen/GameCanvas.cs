using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DnDScreen.WorldObjects;
using DnDScreen.WorldObjects.Grids.Cartesian;

namespace DnDScreen
{
    public partial class GameCanvas : Form
    {
        private GameWorld gameWorld = new GameWorld();

        Matrix translateMatrix = new Matrix();
        Point startCursor = new Point();
        //PointF mouseLocation = new PointF();

        //public PointF MouseLocation
        //{
        //    get { return mouseLocation; }
        //    set { mouseLocation = value; }
        //}

        Matrix scaleMatrix = new Matrix();
        PointF zoomPoint = new PointF();
        Matrix zoomTranslateMatrix = new Matrix();

        const float SCALEFACTOR = .075f;

        Random rnd = new Random(2);

        Cartesian mainGrid;

        public GameCanvas()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;

            //Move 0,0 to the center of the screen.
            translateMatrix.Translate(ClientSize.Width / 2, ClientSize.Height / 2);
            
            mainGrid = new Cartesian(this, pictureBox);
            mainGrid.EnableHighlight = true;
            gameWorld.Add(mainGrid);
            //TODO have controls in mainGrid to control whether axes are drawn or not.
            gameWorld.Add(new CartesianAxes(this));
            for (int i = 1; i < 10; i++)
            {
                Circle c = new Circle(new PointF(rnd.Next(400), rnd.Next(400)), rnd.Next(390) + 10);
                //if (i > 1) c.Visible = false;
                gameWorld.Add(c);
            }
        }

        private Matrix getWorldTransform()
        {
            Matrix worldTransformMatrix = new Matrix();
            worldTransformMatrix.Multiply(translateMatrix, MatrixOrder.Append);
            worldTransformMatrix.Multiply(scaleMatrix, MatrixOrder.Append);
            worldTransformMatrix.Multiply(zoomTranslateMatrix, MatrixOrder.Append);
            return worldTransformMatrix;
        }

        internal Graphics CreateGraphicsWithWorldTransform()
        {
            Graphics g = pictureBox.CreateGraphics();
            g.Transform = getWorldTransform();
            return g;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //GraphicsState gState = g.Save();

            g.MultiplyTransform(translateMatrix, MatrixOrder.Append);
            g.MultiplyTransform(scaleMatrix, MatrixOrder.Append);

            //Adjust the screen to seem like it is zooming from the zoom point
            PointF[] arrPoints = { zoomPoint };
            scaleMatrix.TransformPoints(arrPoints);
            PointF scaledZoomPoint = (PointF)arrPoints.GetValue(0);
            zoomTranslateMatrix.Reset();
            zoomTranslateMatrix.Translate(zoomPoint.X - scaledZoomPoint.X, zoomPoint.Y - scaledZoomPoint.Y, MatrixOrder.Append);
            g.MultiplyTransform(zoomTranslateMatrix, MatrixOrder.Append);

            gameWorld.Draw(g);
            //g.Restore(gState);
        }

        private void canvas_MouseWheel(object sender, MouseEventArgs e)
        {
            //originalPoint = e.Location;
            zoomPoint = new PointF(ClientSize.Width / 2, ClientSize.Height / 2);
            float scale = 1f;
            if (e.Delta > 0)
            {
                scale += SCALEFACTOR;
            }
            else if (e.Delta < 0)
            {
                scale -= SCALEFACTOR;
            }
            scaleMatrix.Scale(scale, scale);
            pictureBox.Invalidate();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            startCursor = e.Location;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PointF endCursor = e.Location;
                PointF[] ptArr = { new PointF(endCursor.X - startCursor.X, endCursor.Y - startCursor.Y) };
                scaleMatrix.Invert();
                scaleMatrix.TransformPoints(ptArr);
                scaleMatrix.Invert();
                PointF offsetPoint = (PointF)ptArr.GetValue(0);
                translateMatrix.Translate(offsetPoint.X, offsetPoint.Y);
                startCursor = e.Location;
                pictureBox.Invalidate();
            }
        }

        private void GameCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
