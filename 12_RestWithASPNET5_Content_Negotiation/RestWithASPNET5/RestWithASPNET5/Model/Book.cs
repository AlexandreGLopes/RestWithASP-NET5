using RestWithASPNET5.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNET5.Model
{
    // Para poder usar no tipo genérico de repositório vamos fazer Book extender de BaseEntity
    // Como temos o Id já na BaseEntity não vamos mais precisar dele aqui
    [Table("books")]
    public class Book : BaseEntity
    {

        [Column("author")]
        public string Author { get; set; }

        [Column("launch_date")]
        public DateTime LaunchDate { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("title")]
        public string Title { get; set; }
    }
}
