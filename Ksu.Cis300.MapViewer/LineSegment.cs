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
        /// 
        /// </summary>
        public PointF Start { get; }

        /// <summary>
        /// 
        /// </summary>
        public PointF End { get;  }

        /// <summary>
        /// 
        /// </summary>
        public Pen Pen { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="pen"></param>
        public LineSegment(PointF start, PointF end, Pen pen)
        {
            Start = start;
            End = end;
            Pen = pen;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public LineSegment Reflect()
        {
            PointF newStart = new PointF(Start.Y, Start.X);
            PointF newEnd = new PointF(End.Y, End.X);

            LineSegment lineSegment = new LineSegment(newStart, newEnd, Pen);

            return lineSegment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public void Split(float x, out LineSegment left, out LineSegment right)
        {
            PointF splitPoint= new PointF(x, Start.Y - End.Y);

            left = new LineSegment(Start, splitPoint, Pen);
            right = new LineSegment(splitPoint, End, Pen);
        }
    }
}
