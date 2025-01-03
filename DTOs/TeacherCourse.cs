using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace finanzauto_Back.DTOs;

public partial class TeacherCourse
{
    public int Id { get; set; }

    public int TeacherId { get; set; }

    public int CourseId { get; set; }

    public DateTime Creationdate { get; set; }

    public DateTime Lastupdate { get; set; }

    public int? Createby { get; set; }

    public int? Updatedby { get; set; }
    [JsonIgnore]
    public virtual Course Course { get; set; } = null!;
    [JsonIgnore]
    public virtual Teacher Teacher { get; set; } = null!;
}
