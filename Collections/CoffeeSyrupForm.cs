/*
 * Name : Anju Chawla
 * Date : November, 2016
 * Purpose: To allow the user to select coffee and syrup flavours.
 * New coffee flavours can be added and old ones removed. 
 * The entire coffee list can be cleared and number of coffee flavours can be displayed.
 * Can print all available coffee flavours and only selected ones too.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Collections
{
    public partial class CoffeeSyrupForm : Form
    {
        public CoffeeSyrupForm()
        {
            InitializeComponent();
        }
      

     

       
        
        
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //shows  information about the application
            AboutForm information = new AboutForm();
            information.ShowDialog();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //terminate the application
            Application.Exit();
        }

        private void addCoffeeFlavourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //add a coffee flavour to the coffee list if it does not exist
            Boolean itemFound = false;
            int itemIndex = 0;

            //if the user has provided a new flavour
            if(coffeeComboBox.Text.Trim() != String.Empty)
            {
                //check if flavour already  exists
                while(!itemFound && itemIndex < coffeeComboBox.Items.Count)
                {
                    if(coffeeComboBox.Text.Trim().ToUpper()== coffeeComboBox.Items[itemIndex].ToString().Trim().ToUpper() )
                    {
                        itemFound = true;

                    }//if
                    else
                    {
                        itemIndex++;
                    }
                }//while

                //if flavour found
                if(itemFound)
                {
                    MessageBox.Show("Duplicate Flavour cannot be added", "Add Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    coffeeComboBox.Focus();
                }
                else
                {
                    //add the flavour
                    coffeeComboBox.Items.Add(coffeeComboBox.Text.Trim());
                    coffeeComboBox.Text = "";
                }

            }//if
            else
            {
                MessageBox.Show("Enter a coffee flavour to add", "Missing Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                coffeeComboBox.Focus();
            }
        }//addCoffee

        private void removeCoffeeFlavourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //remove a coffee flavour if it exists
            Boolean itemFound = false;
            int itemIndex = 0;

            //user types in the coffee flavour to delete
            if (coffeeComboBox.SelectedIndex == -1 && coffeeComboBox.Text.Trim() != String.Empty)
            {
                while (!itemFound && itemIndex < coffeeComboBox.Items.Count)
                {
                    if (coffeeComboBox.Text.Trim().ToUpper() == coffeeComboBox.Items[itemIndex].ToString().Trim().ToUpper())
                    {
                        itemFound = true;
                        
                    }//if
                    else
                    {
                        itemIndex++;
                    }
                }//while

                //if flavour found
                if (!itemFound)
                {
                    MessageBox.Show("Cannot find the flavour to remove", "Remove Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    coffeeComboBox.Focus();
                }
                else
                {
                    //remove the flavour
                    coffeeComboBox.Items.Remove(coffeeComboBox.Items[itemIndex]);
                }


            }
            else  //selection made from list?
            {
                if (coffeeComboBox.SelectedIndex == -1)//no selection made
                {
                    MessageBox.Show("Please select the coffee flavour to delete", "No Selection Made",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    coffeeComboBox.Focus();
                }

                else //selection made
                {
                    coffeeComboBox.Items.RemoveAt(coffeeComboBox.SelectedIndex);
                    // coffeeComboBox.Items.Remove(coffeeComboBox.Items[coffeeComboBox.SelectedIndex]);
                }
            }
        }

        private void clearCoffeeListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //clear the cofffee list-remove all flavours after confirming from user
            DialogResult confirm = MessageBox.Show("Clear all coffee flavours?", "Clear Coffee List",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            //check user response
            if(confirm == DialogResult.Yes)
            {
                coffeeComboBox.Items.Clear();
            }
        }

        private void countCoffeeListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //displaying the number of coffeee flavours
            string message = "The number of available coffee flavours are " + coffeeComboBox.Items.Count;
            MessageBox.Show(message, "Coffee Flavours", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void printAllDocument_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Handle printing and print previews when printing all flavours.

            Font printFont = new Font("Arial", 12);
            float lineHeightFloat = printFont.Height + 2;
            float horizontalPrintLocationFloat = e.MarginBounds.Left;
            float verticalPrintLocationFloat = e.MarginBounds.Top;
            string printLineString;

            //Print the heading
            Font headingFont = new Font("Arial", 14, FontStyle.Bold);
            e.Graphics.DrawString("Coffee Flavours", headingFont,
                Brushes.Black, horizontalPrintLocationFloat,
                verticalPrintLocationFloat);

            // Loop through the entire list.
            //for (int listIndexInteger = 0; listIndexInteger < CoffeeComboBox.Items.Count - 1; listIndexInteger++)
            foreach (Object flavor in coffeeComboBox.Items)
            {
                //increment the  Y position for the next line.
                verticalPrintLocationFloat += lineHeightFloat;

                //Set up a line
                //PrintLineString = CoffeeComboBox.Items[ListIndexInteger].ToString();
                printLineString = flavor.ToString();
                //Send the line to the graphics page object.
                e.Graphics.DrawString(printLineString, printFont,
                    Brushes.Black, horizontalPrintLocationFloat,
                    verticalPrintLocationFloat);
            } // end for
        }

        private void printSelectedDocument_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Handle printing and print previews when printing selected items.

            Font printFont = new Font("Arial", 12);
            Font headingFont = new Font("Arial", 14, FontStyle.Bold);
            float lineHeightFloat = printFont.Height + 2;
            float horizontalPrintLocationFloat = e.MarginBounds.Left;
            float verticalPrintLocationFloat = e.MarginBounds.Top;
            string printLineString;

            //Set up and display heading lines
            printLineString = "Print Selected Item";
            e.Graphics.DrawString(printLineString, headingFont,
                Brushes.Black, horizontalPrintLocationFloat,
                verticalPrintLocationFloat);
            printLineString = "by Anju Chawla";
            verticalPrintLocationFloat += lineHeightFloat;
            e.Graphics.DrawString(printLineString, headingFont,
                Brushes.Black, horizontalPrintLocationFloat,
                verticalPrintLocationFloat);

            // Leave a blank line between the heading and detail line.
            verticalPrintLocationFloat += lineHeightFloat * 2;
            // Set up the selected line.
            printLineString = "Coffee: " + coffeeComboBox.Text +
                "     Syrup: " + syrupListBox.Text;
            // Send the line to the graphics page object.
            e.Graphics.DrawString(printLineString, printFont,
                Brushes.Black, horizontalPrintLocationFloat,
                  verticalPrintLocationFloat);

        }

        private void printSelectedToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //print selected flavours

            if (syrupListBox.SelectedIndex == -1)
            {
                syrupListBox.SelectedIndex = 0;
            }

            if (coffeeComboBox.SelectedIndex != -1)
            {
                printSelectedDocument.Print();
            }
            else
            {
                MessageBox.Show("Please select a coffee flavour", "Print Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                coffeeComboBox.Focus();
            }

        }

        private void previewSelectedToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (syrupListBox.SelectedIndex == -1)
            {
                syrupListBox.SelectedIndex = 0;
            }

            if (coffeeComboBox.SelectedIndex != -1)
            {
                printPreviewDialog1.Document = printSelectedDocument;
                printPreviewDialog1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a coffee flavour", "Print Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                coffeeComboBox.Focus();
            }
        }

        private void printAllToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //print all the flavours
            printAllDocument.Print();

        }

        private void previewAllToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //preview the coffee flavours
            printPreviewDialog1.Document = printAllDocument;
            printPreviewDialog1.ShowDialog();

        }
    }
}
