using System;
using System.Collections.ObjectModel;
using System.IO;
using LiteDB;

namespace Autoservice.Repository;

public class Repo
{
    public Repo()
    {
        var _BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        Databaze = new LiteDatabase(Path.GetFullPath(Path.Combine(_BaseDirectory, @"..\..\..\..\Db\MyDb.db")));
    }

    public LiteDatabase Databaze { get; set; }

    public LiteDatabase getInstance()
    {
        return Databaze;
    }
}

public class Repo2<T>
{
    public ILiteCollection<T> Collection { get; set; }

    public T GetById(int id)
    {
        return Collection.FindById(id);
    }

    public ObservableCollection<T> GetAll()
    {
        var ienu = Collection.FindAll();
        var collection = new ObservableCollection<T>();
        foreach (var item in ienu) collection.Add(item);
        return collection;
    }

    public void AddAll(ObservableCollection<T> list)
    {
        foreach (var item in list) Collection.Insert(item);
    }

    public void RemoveAll()
    {
        Collection.DeleteAll();
    }
}