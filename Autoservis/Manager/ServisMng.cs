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
    public class ServisMng
    {

        public Repo2<Servis> ServisRepo { get; set; }

        public ServisMng(Repo servisRepo)
        {
            ServisRepo = new();
            ServisRepo.Collection = servisRepo.getInstance().GetCollection<Servis>("Service");
        }

        public Servis GetByIdServis(int id)
        {
            return ServisRepo.GetById(id);
        }

        public ObservableCollection<Servis> GetAllServis()
        {
            return ServisRepo.GetAll();
        }

        public void AddAllServis(ObservableCollection<Servis> list)
        {
            ServisRepo.AddAll(list);
        }

        public void RemoveAllServis()
        {
            ServisRepo.RemoveAll();
        }

    }
}
