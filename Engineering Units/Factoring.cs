﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineering_Units
{
    public static class Factoring
    {
        public static IEngineeringUnits GetEngineeringUnits()
        {
            return new Controller();
        }
    }
}
