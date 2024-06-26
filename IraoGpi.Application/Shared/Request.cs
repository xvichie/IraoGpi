﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IraoGpi.Application.Shared;

public class Request
{
    private const int MaxPageSize = 20;

    public int PageNumber { get; set; } = 1;

    private int _pageSize = 100;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}
