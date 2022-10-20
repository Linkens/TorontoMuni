﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorontoMuni.Data
{
    public class ChoiceList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ID { get; set; }
        public ChoiceListEnum Filter { get; set; }
        public int Key { get; set; }
        public string Value { get; set; }

    }
}
