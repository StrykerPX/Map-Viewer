using Ksu.Cis300.ImmutableBinaryTrees;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
using System.Linq;
using System.Text;
using System;


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
                   return new BinaryTreeNode<MapData>(mapData, BuildTree(bounds, zoom, !isQuadTreeNode), BuildTree(bounds, zoom, !isQuadTreeNode));
                }
                else
                {
                    if (bounds.Height * bounds.Width < DrawingQuadThreshold)
                    {
                        RectangleF leftBounds = new RectangleF(bounds.X, bounds.Y, (bounds.Width) / 2, bounds.Height);
                        RectangleF rightBounds = new RectangleF((bounds.Width) / 2, bounds.Y, (bounds.Width) / 2, bounds.Height);
                        return new BinaryTreeNode<MapData>(mapData, BuildTree(leftBounds, zoom, isQuadTreeNode), BuildTree(rightBounds, zoom, isQuadTreeNode));
                    }
                    else
                    {
                        RectangleF topBounds = new RectangleF(bounds.X, bounds.Y, (bounds.Width) / 2, bounds.Height);
                        RectangleF bottomBounds = new RectangleF(bounds.X, (bounds.Height) / 2, bounds.Width, (bounds.Height) / 2);

                        if (bounds.Width * bounds.Height < DrawingQuadThreshold)
                        {
                            zoom = zoom + 1;
                        }

                        return new BinaryTreeNode<MapData>(mapData, BuildTree(topBounds, zoom, isQuadTreeNode), BuildTree(bottomBounds, zoom, isQuadTreeNode));
                    }
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
                float temp;
                if (t.Data.Lines != null)
                {
                    temp = t.Data.Bounds.X + (t.Data.Bounds.Width) / 2;
                }
                else if (t.Data.Lines == null)
                {
                    temp = t.Data.Bounds.Y + (t.Data.Bounds.Height) / 2;
                    t.RightChild.Data.Lines.Add(line.Reflect());
                }
                else
                {
                    
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
