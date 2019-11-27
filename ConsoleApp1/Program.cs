using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoUKnowTheWay
{
    class Graf
    {

        private int qmax, q, modc;
        private int[] p = new int[3];
        private int[] c = new int[4];
        private int[] pc = new int[12];
        private int[] length = new int[12];
        private int[] forcapacity = new int[19];
        private int[] backcapacity = new int[19];
        public Graf()
        {
            pc[0] = 10;
            pc[1] = 16;
            pc[2] = 26;
            pc[3] = 11;
            pc[4] = 18;
            pc[5] = 30;
            pc[6] = 9;
            pc[7] = 20;
            pc[8] = 28;
            pc[9] = 13;
            pc[10] = 20;
            pc[11] = 35;
            for (int i = 0; i < 3; i++)
            {
                Console.Write("Enter p{0}: ", i + 1);
                p[i] = int.Parse(Console.ReadLine());
            }
            for (int i = 0; i < 4; i++)
            {
                Console.Write("Enter c{0}: ", i + 1);
                c[i] = int.Parse(Console.ReadLine());
            }
            qmax = c[0] + c[1] + c[2] + c[3];
            q = 0;
            modc = 0;
            for (int i = 0; i < 19; i++)
            {
                forcapacity[i] = 0;
                backcapacity[i] = 0;
            }
        }

        public void fill()
        {
            for (int o = 0; o < 3; o++)
            {
                forcapacity[o] = p[o];
            }
            int i = 3;
            int u = 0;
            while (i < 15)
            {
                for (int j = 0; j < 3; j++)
                    forcapacity[i + j] = c[u];
                u++;
                i = i + 3;
            }
            for (i = 0; i < 4; i++)
                forcapacity[i + 15] = c[i];
            for (i = 0; i < 12; i++)
            {
                length[i] = pc[i];
            }
        }

        public int findmin(int a, int b, int c)
        {
            int min = qmax - q;
            if (a < min)
                min = a;
            if (b < min)
                min = b;
            if (c < min)
                min = c;
            return min;
        }

        public int findmin(int a, int b, int c, int d, int e)
        {
            int min = qmax - q;
            if (a < min)
                min = a;
            if (b < min)
                min = b;
            if (c < min)
                min = c;
            if (d < min)
                min = d;
            if (e < min)
                min = e;
            return min;
        }
        public void step()
        {
            int min;
            int minnumber = 0;
            int minindex = 0;
            int minindex2 = 0;
            int minindex3 = 0;
            min = 100;
            for (int i = 0; i < 12; i++)
                if (length[i] > 0 && length[i] < min)
                {
                    if (forcapacity[i % 3] > 0 && forcapacity[i + 3] > 0 && forcapacity[15 + i / 3] > 0)
                    {
                        min = length[i];
                        minindex = i;
                    }
                }
            for (int i = 0; i < 12; i++)
                if (i % 3 == 0)
                {
                    if (forcapacity[i % 3] > 0)
                    {
                        for (int j = i + 1; j < i + 3; j++)
                            if (backcapacity[j + 3] > 0)
                                for (int l = j % 3; l < 12; l = l + 3)
                                    if (length[i] - length[j] + length[l] < min)
                                    {
                                        if (forcapacity[l + 3] > 0 && forcapacity[15 + l / 3] > 0)
                                        {
                                            min = length[i] - length[j] + length[l];
                                            minindex = i;
                                            minindex2 = j;
                                            minindex3 = l;
                                        }
                                    }
                    }
                }
                else
                    if (i % 3 == 1)
                {
                    if (forcapacity[i % 3] > 0)
                    {
                        for (int j = i - 1; j < i + 1; j = j + 2)
                            if (backcapacity[j + 3] > 0)
                                for (int l = j % 3; l < 12; l = l + 3)
                                    if (length[i] - length[j] + length[l] < min)
                                    {
                                        if (forcapacity[l + 3] > 0 && forcapacity[15 + l / 3] > 0)
                                        {
                                            min = length[i] - length[j] + length[l];
                                            minindex = i;
                                            minindex2 = j;
                                            minindex3 = l;
                                        }
                                    }
                    }
                }
                else
                if (i % 3 == 2)
                {
                    if (forcapacity[i % 3] > 0)
                    {
                        for (int j = i - 2; j < i; j++)
                            if (backcapacity[j + 3] > 0)
                                for (int l = j % 3; l < 12; l = l + 3)
                                    if (length[i] - length[j] + length[l] < min)
                                    {
                                        if (forcapacity[l + 3] > 0 && forcapacity[15 + l / 3] > 0)
                                        {
                                            min = length[i] - length[j] + length[l];
                                            minindex = i;
                                            minindex2 = j;
                                            minindex3 = l;
                                        }
                                    }
                    }
                }
            if (minindex2 == 0 && minindex3 == 0)
            {
                minnumber = findmin(forcapacity[minindex % 3], forcapacity[minindex + 3], forcapacity[15 + minindex / 3]);
                q = q + minnumber;
                modc = modc + minnumber * min;
                forcapacity[minindex % 3] = forcapacity[minindex % 3] - minnumber;
                backcapacity[minindex % 3] = backcapacity[minindex % 3] + minnumber;
                forcapacity[minindex + 3] = forcapacity[minindex + 3] - minnumber;
                backcapacity[minindex + 3] = backcapacity[minindex + 3] + minnumber;
                forcapacity[15 + minindex / 3] = forcapacity[15 + minindex / 3] - minnumber;
                backcapacity[15 + minindex / 3] = backcapacity[15 + minindex / 3] + minnumber;
            }
            else
            {
                minnumber = findmin(forcapacity[minindex % 3], forcapacity[minindex + 3], backcapacity[minindex2 + 3], forcapacity[minindex3 + 3], forcapacity[15 + (minindex3) / 3]);
                q = q + minnumber;
                modc = modc + minnumber * min;
                forcapacity[minindex % 3] = forcapacity[minindex % 3] - minnumber;
                backcapacity[minindex % 3] = backcapacity[minindex % 3] + minnumber;
                forcapacity[minindex + 3] = forcapacity[minindex + 3] - minnumber;
                backcapacity[minindex + 3] = backcapacity[minindex + 3] + minnumber;
                backcapacity[minindex2 + 3] = backcapacity[minindex2 + 3] - minnumber;
                forcapacity[minindex2 + 3] = forcapacity[minindex2 + 3] + minnumber;
                forcapacity[minindex3 + 3] = forcapacity[minindex3 + 3] - minnumber;
                backcapacity[minindex3 + 3] = backcapacity[minindex3 + 3] + minnumber;
                forcapacity[15 + minindex3 / 3] = forcapacity[15 + minindex3 / 3] - minnumber;
                backcapacity[15 + minindex3 / 3] = backcapacity[15 + minindex3 / 3] + minnumber;
            }
            Console.WriteLine("q = {0}, c = {1}", q, modc);
        }

        public void potok()
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("          {0}-P1-     {1}-C1-{2}        ", backcapacity[0], backcapacity[3], backcapacity[15]);
            Console.WriteLine("                      {0}               ", backcapacity[4]);
            Console.WriteLine("                      {0}               ", backcapacity[5]);
            Console.WriteLine("                                        ");
            Console.WriteLine("                      {0}-C2-{1}           ", backcapacity[6], backcapacity[16]);
            Console.WriteLine("                      {0}               ", backcapacity[7]);
            Console.WriteLine("                      {0}               ", backcapacity[8]);
            Console.WriteLine("S-        {0}-P2-                     -T  ", backcapacity[1]);
            Console.WriteLine("                      {0}-C3-{1}            ", backcapacity[9], backcapacity[17]);
            Console.WriteLine("                      {0}              ", backcapacity[10]);
            Console.WriteLine("                      {0}              ", backcapacity[11]);
            Console.WriteLine("                                        ");
            Console.WriteLine("                      {0}-C4-{1}           ", backcapacity[12], backcapacity[18]);
            Console.WriteLine("                      {0}              ", backcapacity[13]);
            Console.WriteLine("          {0}-P3-      {1}              ", backcapacity[2], backcapacity[14]);
            Console.WriteLine("----------------------------------------");
        }
        public void justdoit()
        {
            fill();
            Console.WriteLine("****ZERO GRAF****");
            potok();
            int i = 1;
            while (q != qmax)
            {
                Console.WriteLine("********STEP {0}*********", i);
                step();
                potok();
                i++;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Graf graf = new Graf();
            graf.justdoit();
            Console.ReadKey();
        }
    }
}

