/* MapData.cs
 * Created by: Sicheng Chen
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.MapViewer
{
    /// <summary>
    /// 
    /// </summary>
    public struct MapData
    {
        /// <summary>
        /// Rectangle bound of the entire map.
        /// </summary>
        public RectangleF Bounds { get; }

        /// <summary>
        /// Zoom value the current map is at.
        /// </summary>
        public int Zoom { get; }

        /// <summary>
        /// List of lines within the map.
        /// </summary>
        public List<LineSegment> Lines { get; }

        /// <summary>
        /// Constructor method of MapData.
        /// </summary>
        /// <param name="bounds"> Rectangle bound of the entire map. </param>
        /// <param name="zoom"> Zoom value the current map is at. </param>
        /// <param name="isQuadTreeNode"> Boolean of whether the data is stored in a Quad Tree </param>
        public MapData(RectangleF bounds, int zoom, bool isQuadTreeNode)
        {
            Bounds = bounds;
            Zoom = zoom;

            // Set lines according to isQuadTreeNode.
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
