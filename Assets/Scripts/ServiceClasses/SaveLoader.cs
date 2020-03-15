using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoader
{
    public static string dataFolderPath = Application.persistentDataPath;

    public static void Save<T>(T data) where T : ISavable
    {
        string path = Path.Combine(dataFolderPath, typeof(T).ToString());
        if (data is IExtensionable) path += (data as IExtensionable).GetExtention();
        File.WriteAllText(path, data.GetSavedData());
    }

    public static T Load<T>() where T : ILoadable<T>, new()
    {
        string path = Path.Combine(dataFolderPath, typeof(T).ToString());
        T instance = new T();
        if (instance is IExtensionable) path += (instance as IExtensionable).GetExtention();
        if (!File.Exists(path)) return new T();
        return instance.GetLoadedData(File.ReadAllText(path));
    }
}