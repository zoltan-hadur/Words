using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Words.Model
{
  public class Question
  {
    public string Word { get; set; }
    public string Answer { get; set; }
    public string[] Choices { get; set; }
  }
}
