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
        /// 
        /// </summary>
        private const int _initialScale = 10;

        /// <summary>
        /// 
        /// </summary>
        private BinaryTreeNode<MapData> _tree;

        /// <summary>
        /// 
        /// </summary>
        private int _zoom;

        /// <summary>
        /// 
        /// </summary>
        private int _scale = _initialScale;

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <returns></returns>
        public int ZoomIn()
        {
            _zoom++;
            _scale *= 2;

            Width *= 2;
            Height *= 2;

            return _zoom;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int ZoomOut()
        {
            _zoom++;
            _scale /= 2;

            Width /= 2;
            Height /= 2;

            return _zoom;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rectangle = e.ClipRectangle;
            Graphics gObject = e.Graphics;

            if (_tree != null && gObject != null)
            {
                Region region = new Region(rectangle);
                gObject.Clip = region;
            }
            base.OnPaint(e);
        }

        /// <summary>
        /// 
        /// </summary>
        public Map()
        {
            InitializeComponent();
        }
    }
}
