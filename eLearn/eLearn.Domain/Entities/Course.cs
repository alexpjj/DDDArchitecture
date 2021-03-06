﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.Entities
{
    public class Course : Base.Entity
    {
        public string Description { get; } 
        public Content Content { get; }

        public virtual IEnumerable<ContentCreator> Teachers { get; set; }
        public virtual IEnumerable<User> Students { get; set; }
    }
}
