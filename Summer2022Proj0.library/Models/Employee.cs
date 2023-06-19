﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Summer2022Proj0.library.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public Employee()
        {
            Name = "Default Name";
            Id = 0;
            Rate = 0;
        }
        public override string ToString()
        {
            return $"{Id}. {Name} paid {Rate} an hour";
        }
    }
}
