﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class FileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return System.IO.Path.Combine("", filename);
        }
    }
}
