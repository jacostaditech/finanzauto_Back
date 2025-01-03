using System;
using System.Collections.Generic;

namespace finanzauto_Back.DTOs;

public partial class Student
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public long Identification { get; set; }

    public string? Gender { get; set; }

    public DateTime Creationdate { get; set; }

    public DateTime Lastupdate { get; set; }

    public int? Createby { get; set; }

    public int? Updatedby { get; set; }

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
