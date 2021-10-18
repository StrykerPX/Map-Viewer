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
                        return new BinaryTreeNode<MapData>(mapData, BuildTree(bounds, zoom, isQuadTreeNode), BuildTree(bounds, zoom, isQuadTreeNode));
                    }
                    else
                    {
                        return new BinaryTreeNode<MapData>(mapData, BuildTree(bounds, zoom, isQuadTreeNode), BuildTree(bounds, zoom, isQuadTreeNode));
                    }
                }
            }
        }
    }
}
