using ImageMagick;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a image path: ");
        string inputImagePath = @Console.ReadLine() ?? throw new InvalidOperationException();
        Console.WriteLine("How many colors do you want?");
        int desiredMaxColors = Convert.ToInt32(Console.ReadLine());
        string outputImagePath = @"C:\Users\thega\Downloads\output.png";
        try
        {
            using (MagickImage inputImage = new MagickImage(inputImagePath))
            {
                ProcessAndSaveQuantizedImage(inputImage, desiredMaxColors, outputImagePath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
    static void ProcessAndSaveQuantizedImage(MagickImage image, int maxColors, string outputPath)
    {
        image.Quantize(new QuantizeSettings
        {
            Colors = maxColors
        });
        image.Write(outputPath);
        Console.WriteLine($"Color quantization with {maxColors} colors complete.");
    }
    
    
}