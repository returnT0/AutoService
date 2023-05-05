using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Autoservis.Repository
{
    public class Repo
    {
        public LiteDatabase Databaze
        {
            get; set;
        }

        public Repo()
        {
            string _BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            Databaze = new LiteDatabase(Path.GetFullPath(Path.Combine(_BaseDirectory, @"..\..\..\..\Db\MyDb.txt")));

        }

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
            IEnumerable<T> ienu = Collection.FindAll();
            ObservableCollection<T> collection = new ObservableCollection<T>();
            foreach (var item in ienu)
            {
                collection.Add(item);
            }
            return collection;
        }

        public void AddAll(ObservableCollection<T> list)
        {
            foreach (var item in list)
            {
                Collection.Insert(item);
            }
        }

        public void RemoveAll()
        {
            Collection.DeleteAll();
        }

    }

}


