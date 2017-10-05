/*
 * Name: Anju Chawla
 * Date: October 2017
 * Purpose:Input sales information about books. 
 * Calculate the extended price and the discount 
 * and discounted price after validating the input.
 * Maintain sale summary information for all sales.
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

namespace BookSales
{

    public partial class BookSalesForm : Form
    {
        //module level variables
        private int TotalQuantity;
        private Decimal TotalDiscountGiven;
        private Decimal TotalDiscountedAmount;
        private Decimal AverageDiscount;

        public BookSalesForm()
        {
            InitializeComponent();
            
            
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            //prints the form
            printForm1.PrintAction = System.Drawing.Printing.PrintAction.PrintToPreview;
            printForm1.Print();

        }

        /// <summary>
        /// Clears the input and and sale value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            //clear the input given by the user 
            titleTextBox.Clear();
            quantityTextBox.Clear();
            priceTextBox.Clear();

            //focus to the title input
            titleTextBox.Focus();
            //clear the current transaction output only, summary information stays
            extendedPriceTextBox.Clear();
            discountTextBox.Clear();
            discountedPriceTextBox.Clear();
        }

        /// <summary>
        /// Exits the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_Click(object sender, EventArgs e)
        {
            //close the application
            this.Close();

        }

        /// <summary>
        /// Calculate the sale amounts and display it at the summary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calculateButton_Click(object sender, EventArgs e)
        {
            //calculate the sale price, discount and the discounted price
            int quantity = 0;
            decimal price = 0;
            decimal totalSale = 0;

            try
            {
                //convert quantity value to int
                quantity = int.Parse(quantityTextBox.Text);
                // quantity = Convert.ToInt32(quantityTextBox.Text);
                
                if (quantity < 0)
                {
                    MessageBox.Show("The number of books must be a whole number", "Quantity Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    quantityTextBox.Clear();
                    quantityTextBox.Focus();
                }
                //convert price to int
                price = decimal.Parse(priceTextBox.Text);

                if (price < 0)
                {
                    MessageBox.Show("The price cannot be less than 0", "Price Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    priceTextBox.Focus();
                    priceTextBox.Clear();
                }

                //maintain the summary sale information


                const decimal DISCOUNT_RATE_Decimal = 0.15m;
                const decimal DISCOUNTED_PRICE_DECIMAL = 0.85m;

                //calculate the sale amounts
                totalSale = (price * quantity);
                String TotalSale = totalSale.ToString();
                int totalSaleIndex = 0;
                if (TotalSale.Length == 1)
                {
                    extendedPriceTextBox.Text = TotalSale;
                }
                else
                {
                    totalSaleIndex = TotalSale.IndexOf(".") + 3;
                    extendedPriceTextBox.Text = TotalSale.Substring(0, totalSaleIndex);
                }
                String fifteenPercent = Decimal.Multiply(totalSale, DISCOUNT_RATE_Decimal).ToString();
                int indexOfDot = fifteenPercent.IndexOf(".") + 3;
                discountTextBox.Text = fifteenPercent.Substring(0, indexOfDot);
                String DiscountedPrice = Decimal.Multiply(totalSale, DISCOUNTED_PRICE_DECIMAL).ToString();
                int Index = DiscountedPrice.IndexOf(".") + 3;
                DiscountedPrice = DiscountedPrice.Substring(0, Index);
                discountedPriceTextBox.Text = DiscountedPrice;


                //summary calculations
                TotalQuantity += quantity;
                TotalDiscountGiven += Decimal.Multiply(totalSale, DISCOUNT_RATE_Decimal);
                TotalDiscountedAmount += Decimal.Multiply(totalSale, DISCOUNTED_PRICE_DECIMAL);
                AverageDiscount = TotalDiscountGiven / TotalQuantity;


                //format output and display
                String totalDiscountGiven = TotalDiscountGiven.ToString();
                int index = totalDiscountGiven.IndexOf(".") + 3;
                String totalDiscountedAmount = TotalDiscountedAmount.ToString();
                int totalDisAmt = totalDiscountedAmount.IndexOf(".") + 3;
                String averageDiscount = AverageDiscount.ToString();
                int avgDiscountIndex = averageDiscount.IndexOf(".") + 3;
                quantitySumTextBox.Text = TotalQuantity.ToString();
                discountSumTextBox.Text = totalDiscountGiven.Substring(0, index);
                discountedAmountSumTextBox.Text = totalDiscountedAmount.Substring(0, totalDisAmt);
                averageDiscountTextBox.Text = averageDiscount.Substring(0, avgDiscountIndex);

            }
            catch (FormatException quantityFE)
            {
                
                if (quantityTextBox.Text == String.Empty)
                {
                    MessageBox.Show("Please enter the number of books", "Quantity Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    quantityTextBox.Focus();
                }
                if (priceTextBox.Text == String.Empty)
                {
                    MessageBox.Show("Please enter the price for the book", "Price Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    priceTextBox.Focus();
                }

                for (int i = 0; i < quantityTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(quantityTextBox.Text[i]))
                    {
                        MessageBox.Show("The number of books must be a whole number.", "Quantity Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        quantityTextBox.Focus();
                        break;
                    }
                }

                for (int i = 0; i < priceTextBox.Text.Length; i++)
                {
                    if (!(Char.IsDigit(priceTextBox.Text[i])))
                    {
                        MessageBox.Show("The price must only contain a '.' or digits.", "Quantity Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        priceTextBox.Focus();
                        break;
                    }
                }
                
                    
                

                

                
            }
            catch(OverflowException quantityOE)
            {
                if (quantity > Int32.MaxValue)
                {
                    MessageBox.Show("The number of books cannot be more than " + Int32.MaxValue);
                    quantityTextBox.Focus();
                }
                if (price > Decimal.MaxValue)
                {
                    MessageBox.Show("The price cannot be more than " + Decimal.MaxValue, "Quantity Overflow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    priceTextBox.Focus();
                }
            }
            catch(Exception quantityE)
            {
                MessageBox.Show(quantityE.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }//of calculate


    }//of class
}//of namespace
