using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Chi.Utilities.Data;
using UnityEngine;

namespace Chi.Utilities.Data
{
    public enum ServiceType {
        LocalPath,
        Custom,
        AwsS3,
    }

    public interface IDataAccess {
        ServiceType serviceType { get; }
        string Process(string path);
    }

    public class DataAccessLocalPath : IDataAccess
    {
        ServiceType IDataAccess.serviceType => ServiceType.LocalPath;

        string IDataAccess.Process(string path) {
            return ReadFileContents(path);
        }

        private string ReadFileContents(string filePath) {
            try {
                using (StreamReader reader = new StreamReader(filePath)) {
                    return reader.ReadToEnd();
                } 
            } catch (Exception ex) {
                return $"Error reading file: {ex.Message}";
            }
        }
    }
    public class DataAccessAwsS3 : IDataAccess
    {
        ServiceType IDataAccess.serviceType => ServiceType.AwsS3;

        string IDataAccess.Process(string path) {
            throw new NotImplementedException();
        }
    }


    public static class DataAccessFactory{

        private static Dictionary<ServiceType, Type> DataAccessByName = null;
        private static bool isInitalized => DataAccessByName != null;

        private static void InitalizeFactory() {
            var serviceType
                = Assembly.GetAssembly(typeof(IDataAccess)).GetTypes()
                  .Where(type => type.IsClass && typeof(IDataAccess).IsAssignableFrom(type));

            DataAccessByName = new Dictionary<ServiceType, Type>();

            foreach (var type in serviceType) {
                var tempInstance
                    = Activator.CreateInstance(type) as IDataAccess;
                DataAccessByName.Add(tempInstance.serviceType, type);
            }

        }

        public static T RequireDataAccess<T>(ServiceType serviceType, string path) {

            InitalizeFactory();

            if (DataAccessByName.TryGetValue(serviceType, out Type type)) {
                var dataAccess = Activator.CreateInstance(type) as IDataAccess;
                string data = dataAccess.Process(path);
                return TryDeserialize<T>(path, data);
            } else {
                Debug.LogError("Cant find " + serviceType + " in Factory ");
            }

            return (T)default;
        }

        private static T TryDeserialize<T>(string path, string @object) {
            try {
                IDeserializeAdapter deserialize = new JsonDotNetAdapter();
                return deserialize.DeserializeObject<T>(path, @object);
            }catch(Exception e) {
                Debug.LogError(e);
                return (T)default;
            }
            
        }

    }


}

