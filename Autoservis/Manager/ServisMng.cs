using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autoservice.Model;
using Autoservice.Repository;

namespace Autoservice.Manager
{
    public class ServisMng
    {

        public Repo2<Service> ServisRepo { get; set; }

        public ServisMng(Repo servisRepo)
        {
            ServisRepo = new();
            ServisRepo.Collection = servisRepo.getInstance().GetCollection<Service>("Service");
        }

        public Service GetByIdServis(int id)
        {
            return ServisRepo.GetById(id);
        }

        public ObservableCollection<Service> GetAllServis()
        {
            return ServisRepo.GetAll();
        }

        public void AddAllServis(ObservableCollection<Service> list)
        {
            ServisRepo.AddAll(list);
        }

        public void RemoveAllServis()
        {
            ServisRepo.RemoveAll();
        }

    }
}