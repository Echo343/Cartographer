using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DnDScreen.WorldObjects;
using DnDScreen.WorldObjects.Grids;

namespace DnDScreen
{
    public partial class GameCanvas : Form
    {
        private GameWorld gameWorld = new GameWorld();

        Matrix translateMatrix = new Matrix();
        Point startCursor = new Point();
        PointF mouseLocation = new PointF();

        public PointF MouseLocation
        {
            get { return mouseLocation; }
            set { mouseLocation = value; }
        }

        Matrix scaleMatrix = new Matrix();
        PointF zoomPoint = new PointF();
        Matrix zoomTranslateMatrix = new Matrix();

        const float SCALEFACTOR = .075f;

        Random rnd = new Random(2);

        Grid mainGrid;
        //TODO make this highlight a component of a grid
        MouseHighlight mainGridHighlight;

        public GameCanvas()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;

            //Move 0,0 to the center of the screen.
            translateMatrix.Translate(ClientSize.Width / 2, ClientSize.Height / 2);

            mainGrid = new Grid(this);
            mainGridHighlight = new MouseHighlight();
            gameWorld.Add(mainGrid);
            //TODO have controls in Grid to control whether axes are drawn or not.
            gameWorld.Add(new Axes(this));
            gameWorld.Add(mainGridHighlight);
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

        private Graphics CreateGraphicsWithWorldTransform()
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
                mainGridHighlight.Visible = false;
                pictureBox.Invalidate();
            }
            else if (true)
            {
                //TODO move this logic into the highlight and fire an event if changed
                PointF[] ptArr = { new PointF(e.X, e.Y) };
                CreateGraphicsWithWorldTransform().TransformPoints(CoordinateSpace.World, CoordinateSpace.Page, ptArr);
                MouseLocation = ptArr[0];
                float gridSize = mainGrid.GridSize;
                PointF pos = new PointF((float)Math.Floor(ptArr[0].X / gridSize) * gridSize, (float)Math.Floor(ptArr[0].Y / gridSize) * gridSize);
                RectangleF newMouseRectangle = new RectangleF(pos, new SizeF(gridSize, gridSize));
                if (!newMouseRectangle.Equals(mainGridHighlight.HighlightRectangle))
                {
                    mainGridHighlight.HighlightRectangle = newMouseRectangle;
                    pictureBox.Invalidate();
                }
            }
        }

        private void GameCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!mainGridHighlight.Visible)
            {
                mainGridHighlight.Visible = true;
                pictureBox.Invalidate();
            }
        }
    }
}
