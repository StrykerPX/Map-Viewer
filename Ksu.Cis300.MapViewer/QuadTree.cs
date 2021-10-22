using Ksu.Cis300.ImmutableBinaryTrees;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
using System.Linq;
using System.Text;
using System;
using System.Windows.Forms;
using System.IO;

namespace Ksu.Cis300.MapViewer
{
    static class QuadTree
    {
        /// <summary>
        /// Maximum zoom value.
        /// </summary>
        const int MaximumZoom = 7;

        /// <summary>
        /// Threshold value for drawing Quad Tree.
        /// </summary>
        const int DrawingQuadThreshold = 100;

        /// <summary>
        /// Build a tree of given type.
        /// </summary>
        /// <param name="bounds"> bounds of the rectangle.  </param>
        /// <param name="zoom"> Zoom value of the tree. </param>
        /// <param name="isQuadTreeNode"> Boolean of whether the tree is a Quad Tree</param>
        /// <returns>re</returns>
        private static BinaryTreeNode<MapData> BuildTree(RectangleF bounds, int zoom, bool isQuadTreeNode)
        {
            
            if (zoom == MaximumZoom)
            {
                MapData mapData = new MapData(bounds, zoom, true);
                return new BinaryTreeNode<MapData>(mapData, null, null);
            }
            else
            {
                int tempZoom = zoom;
                if (isQuadTreeNode)
                {
                    MapData mapData = new MapData(bounds, zoom, isQuadTreeNode);
                    RectangleF leftBounds = new RectangleF(bounds.X, bounds.Y, (bounds.Width) / 2, bounds.Height);
                    RectangleF rightBounds = new RectangleF(bounds.X + (bounds.Width) / 2, bounds.Y, (bounds.Width) / 2, bounds.Height);
                    return new BinaryTreeNode<MapData>(mapData, BuildTree(leftBounds, tempZoom, !isQuadTreeNode), BuildTree(rightBounds, tempZoom, !isQuadTreeNode));
                }
                else
                {
                    RectangleF topBounds = new RectangleF(bounds.X, bounds.Y, (bounds.Width) / 2, bounds.Height);
                    RectangleF bottomBounds = new RectangleF(bounds.X, bounds.Y + (bounds.Height) / 2, bounds.Width, (bounds.Height) / 2);

                    if (bounds.Width * bounds.Height < DrawingQuadThreshold)
                    {
                        tempZoom ++;
                    }
                    MapData mapData = new MapData(bounds, zoom, isQuadTreeNode);

                    return new BinaryTreeNode<MapData>(mapData, BuildTree(topBounds, tempZoom, !isQuadTreeNode), BuildTree(bottomBounds, tempZoom, !isQuadTreeNode));
                }
            }
        }

        /// <summary>
        /// Add line to a rectangle within the Quad Tree.
        /// </summary>
        /// <param name="t"> Tree to add the line to. </param>
        /// <param name="line"> LinSegment to be added. </param>
        /// <param name="zoom"> Zoom value of the line. </param>
        private static void AddLine(BinaryTreeNode<MapData> t, LineSegment line, int zoom)
        {
            if (zoom == t.Data.Zoom)
            {
                t.Data.Lines.Add(line);
            }
            else
            {
                float divideLine;
                if (t.Data.Lines != null)
                {
                    divideLine = t.Data.Bounds.X + (t.Data.Bounds.Width) / 2;
                }
                else
                {
                    divideLine = t.Data.Bounds.Y + (t.Data.Bounds.Height) / 2;
                    
                }

                if (line.End.X <= divideLine)
                {
                    QuadTree.AddLine(t.LeftChild, line.Reflect(), zoom);
                }else if (line.Start.X >= divideLine)
                {
                    QuadTree.AddLine(t.RightChild, line.Reflect(), zoom);
                }
                else
                {
                    LineSegment left;
                    LineSegment right;

                    line.Split(divideLine, out left, out right);
                    QuadTree.AddLine(t.LeftChild, left.Reflect(), zoom);
                    QuadTree.AddLine(t.RightChild, right.Reflect(), zoom);
                }
            }
        }

        /// <summary>
        /// Read data given within a file and apply the 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns> Tree built with added lines. </returns>
        public static BinaryTreeNode<MapData> ReadFile(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                string lineRead1 = sr.ReadLine();
                string[] lineArr1 = lineRead1.Split(',');
                float width = float.Parse(lineArr1[0]);
                float height = float.Parse(lineArr1[1]);

                if(width <= 0 || height <= 0)
                {
                    throw new IOException("Width and height are invalid.");
                }

                RectangleF rectangle = new RectangleF(0, 0, width, height);
                BinaryTreeNode<MapData> tree =  QuadTree.BuildTree(rectangle, 0, true);

                string lineRead2;
                while ((lineRead2 = sr.ReadLine()) != null)
                {
                    string[] lineArr2 = lineRead2.Split(',');

                    float startX = float.Parse(lineArr2[0]);
                    float startY = float.Parse(lineArr2[1]);
                    float endX = float.Parse(lineArr2[2]);
                    float endY = float.Parse(lineArr2[3]);
                    int color = Convert.ToInt32(lineArr2[4]);
                    float length = float.Parse(lineArr2[5]);
                    int zoom = Convert.ToInt32(lineArr2[6]);

                    PointF start = new PointF(startX, startY);
                    PointF end = new PointF(endX, endY);

                    if (start.X >= 0 && start.X <= tree.Data.Bounds.Width && start.Y >= 0 && start.Y <= tree.Data.Bounds.Height)
                    {
                        if (!(end.X >= 0 && end.X <= tree.Data.Bounds.Width && end.Y >= 0 && end.Y <= tree.Data.Bounds.Height))
                        {
                            throw new IOException("Out of bound.");
                        }
                    }
                    else
                    {
                        throw new IOException("Out of bound.");
                    }

                    if (length <= 0)
                    {
                        throw new IOException("Invalid line.");
                    }

                    if (!(zoom >= 0 && zoom < MaximumZoom))
                    {
                        throw new IOException("Zoom out of bound.");
                    }

                    Pen pen = new Pen(Color.FromArgb(color));

                    LineSegment line = new LineSegment(start, end, pen);
                    QuadTree.AddLine(tree, line, zoom);
                }
                return tree;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="g"></param>
        /// <param name="zoom"></param>
        /// <param name="sacle"></param>
        public static void Draw(BinaryTreeNode<MapData> t, Graphics g, int zoom, int scale)
        {
            float x = t.Data.Bounds.X * scale;
            float y = t.Data.Bounds.Y * scale;
            float width = t.Data.Bounds.Width * scale;
            float height = t.Data.Bounds.Height * scale;

            RectangleF rectangle = new RectangleF(x, y, width, height);

            if (rectangle.IntersectsWith(g.ClipBounds))
            {
                if (t.Data.Lines != null)
                {
                    foreach (LineSegment line in t.Data.Lines)
                    {
                        PointF start = new PointF(line.Start.X * scale, line.Start.Y * scale);
                        PointF end= new PointF(line.End.X * scale, line.End.Y * scale);

                        g.DrawLine(line.Pen, start, end);
                    }
                }

                if (zoom > t.Data.Zoom)
                {
                    QuadTree.Draw(t.LeftChild, g, zoom, scale);
                    QuadTree.Draw(t.RightChild, g, zoom, scale);
                }
            }
        }
    }
}
