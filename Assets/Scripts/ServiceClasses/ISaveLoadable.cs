public interface ISavable
{
    string GetSavedData();
}

public interface ILoadable<T>
{
    T GetLoadedData(string savedData);
}

public interface IExtensionable
{
    string GetExtention();
}