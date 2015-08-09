using System.ComponentModel.DataAnnotations;
using GasWebMap.Core.Data;

namespace GasWebMap.Domain
{
    public class Role : EntityBase
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        public int PIndex { get; set; }
    }
}