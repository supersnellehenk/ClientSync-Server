using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientSync_Server.Models
{
    public class FileHash
    {
        [Key]
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string Hash { get; set; }
    }
}
