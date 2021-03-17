using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Words.Extension
{
  public static class EnumExtension
  {
    public static string GetDescription<T>(this T enumeration) where T : System.Enum
    {
      var wType = typeof(T);
      var wMember = wType.GetMember(enumeration.ToString())[0];
      var wCustomAttribute = wMember.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false)[0] as DescriptionAttribute;
      return wCustomAttribute.Description;
    }
  }
}
