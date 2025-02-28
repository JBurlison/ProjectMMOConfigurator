using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using ProjectMMOConfigurator.Services;

namespace ProjectMMOConfigurator.Forms
{
    public partial class SearchSkillsForm : Form
    {
        // ...existing code...

        private void ResultsListView_DoubleClick(object sender, EventArgs e)
        {
            if (resultsListView.SelectedItems.Count > 0)
            {
                string filePath = resultsListView.SelectedItems[0].Text;
                
                // Check if the parent MainForm opened this dialog - if so, return the selected file
                // For simplicity, we'll just open the file in the system's default JSON editor
                try
                {
                    if (File.Exists(filePath))
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = filePath,
                            UseShellExecute = true
                        });
                    }
                    else
                    {
                        MessageBox.Show($"File not found: {filePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
