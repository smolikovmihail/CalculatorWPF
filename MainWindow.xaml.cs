using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalcBySmolikov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
                  
        }
        
    
        private void Button_Click_1(object sender,RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string tmp = b.Content.ToString();
            if (ResultBox.Text =="0")
            {
                ResultBox.Text = tmp;
            }
            else
            {
                ResultBox.Text += tmp;
            }
        }
        private void Button_comma(object sender,RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string tmp = b.Content.ToString();
            if (!(ResultBox.Text.Contains(',')))
            {
                ResultBox.Text += tmp;
            }
        }
        private void Clear_Button_Click(object sender,RoutedEventArgs e)
        {
            ResultBox.Text = "0";
            TempBox.Clear();
        }
        private void Button_Bin_Ops_Click(object sender,RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string oper = b.Content.ToString();
            if (TempBox.Text == "")
            {
                TempBox.Text = ResultBox.Text + oper;
                ResultBox.Text = "0";
            }
            else
            {
               TempBox.Text =SolveIt()+oper;
               ResultBox.Text = "0";
            }

        }
        private string SolveIt()
        {
            
            double x,y;
            string tmp = TempBox.Text.Remove(TempBox.Text.Length-1);
            string oper=TempBox.Text.Replace(tmp,"");
            x=double.Parse(tmp);
            y=double.Parse(ResultBox.Text.ToString());
            switch (oper)
                {
                    case "+": tmp = (x + y).ToString();
                        break;
                    case "-": tmp = (x - y).ToString();
                        break;
                    case "*": tmp = (x * y).ToString();
                        break;
                    case "/":  tmp = (x / y).ToString();
                        break;
                    case "^": tmp = (Math.Pow(x, y)).ToString();
                        break;
                }

           /* try
            {
                temp = double.Parse(tmp);
            }
            catch (SystemException)
            {
                ErrorMessageSend("You can't divide from zero!");
            }*/
            if (tmp == "бесконечность"||tmp=="NaN")
            {
                tmp = "Error!";
            }
                return tmp;
        }
        private void Button_Click_Sqrt(object sender,RoutedEventArgs e)
        {
            double x = double.Parse(ResultBox.Text);
            if (x >= 0)
            {
                double y = Math.Sqrt(x);
                ResultBox.Text = y.ToString();
            }
            else 
            {
                ErrorMessageSend("Error! You can't get square root from this number!");
            }
        }
        private void Button_Click_Equal(object sender,RoutedEventArgs e)
        {
            if (TempBox.Text != "")
            {
                 string res = SolveIt();
                 ResultBox.Text =res;
                 TempBox.Clear();
            }
                
       }
        
        private void ErrorMessageSend(string mes)
        {
            TempBox.Text = mes;
            ResultBox.Text = "Press CLEAR and try again.";
        }
        private void Pi_Click(object sender, RoutedEventArgs e)
        {
            ResultBox.Text = Math.PI.ToString();
        }
        private void Reverse_Click(object sender,RoutedEventArgs e)
        {
            if (ResultBox.Text.Contains('-'))
            {
                string tmp = ResultBox.Text;
                ResultBox.Text = tmp.Replace("-", "");
            }
            else 
            {
                ResultBox.Text = "-" + ResultBox.Text;
            }
        }
        private void Del_One_Char_Click(object sender, RoutedEventArgs e)
        {
           int test=ResultBox.Text.Length;
           if (test > 1)
           {
               string tmp = ResultBox.Text;
               ResultBox.Text = tmp.Remove(test - 1);
           }
           else
           {
               ResultBox.Text = "0";
           }
        }
        
        private void Trigon_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                double TrigonArg = 0, result = 0, x = double.Parse(ResultBox.Text), mult = 0;

                if (rbtn_deg.IsChecked == true)
                {
                    mult = Math.PI / 180;
                }
                else if (rbtn_rad.IsChecked == true)
                {
                    mult = 1;
                }
                else if (rbtn_grad.IsChecked == true)
                {
                    mult = 0.9 * Math.PI / 180;
                }
                Button b = (Button)sender;
                string func = b.Content.ToString();

                TrigonArg = x * mult;
                switch (func)
                {
                    case "sin(x)": result = Math.Sin(TrigonArg);
                        break;
                    case "cos(x)": result = Math.Cos(TrigonArg);
                        break;
                    case "tg(x)": result = Math.Tan(TrigonArg);
                        break;
                    case "Arcsin": result = (Math.Asin(x) / mult);
                        break;
                    case "Arccos": result = Math.Acos(x) / mult;
                        break;
                    case "Arctan": result = Math.Atan(x) / mult;
                        break;
                }
                if (Math.Abs(result) > Math.Pow(10, 15)) throw new Exception("This operation impossible!");
                if (Math.Abs(result) < Math.Pow(10, -15)) result = 0;
                ResultBox.Text = result.ToString();

            }
            catch (SystemException error)
            {
                MessageBox.Show(error.Message);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            /*if (ResultBox.Text == "бесконечность")
            {
 
            }*/
        }
        private void One_Div_Click(object sender,RoutedEventArgs e)
        {
            double x = double.Parse(ResultBox.Text);
            if (x != 0)
            {
                ResultBox.Text = (1 / x).ToString();
            }
            else
            {
                ErrorMessageSend("You can't divide from zero!");
            }
        }
        /*private void Button_Pow_Click(object sender,RoutedEventArgs e)
        {
            double x = double.Parse(ResultBox.Text);
            //double y = double.Parse(TempBox.Text);
            TempBox.Text = x.ToString()+'^';
            ResultBox.Text = "0";
            if (x == 0 && y < 0)
            {
                ErrorMessageSend("This operation is impossible!");
            }
            else 
            {
                ResultBox.Text = Math.Pow(x, y).ToString();
                TempBox.Text = x.ToString() + "^" + y.ToString();
            }
        }*/
        private void Log_Click(object sender,RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string ident = b.Content.ToString();
            double res = 0;
            double x; 
            try
            {
                x= double.Parse(ResultBox.Text);
                if (x <= 0) throw new Exception("You can`t get logarithm from negative number or zero!");
                switch (ident)
                {
                        
                    case "ln": res = Math.Log(x);
                        break;
                    case "lg": res = Math.Log10(x);
                        break;
                }
           }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);   
            }
            ResultBox.Text = res.ToString();
                        
        }
        private void Button_Exit_Click(object sender,RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
