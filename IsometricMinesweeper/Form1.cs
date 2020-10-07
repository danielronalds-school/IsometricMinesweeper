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

        public Form1()
        {
            InitializeComponent();

            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, Canvas, new object[] { true });

            isometricGrid = new IsometricGrid3D(315, 200, 9);

            renderers.Add(new Renderer2D(isometricGrid.to2D(0), TileMapTemplates.FilledGrid(isometricGrid.to2D(0)), Properties.Resources.updated_isometric_tile));

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

            foreach (ColliderComponent collider in colliders)
            {
                collider.DrawCollider(g, Pens.Red);
            }
        }
    }
}
