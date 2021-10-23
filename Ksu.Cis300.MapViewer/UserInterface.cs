using Ksu.Cis300.ImmutableBinaryTrees;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ksu.Cis300.MapViewer
{
    public partial class UserInterface : Form
    {
        /// <summary>
        /// Open and read map file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxOpen_Click(object sender, EventArgs e)
        {
            if (uxOpenDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Map.Tree = QuadTree.ReadFile(uxOpenDialog.FileName);
                    uxZoomIn.Enabled = true;
                    uxZoomOut.Enabled = false;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Unable to open file: " + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Zoom into the map.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxZoomIn_Click(object sender, EventArgs e)
        {
            int upperPositonX = Math.Abs(uxMapContainer.AutoScrollPosition.X);
            int upperPositonY = Math.Abs(uxMapContainer.AutoScrollPosition.Y);

            int sizeWidth = uxMapContainer.ClientSize.Width;
            int sizeHeight = uxMapContainer.ClientSize.Height;

            Point newUpperPoiton = new Point(upperPositonX * 2, upperPositonY * 2);
            Size newSize = new Size(sizeWidth / 2, sizeHeight / 2);

            if (Map.ZoomIn() >= 7)
            {
                uxZoomIn.Enabled = false;
            }
            uxZoomOut.Enabled = true;

            uxMapContainer.AutoScrollPosition = newUpperPoiton + newSize;
        }

        /// <summary>
        /// Zoom out of the map.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxZoomOut_Click(object sender, EventArgs e)
        {
            int upperPositonX = Math.Abs(uxMapContainer.AutoScrollPosition.X);
            int upperPositonY = Math.Abs(uxMapContainer.AutoScrollPosition.Y);

            int sizeWidth = uxMapContainer.ClientSize.Width;
            int sizeHeight = uxMapContainer.ClientSize.Height;

            Point newUpperPoiton = new Point(upperPositonX * 2, upperPositonY * 2);
            Size newSize = new Size(sizeWidth / 2, sizeHeight / 2);

            if (Map.ZoomOut() <= 1)
            {
                uxZoomOut.Enabled = false;
            }
            uxZoomIn.Enabled = true;

            uxMapContainer.AutoScrollPosition = newUpperPoiton - newSize;
        }

        public UserInterface()
        {
            InitializeComponent();
        }
    }
}
