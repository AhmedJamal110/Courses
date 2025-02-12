﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Specifications
{
    public class CourseSpec
    {
        public string? Sort { get; set; }
        public int? CategoryId { get; set; }

        private const int MaxSize = 10;
        private int _PageSize = 5 ;

        public int PageSize
        {
            get { return _PageSize ; }
            set { _PageSize = value > MaxSize ? MaxSize : value ; }
        }

        public int PageIndex { get; set; } = 1;

        private string? _search;

        public string? Search
        {
            get { return _search; }
            set { _search = value.ToLower();}
        }


    }
}
