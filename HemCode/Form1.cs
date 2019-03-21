using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HemCode
{
    public partial class Form1 : Form
    {
        int n;
        int k;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        private void Code()
        {
            StringBuilder mes = ParamCode();
            int n1 = mes.Length;

            int[,] matrix = MatrixCode(k,n);
            for (int i = 0; i < k; i++)
            {

                for (int j = 0; j < n; j++)
                {

                   
                }
            }
            int[] coef = new int[k];
            int count = 0;
            for(int i=0;i<k;i++)
            {
              count = 0;
                for(int j=0;j<n;j++)
                {
                    
                    if(matrix[i,j]==1 && mes[j]!='k')
                    {
                        count++;
                        if (count == 1)
                        {
                            coef[i] = Convert.ToInt16(mes[j]);

                        }
                        else
                        {
                            coef[i] ^= Convert.ToInt16(mes[j]);
                        }

                    }
                }
            }

            for(int i=0;i<k;i++)
            {
                int y = (int)Math.Pow(2, i);
                if(mes[y-1]=='k')
                {
                    mes[y-1] = Convert.ToChar(coef[i]);
                }
            }
            for (int i = 0; i < k; i++)
            {
                if(coef[i]==48)
                {
                    coef[i] = 0;
                }
                else
                {
                    coef[i] = 1;
                }
            }
            StringBuilder mes_error = Error(mes);
            int[] sind = new int[k];
            for (int i = 0; i < k; i++)
            {
                count = 0;
                sind[i] = coef[i];
                for (int j = 0; j < n; j++)
                {
                    
                    if (matrix[i, j] == 1 && j!=Math.Pow(2,i)-1)
                    {
                        sind[i] ^= mes_error[j] - 48; 
                       

                    }
                }
            }
            StringBuilder bit_error = new StringBuilder();
            for (int i=0;i<k; i++)
            {
              
                bit_error.Append(sind[k-i-1]);

            }
            int c = 0;
            int bit = Convert.ToInt16(bit_error.ToString(), 2);
            foreach(var value in sind)
            {
                if(value==0)
                {
                    c++;
                }
            }
            
                if (c==k)
            {
               
               label1.Text = "Message transferred without errors.("+mes +")";

             }
            else
            {
                label1.Text = "Message transferred with errors in " + bit.ToString() + " bit (" + mes_error + ")";
              if(mes[bit-1]=='0')
                {
                mes_error[bit-1] = '1';
                }
              if(mes[bit-1]=='1')
                 {
                mes_error[bit-1] = '0';
                 }
                label3.Text = "\n Error was fixed: " + mes_error;

            }
            
            
        }
        private StringBuilder ParamCode()
        {
            int m = Convert.ToInt16(textBox1.Text);
            string g = Convert.ToString(m, 2);
            StringBuilder mes = new StringBuilder();
            mes.Append(g);
            
            int n1 = mes.Length;
            k = 0;

            while(k<Math.Log(k+n1+1,2))
            {
                k++;
            }
             n = k + n1;

            int pow = 0;
            int i = 0;
            for (i = 0; ; i++)
            {

                pow = (int)Math.Pow(2, i);
                if (pow >= n)
                {
                    i--;
                    break;
                }
                else
                {
                    string s = 'k'.ToString();
                    mes = mes.Insert(pow-1, s);
                }


            }
            return mes;
        }
        private int [,] MatrixCode(int k,int n)
        {

            int[,] matrix = new int[k,n];
            for(int s=0;s<k;s++)
            {
                for (int r=0;r<n;r++)
                {
                    matrix[s, r] = 0;

                }
                
            }

            int i = 0;
            int x = 0;
            int w = 0;
            int count = 0;
            while (i < k)
            {

              
               int y = Convert.ToInt16(Math.Pow(2, i));
                w = 0;
                while ((y - 1 + w) <n)
                {
                    matrix[i, y - 1 + w] = 1;


                    for (int j = 1; j < y && (y - 1 + j) < n; j++)
                    {
                        matrix[i, y - 1+ w + j] = 1;
                        count++;
                    }

                    w += y + 1 + count;

                    count = 0;


                }
                i++;
               
                
            }

            return matrix;
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private StringBuilder Error(StringBuilder mes)
        {
            Random rnd = new Random();
            int index = rnd.Next(0, n);
            if(mes[index]=='0')
            {
                mes[index] = '1';
            }
            else
            {
                mes[index] = '0';
            }
            return mes;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Code();
        }
    }
}
