using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Number.Db
{
    public class Input
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int FirstIndex { get; set; }
        [Required]
        public int LastIndex { get; set; }

        public bool isCached { get; set; }

        public int Time { get; set; }
        public int MemoryUsage { get; set; }
        public List<int> FibSequence { get; set; }
    }
}
