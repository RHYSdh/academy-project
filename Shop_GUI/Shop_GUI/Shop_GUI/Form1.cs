using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_GUI
{
    public partial class Form1 : Form
    {
        string[] drinks = { "Pepsi", "Coke", "Evian", "Tango", "7up", "Dr Pepper", "Sprite", "Mountain Dew", "Monster", "Redbull", "Fanta" };
        decimal[] prices = { 0.99M, 1.20M, 0.70M, 0.89M, 1.00M, 1.10M, 0.99M, 1.50M, 2.10M, 2.00M, 1.20M };
        sql_connection SQL = new sql_connection();
        public Form1()
        {
            InitializeComponent();
                        
            for (int i = 0; i < drinks.Length; i++)
            {
                listBox1.Items.Add(drinks[i].ToString());
                listBox2.Items.Add(drinks[i].ToString());
            }
            monthCalendar1.MaxDate = DateTime.Now;
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker2.MaxDate = DateTime.Now;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.Text;
            textBox2.Text = prices[Array.IndexOf(drinks,listBox1.Text)].ToString();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Text = listBox2.Text;
            textBox5.Text = prices[Array.IndexOf(drinks, listBox2.Text)].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string product = "";
            int quantity = 0;
            decimal price = 0;
                
            if(textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                label13.Text = "Missing Entry";
                label13.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                product = textBox1.Text;
                int value;
                if (int.TryParse(textBox3.Text, out value))
                {
                    quantity = Int32.Parse(textBox3.Text);
                }

                decimal temp;
                if (decimal.TryParse(textBox2.Text, out temp))
                {
                    price = decimal.Parse(textBox2.Text);
                }

                if(product.Length > 0 && quantity > 0 && price > 0)
                {
                    MessageBox.Show("Added");
                    SQL.SQLCreate(product, quantity, price, monthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd"));
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "1";
                    label13.Text = "";

                }
                else
                {
                    label13.Text = "Invalid Entry";
                    label13.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string whereString = "";
            string product = "";
            string price = "";

            if (textBox4.Text.Length > 0)
            {
                whereString = $"where productname = '{textBox4.Text}'";
                product = textBox4.Text;
            }

            if (textBox5.Text.Length > 0)
            {
                if(whereString.Length > 0)
                {
                    whereString += $" and price = '{textBox5.Text}'";
                }
                else
                {
                    whereString = $"where price = '{textBox5.Text}'";
                }
                price = textBox5.Text;
            }

            if (whereString.Length > 0)
            {
                whereString += $" and saledate >= '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' and saledate <= '{dateTimePicker2.Value.ToString("yyyy-MM-dd")}' group by productname order by 2 desc;";
            }
            else
            {
                whereString = $"where saledate >= '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' and saledate <= '{dateTimePicker2.Value.ToString("yyyy-MM-dd")}' group by productname order by 2 desc;";
            }

            Form2 report = new Form2();
            report.richTextBox1.Text = $"Search for Product: {product}, Price: {price}, Date: {dateTimePicker1.Value.ToString("yyyy-MM-dd")} - {dateTimePicker2.Value.ToString("yyyy-MM-dd")}\n\n";
            report.richTextBox1.Text += " Product Name,  Quantity,      Price,         Total\n";
            report.richTextBox1.Text += SQL.SQLReturn("productname,sum(quantity),price,sum(quantity)*price", 4, whereString, 14);
            report.richTextBox1.Text += "                                  Subtotal: £" + SQL.SQLReturn("sum(quantity*price)", 1, whereString.Substring(0, whereString.Length-37), 14);
            report.Show();
        }
    }
}
