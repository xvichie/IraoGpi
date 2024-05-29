using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IraoGpi.Application.Management.Tasks.Dtos;

public class TaskDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int Priority { get; set; }

    public int Status { get; set; }
}
