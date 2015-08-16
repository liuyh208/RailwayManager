using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GasWebMap.Domain;
using GasWebMap.Services.Responses;
using ServiceStack.ServiceHost;

namespace GasWebMap.Services.Dtos
{
     [Route("/slab/add", "POST")]
    [Serializable]
    public class SlabProblemDto : SlabProblem
    {
    }

     [Route("/slab/edit", "POST")]
     public class SlabProblemEdit : SlabProblem
     {
     }

    [Route("/slab", "GET")]
    public class SlabGet
    {
        public int Page { get; set; }
        public int Rows { get; set; }

        public string sort { get; set; }

        public string order { get; set; }

        public string RunType { get; set; }

        public string RailWay { get; set; }

        public double Mileage { get; set; }
        public double Mileage2 { get; set; }
        public string HurtItem { get; set; }
        public string HurtPosition { get; set; }
        public string HurtStyle { get; set; }
        public string HurtLevel { get; set; }
        public DateTime CheckDate { get; set; }
        public DateTime CheckDate2 { get; set; }
        public DateTime LogoutDate { get; set; }
        public DateTime LogoutDate2 { get; set; }
    }

    [Route("/slab/delete", "POST")]
    public class DeleteSlab : DeleteOperation<Guid>
    {
    }


    [Route("/slabxls", "GET")]
    public class SlabExport:SlabGet
    {

    }

}
