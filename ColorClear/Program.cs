using ImageMagick;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a image path: ");
        string inputImagePath = @Console.ReadLine(); //If file doesn't exist or is null, ask for a valid file path
        //Check the file paths quickly just to be sure there weren't accidental ""s surrounding the path
        if(inputImagePath[0].Equals('"') && inputImagePath[^1].Equals('\"'))
        {
            inputImagePath = inputImagePath.Substring(1, inputImagePath.Length - 2);
        }
        while (!File.Exists(inputImagePath) || inputImagePath == null)
        {
            Console.WriteLine("File does not exist. Please enter a valid file path: ");
            inputImagePath = @Console.ReadLine();
            if(inputImagePath[0].Equals('"') && inputImagePath[^1].Equals('\"'))
            {
                inputImagePath = inputImagePath.Substring(1, inputImagePath.Length - 2);
            }
        }
        Console.WriteLine("Where do you want to save the output location?");
        string outputImagePath = @Console.ReadLine();
        if (outputImagePath[0].Equals('\"') && outputImagePath[^1].Equals('\"'))
        {
            outputImagePath = outputImagePath.Substring(1, outputImagePath.Length - 2);
        }
        
        while (outputImagePath == null)
        {
            Console.WriteLine("Please enter a valid file path: ");
            outputImagePath = @Console.ReadLine();
            if (outputImagePath[0].Equals('\"') && outputImagePath[^1].Equals('\"'))
            {
                outputImagePath = outputImagePath.Substring(1, outputImagePath.Length - 2);
            }
        }
        //Double check to see if it includes the file name + extension, or if it's just the folder path. If no file is provided, append "output.png" to the end of the path.
        if (!outputImagePath.Contains("."))
        {
            outputImagePath += "output.png";
        }
        Console.WriteLine("How many colors do you want?");
        int desiredMaxColors = Convert.ToInt32(Console.ReadLine());
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
        Console.ReadLine();

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