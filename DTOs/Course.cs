using System;
using System.Collections.Generic;

namespace finanzauto_Back.DTOs;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Creationdate { get; set; }

    public DateTime Lastupdate { get; set; }

    public int? Createby { get; set; }

    public int? Updatedby { get; set; }

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();

    public virtual ICollection<TeacherCourse> TeacherCourses { get; set; } = new List<TeacherCourse>();
}
