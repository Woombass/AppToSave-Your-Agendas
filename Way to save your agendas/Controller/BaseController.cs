using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Way_to_save_your_agendas.Controller
{
    public abstract class BaseController
    {
        protected virtual T Load<T>(string filePath)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                if (File.Exists(filePath))
                {
                    try
                    {
                        var deserialized = (T)formatter.Deserialize(fs);
                        return deserialized;
                    }
                    catch (Exception e)
                    {
                        return default;
                    }
                    
                }
                else
                {
                    return default;
                }
            }
        }

        protected virtual void Save(string filePath, object toSerialize)
        {
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs,toSerialize);
            }
        }

    }
}
