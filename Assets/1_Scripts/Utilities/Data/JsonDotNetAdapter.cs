
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using Newtonsoft.Json;
using UnityEngine;

namespace Chi.Utilities.Data
{
    public class JsonDotNetAdapter : IDeserializeAdapter
    {
        public T DeserializeObject<T>(string path, string @object) {

            string fileExtension = TryGetFileExtensionFromPath(path);

            if(fileExtension == null) {
                Debug.LogError("File path is invalid");
                return (T)default;
            }

            try {
                Type type = this.GetType();

                ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                object classObject = constructor.Invoke(new object[] { });

                string methodName = "DeserializeObject_" + fileExtension.ToUpper();
                MethodInfo method = type.GetMethod(methodName);
                MethodInfo generic = method.MakeGenericMethod(typeof(T));
                object[] obj = new object[] { @object };
                T value = (T)generic.Invoke(classObject, obj);

                return (T)value;

            }catch(Exception e) {
                Debug.LogException(e);
                return (T)default;
            }
            

        }

        private string TryGetFileExtensionFromPath(string path) {
            try {
                return GetFileExtensionFromPath(path);
            } catch(Exception e) {
                Debug.LogException(e);
                return null;
            }
}

        private string GetFileExtensionFromPath(string path) {
            string[] pathSplit = path.Split('/');
            string fileName = pathSplit[pathSplit.Length - 1];
            return GetFileExtension(fileName);
        }

        private string GetFileExtension(string fileName) {
            string[] fileNameSplit = fileName.Split('.');
            return fileNameSplit[fileNameSplit.Length - 1];
        }


        public T DeserializeObject_CSV<T>(string @object) {
            try {
                string jsonData = ConvertCsvToJson(@object);
                return JsonConvert.DeserializeObject<T>(jsonData);
            } catch (Exception e) {
                Debug.LogException(e);
                return (T)default;
            }
        }

        public T DeserializeObject_JSON<T>(string @object) {
            try {
                return JsonConvert.DeserializeObject<T>(@object);
            } catch (Exception e) {
                Debug.LogException(e);
                return (T)default;
            }
        }

        public T DeserializeObject_XML<T>(string @object) {
            try {
                string jsonData = ConvertXmlToJson(@object);
                return JsonConvert.DeserializeObject<T>(jsonData);
            } catch (Exception e) {
                Debug.LogException(e);
                return (T)default;
            }
        }

        private string ConvertXmlToJson(string xmlData) {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlData);
            string json = JsonConvert.SerializeXmlNode(xmlDocument);
            return json;
        }

        private string ConvertCsvToJson(string csvData) {
            var lines = csvData.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var header = lines[0].Split(',');
            var jsonArray = new List<Dictionary<string, object>>();

            for (int i = 1; i < lines.Length; i++) {
                var values = lines[i].Split(',');
                var item = new Dictionary<string, object>();
                for (int j = 0; j < header.Length; j++) {
                    item[header[j]] = values[j];
                }
                jsonArray.Add(item);
            }
            return JsonConvert.SerializeObject(jsonArray, Newtonsoft.Json.Formatting.Indented);
        }

        
    }
}

