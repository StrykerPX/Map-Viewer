/* UserInterface.cs
 * Modifled by: Sicheng Chen
 */
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
    /// <summary>
    /// 
    /// </summary>
    public partial class UserInterface : Form
    {
        /// <summary>
        /// Open and read map file.
        /// </summary>
        /// <param name="sender"> Event object. </param>
        /// <param name="e"> Event Argument. </param>
        private void uxOpen_Click(object sender, EventArgs e)
        {
            // Open FileDialog.
            if (uxOpenDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Store tree built from file read.
                    Map.Tree = QuadTree.ReadFile(uxOpenDialog.FileName);
                    uxMapContainer.AutoScrollPosition = new Point(0, 0);

                    // Handle button enable & disable.
                    uxZoomIn.Enabled = true;
                    uxZoomOut.Enabled = false;
                }
                catch(Exception ex)     // Handle Exception.
                {
                    MessageBox.Show("Unable to open file: " + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Zoom into the map.
        /// </summary>
        /// <param name="sender"> Event object. </param>
        /// <param name="e"> Event Argument. </param>
        private void uxZoomIn_Click(object sender, EventArgs e)
        {
            // Get center cord of the FlowLayoutPanel.
            int upperPositonX = Math.Abs(uxMapContainer.AutoScrollPosition.X);
            int upperPositonY = Math.Abs(uxMapContainer.AutoScrollPosition.Y);

            // Get current size of the FlowLayoutPanel client.
            int sizeWidth = uxMapContainer.ClientSize.Width / 2;
            int sizeHeight = uxMapContainer.ClientSize.Height / 2;

            // Calculate and store the new positon and size.
            Point newUpperPoiton = new Point(upperPositonX * 2 + sizeWidth, upperPositonY * 2 + sizeHeight);

            // Zoom In.
            // Handle button enable & disable.
            if (Map.ZoomIn() >= 7)
            {
                uxZoomIn.Enabled = false;
            }
            uxZoomOut.Enabled = true;

            // Set the new position.
            uxMapContainer.AutoScrollPosition = newUpperPoiton;
        }

        /// <summary>
        /// Zoom out of the map.
        /// </summary>
        /// <param name="sender"> Event object. </param>
        /// <param name="e"> Event Argument. </param>
        private void uxZoomOut_Click(object sender, EventArgs e)
        {
            // Get center cord of the FlowLayoutPanel.
            int upperPositonX = Math.Abs(uxMapContainer.AutoScrollPosition.X);
            int upperPositonY = Math.Abs(uxMapContainer.AutoScrollPosition.Y);

            // Get current size of the FlowLayoutPanel client.
            int sizeWidth = uxMapContainer.ClientSize.Width  / 4;
            int sizeHeight = uxMapContainer.ClientSize.Height / 4;

            // Calculate and store the new positon and size.
            Point newUpperPoiton = new Point(upperPositonX / 2 - sizeWidth, upperPositonY / 2 - sizeHeight);

            // Zoom Out
            // Handle button enable & disable.
            if (Map.ZoomOut() <= 1)
            {
                uxZoomOut.Enabled = false;
            }
            uxZoomIn.Enabled = true;

            // Set the new position.
            uxMapContainer.AutoScrollPosition = newUpperPoiton;
        }

        /// <summary>
        /// Create UserInterface form.
        /// </summary>
        public UserInterface()
        {
            InitializeComponent();
        }
    }
}
