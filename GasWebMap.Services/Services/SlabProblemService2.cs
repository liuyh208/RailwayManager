using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GasWebMap.Core;
using GasWebMap.Domain;

namespace GasWebMap.Services.Services
{
    public class SlabProblemService2
    {
        public string Add(IList<SlabProblem> lst)
        {
            var rep = AppEx.Container.GetRepository<SlabProblem>();
            try
            {
                rep.Add(lst);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
