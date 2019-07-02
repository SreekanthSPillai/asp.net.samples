using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Data
{
    public abstract class BaseEntity
    {
        [Key]
        public int ID
        {
            get;
            set;
        }
    }
}
