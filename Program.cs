using System;
using System.IO;

public class DiagonalPrincipal{
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
        //buffer.Clear(Config.MAXCOLORS);

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
        
    }
}

class Config {
    public static int WIDTH = 400;
    public static int HEIGHT = 250;
    public static int MAXCOLORS = 255;
}

//** SALVAR IMAGEM
class SaveImage {
    public static void Save(string s, string name){
        string str =  "P2\n" + Config.WIDTH + " " + Config.HEIGHT + "\n"+ Config.MAXCOLORS +"\n";
        str += "#nome\n" + s;
        File.WriteAllText(name+".ppm", str);
    }
}

public class Buffer {   //** FRAME BUFFER
    public int[,] frame;
    public Buffer(){
        frame = new int[Config.WIDTH, Config.HEIGHT];
    }
    public void Clear(int color){
        for (int h = 0; h < Config.HEIGHT; h++){
            for (int w = 0; w < Config.WIDTH; w++){
                frame[w, h] = color;
            }
        }
    }
    public void SetPixel(int x, int y, int color){
        x = Clamp(x, 0, Config.WIDTH-1);  
        y = Clamp(y, 0, Config.HEIGHT-1);
        color = Clamp(color, 0, Config.MAXCOLORS);
        frame[x, y] = color;
    }
    int Clamp(int v, int min, int max){
        return (v < min)? min : (v > max)? max : v;
    }
    int Lerp(int min, int max, float t){
        return (int)(min + (max - min) * t);
    }
    public override string ToString(){
        string s = "";
        //for (int h = Config.HEIGHT-1; h >= 0; h--){
        for (int h = 0; h <  Config.HEIGHT; h++){
            for (int w = 0; w < Config.WIDTH; w++){
                s += frame[w, h] + " ";
            }
            s += "\n";
        }
        return s;
    }
    public void Save(string name){
            SaveImage.Save(ToString(), name);
    }
    public void DegradeLinha1(){
        for (int h = 0; h < Config.HEIGHT; h++){
            for (int w = 0; w < Config.WIDTH; w++){
                    int cor = (int) (((float) w / (float)Config.WIDTH) * (float) Config.MAXCOLORS);
                    SetPixel(w, h, cor); 
            }
        }
    }    
    public void DegradeLinha2(){

    }
    public void DegradeColuna1(){

    }    
    public void DegradeColuna2(){

    }
    public void DegradeSeno(){

    }      
    public void DegradeCosseno(){

    }  
}
