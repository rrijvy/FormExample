using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormExample.ViewModels
{
    public class PatientCreateViewModel
    {
        public string Name { get; set; }
        public List<CheckboxSelectList> CheckboxSelectList { get; set; }
    }
}
