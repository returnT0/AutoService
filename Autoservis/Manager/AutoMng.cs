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
    public class AutoMng
    {

        public Repo2<Auto> AutoRepo { get; set; }

        public AutoMng(Repo autoRepo)
        {
            AutoRepo = new();
            AutoRepo.Collection = autoRepo.getInstance().GetCollection<Auto>("Auto");
        }

        public Auto GetByIdAuto(int id)
        {
            return AutoRepo.GetById(id);
        }

        public ObservableCollection<Auto> GetAllAuto()
        {
            return AutoRepo.GetAll();
        }

        public void AddAllAuto(ObservableCollection<Auto> list)
        {
            AutoRepo.AddAll(list);
        }

        public void RemoveAllAuto()
        {
            AutoRepo.RemoveAll();
        }


    }
}

