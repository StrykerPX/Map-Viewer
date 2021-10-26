/* Map.cs
 * Created by: Sicheng Chen
 */
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
    /// <summary>
    /// Class description and methods for UserControl Map.
    /// </summary>
    public partial class Map : UserControl
    {
        /// <summary>
        /// Initialized scale value.
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
        /// Get and set the Tree Property.
        /// </summary>
        public BinaryTreeNode<MapData> Tree
        {
            get
            {
                return _tree;
            }

            set
            {
                //Initialize tree to value.
                _tree = value;

                // Check if tree is null.
                if (_tree != null)
                {
                    // Initialize map scale.
                    _scale = _initialScale;

                    // Scale map size according to scale value.
                    Width = (int)(_tree.Data.Bounds.Width * _scale);
                    Height = (int)(_tree.Data.Bounds.Height * _scale);

                    // Increment Size by 1.
                    Width += 1;
                    Height += 1;


                    _zoom = 1;      // Initialize zoom value to 1.
                    Invalidate();   // Mark the Control for redraw.

                }
            }
        }

        /// <summary>
        /// Increase zoom value.
        /// </summary>
        /// <returns> New zoom value. </returns>
        public int ZoomIn()
        {
            _zoom++;        // Increment zoom value.
            _scale *= 2;    // Double the sacle value.

            Width *= 2;     // Double the width.
            Height *= 2;    // Double the height.

            Invalidate();   // Mark the Control for redraw.
            return _zoom;   // Return the new zoom value.
        }

        /// <summary>
        /// Decrease zoom value.
        /// </summary>
        /// <returns></returns>
        public int ZoomOut()
        {
            _zoom--;        // Decrement zoom value.
            _scale /= 2;    // Half the sacle value.

            Width /= 2;     // Half the width.
            Height /= 2;    // Half the height.

            Invalidate();   // Mark the Control for redraw.
            return _zoom;   // Return the new zoom value.
        }

        /// <summary>
        /// Redraw the map.
        /// </summary>
        /// <param name="e"> PaintEventArgs. </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Create and initialze veriable used for the draw.
            Rectangle rectangle = e.ClipRectangle;
            Graphics g = e.Graphics;

            // Check both the Tree and the Graphic veriables are NOT null.
            if (_tree != null && g != null)
            {
                Region region = new Region(rectangle);      // Create and initialize the region to the rectangle.
                g.Clip = region;                            // Set the Graphic veriable draw bound to the region.


                QuadTree.Draw(_tree, g, _zoom, _scale);     // Call the QudTree Draw() method.
            }
            base.OnPaint(e);                                // Paint graph onto the UserControl.
        }

        /// <summary>
        /// Create the map UserControl.
        /// </summary>
        public Map()
        {
            InitializeComponent();
        }
    }
}
