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
    public class CenaMng
    {


        public Repo2<Cena> CenaRepo { get; set; }

        public CenaMng(Repo cenaRepo)
        {
            CenaRepo = new();
            CenaRepo.Collection = cenaRepo.getInstance().GetCollection<Cena>("Price");
        }

        public Cena GetByIdCena(int id)
        {
            return CenaRepo.GetById(id);
        }

        public ObservableCollection<Cena> GetAllCena()
        {
            return CenaRepo.GetAll();
        }

        public void AddAllCena(ObservableCollection<Cena> list)
        {
            CenaRepo.AddAll(list);
        }

        public void RemoveAllCena()
        {
            CenaRepo.RemoveAll();
        }

    }
}

