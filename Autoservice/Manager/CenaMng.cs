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
    public class CenaMng
    {


        public Repo2<Price> CenaRepo { get; set; }

        public CenaMng(Repo cenaRepo)
        {
            CenaRepo = new();
            CenaRepo.Collection = cenaRepo.getInstance().GetCollection<Price>("Price");
        }

        public Price GetByIdCena(int id)
        {
            return CenaRepo.GetById(id);
        }

        public ObservableCollection<Price> GetAllCena()
        {
            return CenaRepo.GetAll();
        }

        public void AddAllCena(ObservableCollection<Price> list)
        {
            CenaRepo.AddAll(list);
        }

        public void RemoveAllCena()
        {
            CenaRepo.RemoveAll();
        }

    }
}