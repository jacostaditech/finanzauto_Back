using System;
using System.Collections.Generic;

namespace finanzauto_Back.DTOs;

public partial class Teacher
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime Creationdate { get; set; }

    public DateTime Lastupdate { get; set; }

    public int? Createby { get; set; }

    public int? Updatedby { get; set; }

    public virtual ICollection<TeacherCourse> TeacherCourses { get; set; } = new List<TeacherCourse>();
}
