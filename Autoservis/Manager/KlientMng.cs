using Autoservis.Model;
using Autoservis.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservis.Manager
{
    public class KlientMng
    {
        public Repo2<Zakaznik> KlientRepo { get; set; }

        public KlientMng(Repo klientRepo)
        {
            KlientRepo = new();
            KlientRepo.Collection = klientRepo.getInstance().GetCollection<Zakaznik>("Zakaznik");
        }

        public Zakaznik GetByIdKlient(int id)
        {
            return KlientRepo.GetById(id);
        }

        public ObservableCollection<Zakaznik> GetAllKlient()
        {
            return KlientRepo.GetAll();
        }

        public void AddAllKlient(ObservableCollection<Zakaznik> list)
        {
            KlientRepo.AddAll(list);
        }

        public void RemoveAllKlient()
        {
            KlientRepo.RemoveAll();
        }

    
    }
}