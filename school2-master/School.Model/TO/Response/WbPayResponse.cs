﻿using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class WbPayResponse:AbsResponse
    {
        public PayRequesEntity Model { get; set; }
    }
}
