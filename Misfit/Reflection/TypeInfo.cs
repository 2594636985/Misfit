﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Reflection
{
    [Serializable]
    public class TypeInfo
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private Type value;

        public Type Value
        {
            get { return value; }
            set { value = value; }
        }
        private Attribute[] attributes;

        public Attribute[] Attributes
        {
            get { return attributes; }
        }
    }
}
