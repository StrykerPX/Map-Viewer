/* QuadTree.cs
 * Created by: Sicheng Chen
 */
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
    /// <summary>
    /// 
    /// </summary>
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
        /// <param name="isQuadTreeNode"> Boolean of whether the tree is a Quad Tree. </param>
        /// <returns> Built Tree. </returns>
        private static BinaryTreeNode<MapData> BuildTree(RectangleF bounds, int zoom, bool isQuadTreeNode)
        {
            // Create MapData using the premeter.
            MapData mapData = new MapData(bounds, zoom, isQuadTreeNode);

            // Check if the zoom level is at the maximum.
            if (zoom == MaximumZoom)
            {
                return new BinaryTreeNode<MapData>(mapData, null, null);    // Return Tree Node.
            }
            else
            {
                // Check if it needs to build a Quad Tree or not.
                if (isQuadTreeNode)
                {
                    // Get the left and right bound.
                    RectangleF leftBounds = new RectangleF(bounds.X, bounds.Y, (bounds.Width) / 2, bounds.Height);
                    RectangleF rightBounds = new RectangleF(bounds.X + (bounds.Width) / 2, bounds.Y, (bounds.Width) / 2, bounds.Height);

                    // Return Quad Tree Built.
                    return new BinaryTreeNode<MapData>(mapData, BuildTree(leftBounds, zoom, !isQuadTreeNode), BuildTree(rightBounds, zoom, !isQuadTreeNode));
                }
                else
                {
                    // Get the top and bottom bound.
                    RectangleF topBounds = new RectangleF(bounds.X, bounds.Y, (bounds.Width), bounds.Height / 2);
                    RectangleF bottomBounds = new RectangleF(bounds.X, bounds.Y + (bounds.Height) / 2, bounds.Width, (bounds.Height) / 2);

                    // Check if the area of the bound is less than the threshold or not.
                    if (bounds.Width * bounds.Height < DrawingQuadThreshold)
                    {
                        zoom ++;    // Increment zoom value.
                    }

                    // Return Split Tree built.
                    return new BinaryTreeNode<MapData>(mapData, BuildTree(topBounds, zoom, !isQuadTreeNode), BuildTree(bottomBounds, zoom, !isQuadTreeNode));
                }
            }
        }

        /// <summary>
        /// Add line to the bound within the Quad Tree.
        /// </summary>
        /// <param name="t"> Tree to add the line to. </param>
        /// <param name="line"> LinSegment to be added. </param>
        /// <param name="zoom"> Zoom value of the line. </param>
        private static void AddLine(BinaryTreeNode<MapData> t, LineSegment line, int zoom)
        {
            if (zoom == t.Data.Zoom)    // Check if the current zoom value given is equal to the max.
            {
                t.Data.Lines.Add(line);     // Add the line given.
            }
            else
            {
                float divideLine;   // Veriable to store the divding line value.

                if (t.Data.Lines != null) // Check if Tree is a Quad Tree.
                {
                    // Calculate the dividing line.
                    divideLine = t.Data.Bounds.X + (t.Data.Bounds.Width) / 2;
                }
                else
                {
                    // Calculate the dividing line.
                    divideLine = t.Data.Bounds.Y + (t.Data.Bounds.Height) / 2;
                    
                }

                // Check if the dividing line is within the x bound of the given line.
                if (line.End.X <= divideLine)
                {
                    // Recursively add line.
                    QuadTree.AddLine(t.LeftChild, line.Reflect(), zoom);
                }
                else if (line.Start.X >= divideLine)
                {
                    // Recursively add line.
                    QuadTree.AddLine(t.RightChild, line.Reflect(), zoom);
                }
                else
                {
                    // Create variable to store left and right segment.
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
        /// <param name="fileName"> name of the file to open. </param>
        /// <returns> Tree built with added lines. </returns>
        public static BinaryTreeNode<MapData> ReadFile(string fileName)
        {
            // Using Stream Reader.
            using (StreamReader sr = new StreamReader(fileName))
            {
                // Read and store data of the bound.
                string lineRead1 = sr.ReadLine();
                string[] lineArr1 = lineRead1.Split(',');
                float width = float.Parse(lineArr1[0]);
                float height = float.Parse(lineArr1[1]);

                // Handle invalid width and height.
                if(width <= 0 || height <= 0)
                {
                    throw new IOException("Width and height are invalid.");
                }

                RectangleF rectangle = new RectangleF(0, 0, width, height);
                BinaryTreeNode<MapData> tree =  QuadTree.BuildTree(rectangle, 0, true);

                // Read and store rest of the data.
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

                    // Create new Start and End.
                    PointF start = new PointF(startX, startY);
                    PointF end = new PointF(endX, endY);

                    // Exception handling.
                    if (start.X >= 0 && start.X <= tree.Data.Bounds.Width && start.Y >= 0 && start.Y <= tree.Data.Bounds.Height)
                    {
                        if (!(end.X >= 0 && end.X <= tree.Data.Bounds.Width && end.Y >= 0 && end.Y <= tree.Data.Bounds.Height))
                        {
                            // Line out of bound.
                            throw new IOException("Line n describes a street that is not within the map bounds.");
                        }
                    }
                    else
                    {
                        // Line out of bound.
                        throw new IOException("Line n describes a street that is not within the map bounds.");
                    }

                    if (length <= 0)
                    {
                        // Invalid line.
                        throw new IOException("Line 1 contains a non-positive value.");
                    }

                    if (zoom < 1 || zoom > MaximumZoom)
                    {
                        // Invalid zoom.
                        throw new IOException("Line n contains an invalid zoom level.");
                    }

                    Pen pen = new Pen(Color.FromArgb(color)); // Create new Pen variable.
                    LineSegment line = new LineSegment(start, end, pen);

                    QuadTree.AddLine(tree, line, zoom);     // Add line.
                }
                return tree;
            }
        }

        /// <summary>
        /// Draw the given Tree.
        /// </summary>
        /// <param name="t"> Tree given. </param>
        /// <param name="g"> Graphics given </param>
        /// <param name="zoom"> Zoom value given. </param>
        /// <param name="sacle">  Scale value given. </param>
        public static void Draw(BinaryTreeNode<MapData> t, Graphics g, int zoom, int scale)
        {
            // Scale the positon and size to the appropriate scale.
            float x = t.Data.Bounds.X * scale;
            float y = t.Data.Bounds.Y * scale;
            float width = t.Data.Bounds.Width * scale;
            float height = t.Data.Bounds.Height * scale;

            // Create new rectangle using the new values.
            RectangleF rectangle = new RectangleF(x, y, width, height);

            // Check if the new rectangle intersect with the Graphic bound.
            if (g.ClipBounds.IntersectsWith(rectangle))
            {
                // Check if the Tree is a Quad Tree.
                if (t.Data.Lines != null)
                {
                    // Draw all lines stored in MapData of the Tree.
                    foreach (LineSegment line in t.Data.Lines)
                    {
                        PointF start = new PointF(line.Start.X * scale, line.Start.Y * scale);
                        PointF end= new PointF(line.End.X * scale, line.End.Y * scale);

                        g.DrawLine(line.Pen, start, end);
                    }
                }

                if (zoom > t.Data.Zoom) // Check if the zoom value is greater than zoom value of the Tree.
                {
                    // Draw recursively.
                    QuadTree.Draw(t.LeftChild, g, zoom, scale);
                    QuadTree.Draw(t.RightChild, g, zoom, scale);
                }
            }
        }
    }
}
