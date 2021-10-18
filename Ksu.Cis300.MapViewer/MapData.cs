using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.MapViewer
{
    public struct MapData
    {
        /// <summary>
        /// 
        /// </summary>
        public RectangleF Bounds { get; }

        /// <summary>
        /// 
        /// </summary>
        public int Zoom { get; }

        /// <summary>
        /// 
        /// </summary>
        public List<LineSegment> Lines { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="zoom"></param>
        /// <param name="isQuadTreeNode"></param>
        public MapData(RectangleF bounds, int zoom, bool isQuadTreeNode)
        {
            Bounds = bounds;
            Zoom = zoom;

            if (isQuadTreeNode)
            {
                Lines = new List<LineSegment>();
            }
            else
            {
                Lines = null;
            }
        }
    }
}
