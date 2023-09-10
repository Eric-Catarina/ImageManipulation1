using System;
using System.IO;

public class DiagonalPrincipal
{
    static string filename = "TexturaProcedural";
    public static void Main()
    {

        /*Console.WriteLine("Informe as dimensões da imagem.");
        Console.WriteLine("Largura: ");
        Config.WIDTH = int.Parse(Console.ReadLine());
        Console.WriteLine("Altura: ");
        Config.HEIGHT = int.Parse(Console.ReadLine());
        */
        Buffer buffer = new Buffer();
        buffer.Clear(Config.MAXCOLORS);

        filename = "DegradeLinha1";
        buffer.DegradeLinha1();
        buffer.Save(filename);

        filename = "DegradeLinha2";
        buffer.DegradeLinha2();
        buffer.Save(filename);

        filename = "DegradeColuna1";
        buffer.DegradeColuna1();
        buffer.Save(filename);

        filename = "DegradeColuna2";
        buffer.DegradeColuna2();
        buffer.Save(filename);

        filename = "DegradeSeno";
        buffer.DegradeSeno();
        buffer.Save(filename);

        filename = "DegradeCosseno";
        buffer.DegradeCosseno();
        buffer.Save(filename);

        filename = "DegradeLinear";
        buffer.DegradeLinear();
        buffer.Save(filename);

        filename = "DegradeDiagonal";
        buffer.DegradeDiagonal();
        buffer.Save(filename);

        filename = "DegradeCircular";
        buffer.DegradeCircular();
        buffer.Save(filename);

    }
}

class Config
{
    public static int WIDTH = 250;
    public static int HEIGHT = 250;
    public static int MAXCOLORS = 255;
}

//** SALVAR IMAGEM
class SaveImage
{
    public static void Save(string s, string name)
    {
        string str = "P2\n" + Config.WIDTH + " " + Config.HEIGHT + "\n" + Config.MAXCOLORS + "\n";
        str += "#nome\n" + s;
        File.WriteAllText(name + ".ppm", str);
    }
}

public class Buffer
{   //** FRAME BUFFER
    public int[,] frame;

    public Buffer()
    {
        frame = new int[Config.WIDTH, Config.HEIGHT];
    }
    public void Clear(int color)
    {
        for (int h = 0; h < Config.HEIGHT; h++)
        {
            for (int w = 0; w < Config.WIDTH; w++)
            {
                frame[w, h] = color;
            }
        }
    }
    public void SetPixel(int x, int y, int color)
    {
        x = Clamp(x, 0, Config.WIDTH - 1);
        y = Clamp(y, 0, Config.HEIGHT - 1);
        color = Clamp(color, 0, Config.MAXCOLORS);
        frame[x, y] = color;
    }
    int Clamp(int v, int min, int max)
    {
        return (v < min) ? min : (v > max) ? max : v;
    }

    float Lerp(float min, float max, float t)
    {
        return (min + (max - min) * t);
    }

    float InverseLerp(float min, float max, float t)
    {
        return (t - min) / (max - min);
    }
    public override string ToString()
    {
        string s = "";
        //for (int h = Config.HEIGHT-1; h >= 0; h--){
        for (int h = 0; h < Config.HEIGHT; h++)
        {
            for (int w = 0; w < Config.WIDTH; w++)
            {
                s += frame[w, h] + " ";
            }
            s += "\n";
        }
        return s;
    }
    public void Save(string name)
    {
        SaveImage.Save(ToString(), name);
    }
    public void DegradeLinha1()
    {
        for (int h = 0; h < Config.HEIGHT; h++)
        {
            for (int w = 0; w < Config.WIDTH; w++)
            {
                int cor = (int)(((float)w / (float)Config.WIDTH) * (float)Config.MAXCOLORS);
                SetPixel(w, h, cor);
            }
        }
    }
    public void DegradeLinha2()
    {
        for (int h = 0; h < Config.HEIGHT; h++)
        {
            for (int w = 0; w < Config.WIDTH; w++)
            {
                int cor = (int)(((float)w / (float)Config.WIDTH) * (float)Config.MAXCOLORS);
                cor = Math.Abs(cor - Config.MAXCOLORS);
                SetPixel(w, h, cor);
            }
        }
    }
    public void DegradeColuna1()
    {
        for (int h = 0; h < Config.HEIGHT; h++)
        {
            for (int w = 0; w < Config.WIDTH; w++)
            {
                int cor = (int)(((float)h / (float)Config.HEIGHT) * (float)Config.MAXCOLORS);
                SetPixel(w, h, cor);
            }
        }
    }
    public void DegradeColuna2()
    {
        for (int h = 0; h < Config.HEIGHT; h++)
        {
            for (int w = 0; w < Config.WIDTH; w++)
            {
                int cor = (int)(((float)h / (float)Config.HEIGHT) * (float)Config.MAXCOLORS);
                cor = Math.Abs(cor - Config.MAXCOLORS);
                SetPixel(w, h, cor);
            }
        }
    }
    public void DegradeSeno()
    {
        float pi = 3.1415f;

        for (int h = 0; h < Config.HEIGHT; h++)
        {
            for (int w = 0; w < Config.WIDTH; w++)
            {
                int cor = (int)(((float)w / (float)Config.WIDTH) * (float)Config.MAXCOLORS);
                float relativeWidthPosition = (InverseLerp(0, Config.WIDTH, w));
                cor = (int)(Math.Sin(relativeWidthPosition * pi) * (float)Config.MAXCOLORS);
                SetPixel(w, h, cor);
            }
        }
    }
    public void DegradeCosseno()
    {
        float pi = 3.1415f;

        for (int h = 0; h < Config.HEIGHT; h++)
        {
            for (int w = 0; w < Config.WIDTH; w++)
            {
                int cor = (int)(((float)w / (float)Config.WIDTH) * (float)Config.MAXCOLORS);
                float relativeWidthPosition = (InverseLerp(0, Config.WIDTH, w));
                cor = (int)(Math.Abs(Math.Cos(relativeWidthPosition * pi)) * (float)Config.MAXCOLORS);
                SetPixel(w, h, cor);
            }
        }
    }

    public void DegradeLinear()
    {
        for (int h = 0; h < Config.HEIGHT; h++)
        {
            for (int w = 0; w < Config.WIDTH; w++)
            {
                float relativeWidthPosition = (float)w / (float)Config.WIDTH;
                int cor = (int)(relativeWidthPosition * Config.MAXCOLORS);
                cor = cor * 2;
                if (relativeWidthPosition > 0.5f)
                {
                    relativeWidthPosition = 1 - relativeWidthPosition;
                    cor = (int)(relativeWidthPosition * Config.MAXCOLORS * 2);
                }
                SetPixel(w, h, cor);
            }
        }
    }
    public void DegradeDiagonal()
    {
        for (int h = 0; h < Config.HEIGHT; h++)
        {
            for (int w = 0; w < Config.WIDTH; w++)
            {
                int cor = (int)(((((float)w / (float)Config.WIDTH) + (float)h / (float)Config.HEIGHT)) * (float)Config.MAXCOLORS);
                SetPixel(w, h, cor);
            }
        }
    }

    public void DegradeCircular()
    {
        int GetDistance(int x1, int y1, int x2, int y2)
        {
            return (int)Math.Sqrt((int)Math.Pow(((double)x2 - (double)x1), 2) + Math.Pow(((double)y2 - (double)y1), 2));
        }

        for (int h = 0; h < Config.HEIGHT; h++)
        {
            for (int w = 0; w < Config.WIDTH; w++)
            {

                int[] center = { Config.WIDTH / 2, Config.HEIGHT / 2 };


                int distance = GetDistance(w, h, center[0], center[1]);
                int frameHalfDiagonal = GetDistance(0,0,center[0], center[1]);
            
                int cor = (int)(InverseLerp(0, frameHalfDiagonal, distance) * (float)Config.MAXCOLORS);
                cor = Math.Abs(cor - Config.MAXCOLORS);

                SetPixel(w, h, cor);
            }
        }
    }
}
