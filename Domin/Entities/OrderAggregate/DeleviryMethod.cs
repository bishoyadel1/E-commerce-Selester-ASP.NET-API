using Domin.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domian.Entities.OrderAggregate
{
    public class DeleviryMethod 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeleviryTime { get; set; }
        public decimal Price { get; set; }
    }
}