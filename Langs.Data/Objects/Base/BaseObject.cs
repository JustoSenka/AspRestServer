using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Langs.Data.Objects.Base
{
    public class BaseObject : IHaveID
    {
        [NotMapped]
        protected const int k_WordLength = 40;

        [Key]
        public virtual int ID { get; set; }
    }
}
