using Ksu.Cis300.ImmutableBinaryTrees;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ksu.Cis300.MapViewer
{
    public partial class Map : UserControl
    {
        /// <summary>
        /// Initialized sacle value.
        /// </summary>
        private const int _initialScale = 10;

        /// <summary>
        /// Tree to draw onto the map.
        /// </summary>
        private BinaryTreeNode<MapData> _tree;

        /// <summary>
        /// Zoom value of the map.
        /// </summary>
        private int _zoom;

        /// <summary>
        /// sale value of the map.
        /// </summary>
        private int _scale = _initialScale;

        /// <summary>
        /// Get and set the Tree.
        /// </summary>
        public BinaryTreeNode<MapData> Tree
        {
            get
            {
                return _tree;
            }

            set
            {
                _tree = value;

                if (_tree != null)
                {
                    _scale = _initialScale;

                    Width = (int)_tree.Data.Bounds.Width * _scale;
                    Height = (int)_tree.Data.Bounds.Height * _scale;

                    Width += 1;
                    Height += 1;

                    _zoom = 1;
                    Invalidate();
                    
                }
            }
        }

        /// <summary>
        /// Increase zoom value.
        /// </summary>
        /// <returns> New zoom value. </returns>
        public int ZoomIn()
        {
            _zoom++;
            _scale *= 2;

            Width *= 2;
            Height *= 2;

            return _zoom;
        }

        /// <summary>
        /// Decrease zoom value.
        /// </summary>
        /// <returns></returns>
        public int ZoomOut()
        {
            _zoom--;
            _scale /= 2;

            Width /= 2;
            Height /= 2;

            return _zoom;
        }

        /// <summary>
        /// Redraw the map.
        /// </summary>
        /// <param name="e"> PaintEventArgs. </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rectangle = e.ClipRectangle;
            Graphics g = e.Graphics;

            if (_tree != null && g != null)
            {
                Region region = new Region(rectangle);
                g.Clip = region;

                QuadTree.Draw(_tree, g, _zoom, _scale);
            }
            base.OnPaint(e);
        }

        /// <summary>
        /// Create the map User Control.
        /// </summary>
        public Map()
        {
            InitializeComponent();
        }
    }
}
