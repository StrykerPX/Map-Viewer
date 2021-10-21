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
        /// 
        /// </summary>
        const int MaximumZoom = 7;

        /// <summary>
        /// 
        /// </summary>
        const int DrawingQuadThreshold = 100;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="zoom"></param>
        /// <param name="isQuadTreeNode"></param>
        /// <returns></returns>
        private static BinaryTreeNode<MapData> BuildTree(RectangleF bounds, int zoom, bool isQuadTreeNode)
        {
            MapData mapData = new MapData(bounds, zoom, isQuadTreeNode);
            if (zoom == MaximumZoom)
            {
                return new BinaryTreeNode<MapData>(mapData, null, null);
            }
            else
            {
                if (isQuadTreeNode)
                {
                    RectangleF leftBounds = new RectangleF(bounds.X, bounds.Y, (bounds.Width) / 2, bounds.Height);
                    RectangleF rightBounds = new RectangleF(bounds.X + (bounds.Width) / 2, bounds.Y, (bounds.Width) / 2, bounds.Height);
                    return new BinaryTreeNode<MapData>(mapData, BuildTree(leftBounds, zoom, isQuadTreeNode), BuildTree(rightBounds, zoom, isQuadTreeNode));
                }
                else
                {
                    RectangleF topBounds = new RectangleF(bounds.X, bounds.Y, (bounds.Width) / 2, bounds.Height);
                    RectangleF bottomBounds = new RectangleF(bounds.X, bounds.Y + (bounds.Height) / 2, bounds.Width, (bounds.Height) / 2);

                    if (bounds.Width * bounds.Height < DrawingQuadThreshold)
                    {
                        zoom = zoom + 1;
                    }

                    return new BinaryTreeNode<MapData>(mapData, BuildTree(topBounds, zoom, isQuadTreeNode), BuildTree(bottomBounds, zoom, isQuadTreeNode));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="line"></param>
        /// <param name="zoom"></param>
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
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public BinaryTreeNode<MapData> ReadFile(string fileName)
        {
            float width;
            float height;

            using (StreamReader sr = new StreamReader(fileName))
            {
                width = float.Parse(sr.ReadLine().Split(',')[0]);
                height = float.Parse(sr.ReadLine().Split(',')[1]);

                RectangleF rectangle = new RectangleF(0, 0, width, height);
                QuadTree.BuildTree(rectangle, 0, true);
                while (sr.ReadLine() != null)
                {
                    
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="zoom"></param>
        /// <param name="sacle"></param>
        public void Draw(Graphics g, int zoom, int sacle)
        {

        }
    }
}
