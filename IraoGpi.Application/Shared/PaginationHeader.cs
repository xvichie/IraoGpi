﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IraoGpi.Application.Shared;

public class PaginationHeader
{
    public int CurrentPage { get; }
    public int ItemsPerPage { get; }
    public int TotalItems { get; }
    public int TotalPages { get; }

    public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
    {
        CurrentPage = currentPage;
        ItemsPerPage = itemsPerPage;
        TotalItems = totalItems;
        TotalPages = totalPages;
    }
}
