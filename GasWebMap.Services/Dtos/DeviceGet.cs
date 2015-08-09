using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GasWebMap.Domain;
using GasWebMap.Services.Responses;
using ServiceStack.ServiceHost;

namespace GasWebMap.Services.Dtos
{
    [Route("/device", "GET")]
    public class DeviceGet
    {
        
        public DeviceGet()
        {
            Page = 1;
            Rows = 20;
        }

        public string sort { get; set; }

        public string order { get; set; }

        public string Name { get; set; }

        public string WorkShop { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Page { get; set; }
        public int Rows { get; set; }

        public void Valid()
        {
            if (Page <= 0)
            {
                Page = 1;
            }
            if (Rows < 1)
            {
                Rows = 20;
            }

            if (!StartDate.HasValue)
            {
                StartDate = new DateTime(1900, 1, 1);
            }
            if (!EndDate.HasValue)
            {
                EndDate = new DateTime(2200,1,1);
            }
            StartDate = StartDate.Value.Date;
            EndDate = EndDate.Value.Date.AddDays(1);
            if (string.IsNullOrEmpty(Name))
            {
                Name = "";
            }
        }
    }

    [Route("/devicexls", "GET")]
    public class DeviceExport
    {

        public string Name { get; set; }

        public string WorkShop { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public void Valid()
        {

            if (!StartDate.HasValue)
            {
                StartDate = new DateTime(1900, 1, 1);
            }
            if (!EndDate.HasValue)
            {
                EndDate = new DateTime(2200, 1, 1);
            }
            StartDate = StartDate.Value.Date;
            EndDate = EndDate.Value.Date.AddDays(1);
            if (string.IsNullOrEmpty(Name))
            {
                Name = "";
            }
        }
    }


    [Route("/device/edit", "POST")]
    public class DeviceEdit : DeviceInfo
    {
    }

    [Route("/device/add", "POST")]
    public class DeviceAdd : DeviceInfo
    {
    }

    [Route("/device/delete", "POST")]
    public class DeleteDevice : DeleteOperation<Guid>
    {
    }

    [Route("/deviceImage", "POST,GET")]
    public class DeviceImage
    {
        public Guid DeviceID { get; set; }
        public string ImageName { get; set; }
    }

    [Route("/deleteImage", "GET")]
    public class DeleteImage
    {
        public Guid DeviceID { get; set; }
        public string ImageID { get; set; }
    }
}
