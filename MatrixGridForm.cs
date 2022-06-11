using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MatrixGridAnimationApp
{
    public partial class Form1 : Form
    {
        private Thread workerThread = null;


        public Form1()
        {
            InitializeComponent();
            
            object number;
            number = 0;          
            
        }

        
        private void IncrementCounter(object number)
        {
            int value = (int)number;
            
            while (true)
            {
                Draw(value);
                Thread.Sleep(500);                
                value++;
                if (value == 9)
                    value = 1;
                Invalidate();
            }
        }        

        private void OnClick_Play(object sender, EventArgs e)
        {
            ParameterizedThreadStart parameterizedThreadStart = new ParameterizedThreadStart(IncrementCounter);
            workerThread = new Thread(parameterizedThreadStart);
            workerThread.Start(1); //calls Draw() for generating 1 grid
            
        }

        private void OnClick_Pause(object sender, EventArgs e)
        {
            workerThread.Suspend();
        }

        private void OnClick_Stop(object sender, EventArgs e)
        {
            workerThread.Abort();
            workerThread.Join(500);
            Invalidate();
        }

        private void Resume_Click(object sender, EventArgs e)
        {
            workerThread.Resume();
        }


        private void Draw(int value)
        {
            int size = value * value;
            int[] x, y;
            x = new int[size];
            y = new int[size];
            int a, b;
            a = 50;
            b = 70;            
            Rectangle[] rectangles = new Rectangle[size];       
            int k; // counter for rectangles
            k = 0;
            
            Graphics graphics = CreateGraphics();
            Pen myPen = new Pen(Color.Tomato);
            myPen.Width = 2;

            xCoordinate();
            
            yCoordinate();
            
            rectIndices(x, y);
            workerThread.Join(500); //if not used, misses some grid.
            graphics.DrawRectangles(myPen, rectangles);
            graphics.Dispose();
            
        
            void rectIndices(int[] sample1, int[] sample2)
            {
                for (int i = 0; i < value; i++)  //for x coordinate
                {
                    for (int j = 0; j < value; j++)  //for y coordinate
                    {   
                        
                        rectangles[k] = new Rectangle(sample1[i], sample2[j], 20, 20);
                        k++;
                    }
                }
            }

            void xCoordinate()
            {
                for (int i = 0; i < size; i++)
                {
                    x[i] = a;
                    a += 20;
                }
            }

            void yCoordinate()
            {
                for (int i = 0; i < size; i++)
                {
                    y[i] = b;
                    b += 20;
                }
            }
        }

       
    }       
}
