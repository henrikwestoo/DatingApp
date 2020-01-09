using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace DatingApp.Services
{
    public class XMLSerializer
    {
        public static void Serialize<T>(T data, string path)
        {
            try
            {
                if(File.Exists(path))
                {
                    File.Delete(path);
                }

                using (FileStream fs = File.Create(path))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    xs.Serialize(fs, data);
                }

            // todo
            } catch(IOException e)
            {

            }
        }
    }
}