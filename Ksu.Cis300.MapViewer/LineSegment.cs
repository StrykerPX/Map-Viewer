/* LineSegment.cs
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
    public struct LineSegment
    {
        /// <summary>
        /// Starting point of the line.
        /// </summary>
        public PointF Start { get; }

        /// <summary>
        /// Ending point of the line.
        /// </summary>
        public PointF End { get;  }

        /// <summary>
        /// Pen property used to draw the line.
        /// </summary>
        public Pen Pen { get; }

        /// <summary>
        /// Constructor method of LineSegment.
        /// </summary>
        /// <param name="start"> Starting point of the LineSegment. </param>
        /// <param name="end"> Ending point of the LineSegment. </param>
        /// <param name="pen"> Pen property used to draw the LineSegment. </param>
        public LineSegment(PointF start, PointF end, Pen pen)
        {
            Start = start;
            End = end;
            Pen = pen;
        }

        /// <summary>
        /// Reflect the line.
        /// </summary>
        /// <returns> Reflected LineSegment. </returns>
        public LineSegment Reflect()
        {
            // Create new Start and End point.
            PointF newStart = new PointF(Start.Y, Start.X);
            PointF newEnd = new PointF(End.Y, End.X);

            // Create new lineSegment using new Start and End.
            LineSegment lineSegment = new LineSegment(newStart, newEnd, Pen);

            return lineSegment; // Return the new LineSegment.
        }

        /// <summary>
        /// Split the line.
        /// </summary>
        /// <param name="x"> X cord of where the LineSegment is split.  </param>
        /// <param name="left"> Left segment of the LineSegment. </param>
        /// <param name="right"> Right segment of the LineSegment. </param>
        public void Split(float x, out LineSegment left, out LineSegment right)
        {
            // Calculate and store the Y cord using the slope.
            float slope = (End.Y - Start.Y) / (End.X - Start.X) * (x - Start.X) + Start.Y;

            PointF splitPoint= new PointF(x, slope);    // create new split point.

            // Initialize and Output the left and right segment of the split/
            left = new LineSegment(Start, splitPoint, Pen);
            right = new LineSegment(splitPoint, End, Pen);
        }
    }
}
