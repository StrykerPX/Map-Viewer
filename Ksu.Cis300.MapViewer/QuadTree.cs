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
                        RectangleF leftRightBound = new RectangleF();
                        return new BinaryTreeNode<MapData>(mapData, BuildTree(bounds, zoom, isQuadTreeNode), BuildTree(bounds, zoom, isQuadTreeNode));
                    }
                    else
                    {
                        RectangleF
                        return new BinaryTreeNode<MapData>(mapData, BuildTree(bounds, zoom, isQuadTreeNode), BuildTree(bounds, zoom, isQuadTreeNode));
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
            if ()
            {
                line = line.Reflect();
            }

            if (zoom == t.Data.Zoom)
            {
                t.Data.Lines.Add(line);
            }
            else
            {
                if (line.End.X <= t.Data.Bounds.Left)
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
