using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IsometricGameEngine;
using System.Reflection;

namespace IsometricMinesweeper
{
    public partial class Form1 : Form
    {
        Graphics g;

        IsometricGrid3D isometricGrid;
        IsometricGrid2D playerGrid;

        List<Renderer2D> renderers = new List<Renderer2D>();
        List<ColliderComponent> colliders = new List<ColliderComponent>();

        Point MouseLocation;

        bool collidersVisible = false;

        public Form1()
        {
            InitializeComponent();

            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, Canvas, new object[] { true });

            isometricGrid = new IsometricGrid3D(315, 200, 9);

            renderers.Add(new Renderer2D(isometricGrid.to2D(0), TileMapTemplates.FilledGrid(isometricGrid.to2D(0)), Properties.Resources.TileBase));
            renderers.Add(new Renderer2D(isometricGrid.to2D(0), TileMapTemplates.FilledGrid(isometricGrid.to2D(0)), Properties.Resources.GrassTop));

            playerGrid = isometricGrid.to2D(1);

            colliders = Collision.placeColliders(playerGrid, TileMapTemplates.FilledGrid(playerGrid));
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            foreach (Renderer2D renderer in renderers)
            {
                renderer.Render(g);
            }

            if(collidersVisible)
            { 
                foreach (ColliderComponent collider in colliders)
                {
                    collider.DrawCollider(g, Pens.Red);
                }
            }
        }

        private void Canvas_Click(object sender, EventArgs e)
        {
            Rectangle mouseRec;

            mouseRec = new Rectangle(MouseLocation, new Size(1, 1));

            GridIndex tileIndex = calculateTile(mouseRec);

            if(tileIndex != null)
            {
                Console.WriteLine($"x: {tileIndex.X} y: {tileIndex.Y}");

                RenderComponent tile = findTile(tileIndex);

                if (tile != null)
                {
                    if (tile.Visible)
                    {
                        tile.Visible = false;
                    }
                    else
                    {
                        tile.Visible = true;
                    }
                }
            }

            Canvas.Invalidate();
        }

        private GridIndex calculateTile(Rectangle rectangle)
        {
            GridIndex tileIndex;

            foreach (ColliderComponent collider in colliders)
            {
                foreach(Rectangle colliderRect in collider.Colliders)
                {
                    if(rectangle.IntersectsWith(colliderRect))
                    {
                        tileIndex = IsometricUtilities.findGridID(playerGrid, collider);

                        return tileIndex;
                    }
                }
            }

            Console.WriteLine(false);

            return null;
        }

        private RenderComponent findTile(GridIndex gridIndex)
        {
            GridIndex renderIndex;

            foreach(RenderComponent renderComponent in renderers[1].renderComponents)
            {
                renderIndex = IsometricUtilities.findGridID(isometricGrid.to2D(0), renderComponent.renderRect);

                if (gridIndex.X == renderIndex.X && gridIndex.Y == renderIndex.Y)
                {
                    Console.WriteLine(true);


                    return renderComponent;
                }
            }

            return null;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            MouseLocation = new Point(e.X, e.Y);
        }
    }
}
