using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using GasWebMap.Core.Data;

namespace GasWebMap.Domain
{
    [Table(Name = "Role")]
    public class Role : EntityBase
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        public int PIndex { get; set; }
    }
}