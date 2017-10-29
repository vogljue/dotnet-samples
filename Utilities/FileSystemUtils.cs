// Simple Encoder Utilities in C# (Tutorial).
using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace carservice.Utilities
{
    
    /// <summary>
    /// Represents the EncoderUtils class.
    /// </summary>
    public class FileSystemUtils
    {
        public static string ToJson(Type type, Object obj) 
        {
            string jsonString = null;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(type);
            using (MemoryStream stream = new MemoryStream()) 
            {
                ser.WriteObject(stream, obj);
                jsonString = Encoding.ASCII.GetString(stream.ToArray());                    
            }
            return jsonString;
        }

        public static string LoadHostsFile() 
        {
            // File read
            string content = null;
            string path = "/etc/hosts";
            using (StringWriter writer = new StringWriter())
            {
                writer.WriteLine("=== Reading file {0} ===", path);
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        writer.WriteLine(sr.ReadLine());
                    }
                }
                writer.WriteLine("=== EOF ===");
                content = writer.ToString();
            }
            return content;
        }
        
        public static string GetCurrentDirectory()
        {
            string cwd = Directory.GetCurrentDirectory();
            return cwd;
        }        
        
        public static string ConvertAsciiToEbcdic(string asciiData)     
        {          
            // Create two different encodings.         
            Encoding ascii = Encoding.ASCII;
            Encoding ebcdic = Encoding.GetEncoding("IBM273");          

            //Retutn Ebcdic Data
     
            byte[] ebcdicData = Encoding.Convert(ascii, ebcdic, ascii.GetBytes(asciiData));      
            return ebcdic.GetString(ebcdicData);
        }
        

        public static string ConvertEbcdicToAscii(string ebcdicData)
        {            
            // Create two different encodings.      
            Encoding ascii = Encoding.ASCII;
            Encoding ebcdic = Encoding.GetEncoding("IBM273"); 

            //Retutn Ascii Data 
             
            byte[] asciiData = Encoding.Convert(ebcdic, ascii, ebcdic.GetBytes(ebcdicData)); 
            return ascii.GetString(asciiData);
        }
        
    }
}